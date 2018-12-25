using Windows.UI.Xaml.Controls;
using Chat.ViewModels;

namespace Chat.Views
{
	public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            this.InitializeComponent();
			
			ViewModel = new LoginViewModel();

			this.DataContext = ViewModel;
		}

		/// <summary>
        ///		View model for login view.
        /// </summary>
		public LoginViewModel ViewModel { get; private set; }
    }
}
