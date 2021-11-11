﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using CurrencyApp.Database;
using CurrencyApp.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace CurrencyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public IServiceProvider Container { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            Initialize();
        }

        private void Initialize()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                 .Build();

            _host = Host
              .CreateDefaultBuilder()
              .ConfigureServices(services =>
              {
                  services.UseMicrosoftDependencyResolver();
                  var resolver = Locator.CurrentMutable;
                  resolver.InitializeSplat();
                  resolver.InitializeReactiveUI();

                  services.AddSingleton<MainWindow>();

                  // Configure our local services and access the host configuration
                  ConfigureMediatR(services);
                  ConfigureEntityFramework(services);
                  ConfigureServices(services);
                  ConfigureQuartz(services);
              })
              //.UseEnvironment(Environments.Development)
              .Build();

            // Since MS DI container is a different type,
            // we need to re-register the built container with Splat again
            Container = _host.Services;
            Container.UseMicrosoftDependencyResolver();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
        }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<CurrencyContext>(opt => opt.UseSqlite("Filename=app.db")); // TODO get value from config
        }

        private void ConfigureQuartz(IServiceCollection services)
        {
            const string currencyJobKey = nameof(LoadCurrencyJob);
            const string ratesJobKey = nameof(LoadCurrencyRatesJob);

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<LoadCurrencyJob>(opt => opt.WithIdentity(currencyJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(currencyJobKey)
                    .WithIdentity($"{currencyJobKey}-trigger")
                    .WithCronSchedule("10 * * * * ?")); // TODO get value from config

                q.AddJob<LoadCurrencyRatesJob>(opt => opt.WithIdentity(ratesJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(ratesJobKey)
                    .WithIdentity($"{ratesJobKey}-trigger")
                    .WithCronSchedule("30 * * * * ?")); // TODO get value from config
            });

            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}