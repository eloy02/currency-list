using System.Reactive.Disposables;
using System.Windows;
using CurrencyApp.ViewModels;
using ReactiveUI;
using Splat;

namespace CurrencyApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<MainWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(MainWindowViewModel), typeof(MainWindow), null);

        public MainWindowViewModel ViewModel
        {
            get => (MainWindowViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainWindowViewModel)value;
        }

        public MainWindow()
        {
            InitializeComponent();

            if (ViewModel is null)
                ViewModel = Locator.Current.GetService<MainWindowViewModel>();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Router,
                        v => v.Router.Router)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        vm => vm.NavigateToHome,
                        v => v.HomeNav)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        vm => vm.NavigateToCurrencyRate,
                        v => v.CurRatesNav)
                    .DisposeWith(disposables);
            });
        }
    }
}