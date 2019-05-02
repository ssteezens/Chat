using ChatWpf.ViewModels;
using System.Windows;

namespace ChatWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

			// get view model locator
			var locator = (Application.Current.Resources["Locator"] as ViewModelLocator);
			
			// set data context to MainVm
            DataContext = locator?.MainVm;

			// set main vm's navigation frame
			locator?.MainVm.SetNavigationFrame(NavigationFrame);
			locator?.MainVm.NavigateToLogin();
		}
    }
}
