using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using CurrencyApp.ViewModels;
using ReactiveUI;

namespace CurrencyApp.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl, IViewFor<HomeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
           .Register(nameof(ViewModel), typeof(HomeViewModel), typeof(HomeView), null);

        public HomeViewModel ViewModel
        {
            get => (HomeViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (HomeViewModel)value;
        }

        public HomeView()
        {
            InitializeComponent();

            Currency.DisplayMemberPath = nameof(ViewModel.SelectedCurrency.ISOCode);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Currencies,
                        v => v.Currency.ItemsSource)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.SelectedCurrency,
                        v => v.Currency.SelectedItem)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        vm => vm.IsLoading,
                        v => v.Loader.Visibility)
                    .DisposeWith(disposables);

                ViewModel.WhenAnyValue(x => x.SelectedCurrency)
                    .Select(x => x is not null ? Visibility.Visible : Visibility.Collapsed)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => CurrencyRate.Visibility = x)
                    .DisposeWith(disposables);

                ViewModel.WhenAnyValue(x => x.SelectedCurrency, x => x.CurrencyRate)
                    .Where(x => x.Item1 is not null && x.Item2 is not null)
                    .Select(x => $"Курс {x.Item1.Name} к рублю = {x.Item2}")
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(x => CurrencyRate.Text = x)
                    .DisposeWith(disposables);
            });
        }
    }
}