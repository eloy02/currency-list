using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CurrencyApp.Model;
using CurrencyApp.Queries.Handlers;
using DynamicData;
using MediatR;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CurrencyApp.ViewModels
{
    public class CurrencyRateViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
    {
        private readonly IMediator _mediator;
        private readonly SourceCache<CurrencyRate, Guid> _ratesSource = new(x => x.Id);

        private ReadOnlyObservableCollection<CurrencyRate> _rates;

        public ViewModelActivator Activator { get; }

        public string? UrlPathSegment => "rates";

        public IScreen HostScreen { get; }

        public ReactiveCommand<System.Reactive.Unit, IEnumerable<CurrencyRate>> GetRates { get; }

        public ReadOnlyObservableCollection<CurrencyRate> CurrencyRates => _rates;

        [Reactive]
        public bool IsLoading { get; private set; }

        public CurrencyRateViewModel(IMediator mediator, MainWindowViewModel hostScreen)
        {
            _mediator = mediator;
            HostScreen = hostScreen;

            Activator = new ViewModelActivator();

            GetRates = ReactiveCommand.CreateFromTask(() => GetRatesAsync());

            this.WhenActivated(disposables =>
            {
                _ratesSource
                    .Connect()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Bind(out _rates)
                    .DisposeMany()
                    .Subscribe()
                    .DisposeWith(disposables);

                GetRates
                    .Subscribe(x => _ratesSource.AddOrUpdate(x))
                    .DisposeWith(disposables);

                GetRates.IsExecuting
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => IsLoading = x)
                    .DisposeWith(disposables);

                GetRates.Execute().Subscribe();
            });
        }

        private Task<IEnumerable<CurrencyRate>> GetRatesAsync() =>
            _mediator.Send(new GetCurrenciesRatesQuery());
    }
}