using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using CurrencyApp.ViewModels;
using ReactiveUI;

namespace CurrencyApp.Views
{
    /// <summary>
    /// Interaction logic for CurrencyRateView.xaml
    /// </summary>
    public partial class CurrencyRateView : UserControl, IViewFor<CurrencyRateViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
          .Register(nameof(ViewModel), typeof(CurrencyRateViewModel), typeof(CurrencyRateView), null);

        public CurrencyRateViewModel ViewModel
        {
            get => (CurrencyRateViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (CurrencyRateViewModel)value;
        }

        public CurrencyRateView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.CurrencyRates,
                        v => v.Rates.ItemsSource)
                    .DisposeWith(disposables);
            });
        }
    }
}