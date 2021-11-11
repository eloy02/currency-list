using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CurrencyApp.Model;
using CurrencyApp.Queries;
using DynamicData.Binding;
using MediatR;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CurrencyApp.ViewModels
{
    public class HomeViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
    {
        private readonly IMediator _mediator;

        public ViewModelActivator Activator { get; }

        public string? UrlPathSegment => "home";

        public IScreen HostScreen { get; }

        public ReactiveCommand<Currency, decimal?> GetCurrencyRate { get; }

        public ReactiveCommand<System.Reactive.Unit, IEnumerable<Currency>> GetCurrencies { get; }

        [Reactive]
        public ObservableCollectionExtended<Currency> Currencies { get; private set; } = new();

        [Reactive]
        public Currency? SelectedCurrency { get; set; }

        [Reactive]
        public decimal? CurrencyRate { get; private set; }

        [Reactive]
        public bool IsLoading { get; private set; }

        public HomeViewModel(IMediator mediator, MainWindowViewModel hostScreen)
        {
            _mediator = mediator;
            HostScreen = hostScreen;

            Activator = new();

            GetCurrencies = ReactiveCommand.CreateFromTask(() => GetCurrenciesAsync());

            GetCurrencyRate = ReactiveCommand.CreateFromTask<Currency, decimal?>(x => GetCurrencyRateAsync(x));

            this.WhenActivated(disposables =>
            {
                GetCurrencies
                    .Select(x => x.OrderBy(c => c.ISOCode))
                    .Select(x => new ObservableCollectionExtended<Currency>(x))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => Currencies = x)
                    .DisposeWith(disposables);

                GetCurrencyRate
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => CurrencyRate = x)
                    .DisposeWith(disposables);

                this.WhenAnyValue(x => x.SelectedCurrency)
                    .WhereNotNull()
                    .DistinctUntilChanged(x => x.Id)
                    .Select(x => GetCurrencyRate.Execute(x))
                    .Subscribe()
                    .DisposeWith(disposables);

                GetCurrencies.IsExecuting
                    .Concat(GetCurrencyRate.IsExecuting)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => IsLoading = x)
                    .DisposeWith(disposables);
            });
        }

        private Task<IEnumerable<Currency>> GetCurrenciesAsync() =>
            _mediator.Send(new GetCurrencyListQuery());

        private Task<decimal?> GetCurrencyRateAsync(Currency currency) =>
            _mediator.Send(new GetLatestCurrencyRateQuery(currency.Id));
    }
}