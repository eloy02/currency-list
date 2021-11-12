using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using CurrencyApp.Database;
using CurrencyApp.Jobs;
using CurrencyApp.ViewModels;
using CurrencyApp.Views;
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

                  // Configure our local services and access the host configuration
                  ConfigureMediatR(services);
                  ConfigureEntityFramework(services);
                  ConfigureServices(services);
                  ConfigureQuartz(services);
                  RegisterViewModels(services);
              })
              //.UseEnvironment(Environments.Development)
              .Build();

            // Since MS DI container is a different type,
            // we need to re-register the built container with Splat again
            Container = _host.Services;
            Container.UseMicrosoftDependencyResolver();
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
            services.AddDbContext<CurrencyContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("FileDatabase")));
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
                    .WithCronSchedule(Configuration[$"Quartz:{currencyJobKey}"]));

                q.AddJob<LoadCurrencyRatesJob>(opt => opt.WithIdentity(ratesJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(ratesJobKey)
                    .WithIdentity($"{ratesJobKey}-trigger")
                    .WithCronSchedule(Configuration[$"Quartz:{ratesJobKey}"]));
            });

            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }

        private void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IViewFor<MainWindowViewModel>, MainWindow>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<IViewFor<HomeViewModel>, HomeView>();

            services.AddTransient<CurrencyRateViewModel>();
            services.AddTransient<IViewFor<CurrencyRateViewModel>, CurrencyRateView>();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetService<IViewFor<MainWindowViewModel>>() as MainWindow;
            mainWindow.Show();
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