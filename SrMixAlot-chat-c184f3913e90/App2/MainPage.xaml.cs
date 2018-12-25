using Chat.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Chat.Models;

namespace Chat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Vm = new MainViewModel();

            DataContext = Vm;
        }

        public MainViewModel Vm { get; set; }

		/// <summary>
		///		Override behavior for when this view has been navigated to.
		/// </summary>
		/// <param name="e"> Navigation arguments. </param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e.Parameter is User user)
			{
				Vm.HandleNavigationFromLogin(user);
			}
		}
    }
}
