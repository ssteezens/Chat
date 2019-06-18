using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;
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
			
			LoginCommand = new RelayCommand(async () => await Login(), CanLogin);
			PasswordChangedCommand = new RelayCommand<object>(PasswordChanged, true);
        }

        #region Properties

		private bool _canLogin = true;
        private string _username;
        private string _password;
		private string _serverError;

        /// <summary>
        ///		Username for login.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(CanLogin));
            }
        }

        /// <summary>
        ///		Password for login.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(CanLogin));
            }
        }

        /// <summary>
        ///		Gets or sets whether the user can login.
        /// </summary>
        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

		/// <summary>
        ///		Gets or sets the server error.
        /// </summary>
		public string ServerError
		{
			get => _serverError;
			set => Set(ref _serverError, value, nameof(ServerError));
		}

        #endregion

        #region Commands

		/// <summary>
        ///		Command for logging in.
        /// </summary>
		public RelayCommand LoginCommand { get; }

		/// <summary>
        ///		Calls authentication service and gets user.
        /// </summary>
		private async Task Login()
        {
            IsBusy = true;

			// clear the server error if there is one
			ServerError = string.Empty;

			try
			{
				// call authentication service and get current user
				var user = await _authenticationService.AuthenticateUser(Username, Password);

				// set current user
				UserInstance.Current = user;

				MessengerInstance.Send(new NotificationMessage<string>("LoginSuccessful", "LoginSuccessful"));
			}
			catch (Exception)
			{
				ServerError = "Something went wrong when attempting to login.  Please verify your username and password and try again.";
			}

            IsBusy = false;
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
