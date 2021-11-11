using System;
using ReactiveUI;
using Splat;

namespace CurrencyApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }

        public ReactiveCommand<System.Reactive.Unit, IRoutableViewModel> NavigateToHome { get; }

        public ReactiveCommand<System.Reactive.Unit, IRoutableViewModel> NavigateToCurrencyRate { get; }

        public MainWindowViewModel()
        {
            Router = new RoutingState();

            NavigateToHome = ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(Locator.Current.GetService<HomeViewModel>()));

            NavigateToCurrencyRate = ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(Locator.Current.GetService<CurrencyRateViewModel>()));
        }
    }
}