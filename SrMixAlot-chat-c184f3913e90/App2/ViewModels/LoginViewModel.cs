using Windows.System;
using Chat.Services.Data;
using Chat.Services.Data.Interfaces;
using Chat.Services.UI;
using Chat.Services.UI.Interfaces;
using GalaSoft.MvvmLight;
using StructureMap;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Chat.Models;

namespace Chat.ViewModels
{
    /// <summary>
    ///     View model for the login view.
    /// </summary>
    public class LoginViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		private readonly IAuthenticationService _authenticationService;

		public LoginViewModel()
		{
			var container = new Container(config =>
			{
				config.For<INavigationService>().Singleton().Use<NavigationService>()
					.Named("NavigationService");
				config.For<IAuthenticationService>().Singleton().Use<AuthenticationService>()
					.Named("AuthenticationService");
			});

			_navigationService = container.GetInstance<INavigationService>();
			_authenticationService = container.GetInstance<IAuthenticationService>();
		}

        #region Properties

        /// <summary>
        ///     The entered username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     The entered password. 
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Event handlers

		/// <summary>
		///		Event handler for keyboard presses.
		/// </summary>
		public void OnKeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
				Login();
		}

        /// <summary>
        ///		Event handler for when password has changed.
        /// </summary>
        /// <param name="sender"> The password box. </param>
        /// <param name="e"> Args for event. </param>
        public void PasswordChanged(object sender, RoutedEventArgs e)
		{
			// todo: hash and salt password
			Password = ((PasswordBox) sender).Password;
		}

        /// <summary>
        ///     Event handler for when the login button is clicked. 
        /// </summary>
        /// <param name="sender"> The login button. </param>
        /// <param name="e"> Login button clicked event args. </param>
		public void LoginClicked(object sender, RoutedEventArgs e)
		{
			Login();
		}

		/// <summary>
        ///		Authenticates user, navigates to the main page.
        /// </summary>
		private void Login()
		{
			var user = _authenticationService.AuthenticateUser(Username, Password);

			// set the current user instance
            UserInstance.Current = user;

			// navigate to main page.
			_navigationService.Navigate(typeof(MainPage));
        }

        #endregion

    }
}
