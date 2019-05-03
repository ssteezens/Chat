using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for logging in.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
		
		public LoginViewModel(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
			
			LoginCommand = new RelayCommand(Login, CanLogin);
			PasswordChangedCommand = new RelayCommand<object>(PasswordChanged, true);
        }

        #region Properties

		private bool _canLogin = true;
		
		/// <summary>
        ///		Username for login.
        /// </summary>
        public string Username { get; set; }

		/// <summary>
        ///		Password for login.
        /// </summary>
		public string Password { get; set; }

		/// <summary>
        ///		Gets or sets whether the user can login.
        /// </summary>
		public bool CanLogin
		{
			get => _canLogin;
            set => _canLogin = value;
		}

        #endregion

        #region Event Handlers

		/// <summary>
        ///		Command for logging in.
        /// </summary>
		public RelayCommand LoginCommand { get; }

		/// <summary>
        ///		Calls authentication service and gets user.
        /// </summary>
		private void Login()
		{
			// call authentication service and get current user
			var user = _authenticationService.AuthenticateUser(Username, Password);

			// set current user
            UserInstance.Current = user;
			
			MessengerInstance.Send(new NotificationMessage<string>("LoginSuccessful", "LoginSuccessful"));
		}

		/// <summary>
        ///		Command for the password changing.
        /// </summary>
		public RelayCommand<object> PasswordChangedCommand { get; }

		/// <summary>
        ///		Set the password to the value of the password box.
        /// </summary>
        /// <param name="parameter"> The password box. </param>
		private void PasswordChanged(object parameter)
		{
            var passwordBox = (PasswordBox)parameter;
			Password = passwordBox.Password;
		}

        #endregion
    }
}
