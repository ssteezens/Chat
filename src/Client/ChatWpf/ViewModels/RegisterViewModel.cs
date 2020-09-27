using System;
using System.Threading.Tasks;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using ServiceStack;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for register page.
    /// </summary>
    public class RegisterViewModel : ViewModelBase
	{
		private readonly IUserAccountService _userAccountService;

		public RegisterViewModel(IUserAccountService userAccountService)
		{
			_userAccountService = userAccountService;

			GoBackCommand = new RelayCommand(GoBack);
			PasswordChangedCommand = new RelayCommand<object>(PasswordChanged);
			VerifyPasswordChangedCommand = new RelayCommand<object>(VerifyPasswordChanged);
			RegisterCommand = new RelayCommand(async () => await Register(), CanRegister);

			// todo: implement INotifyDataErrorInfo
		}

        #region Properties

        private string _username;
		private string _email;
		private string _password;
		private string _verifyPassword;
		private string _serverError;
		private string _nickname;
		private string _passwordMatchError;
		private bool _registrationSuccessful;

		/// <summary>
        ///		Username input for the form.
        /// </summary>
		public string Username
		{
            get => _username;
			set => Set(ref _username, value, nameof(Username));
		}

		/// <summary>
        ///		Nickname input for the form.
        /// </summary>
		public string Nickname
		{
            get => _nickname;
			set => Set(ref _nickname, value, nameof(Nickname));
		}
		
		/// <summary>
        ///		Email input for the form.
        /// </summary>
		public string Email
		{
            get => _email;
			set => Set(ref _email, value, nameof(Email));
		}
		
		/// <summary>
        ///		Password input for the form.
        /// </summary>
		public string Password
		{
            get => _password;
			set => Set(ref _password, value, nameof(Password));
		}

		/// <summary>
        ///		Verify password input for the form.
        /// </summary>
		public string VerifyPassword
		{
            get => _verifyPassword;
			set => Set(ref _verifyPassword, value, nameof(VerifyPassword));
		}

		/// <summary>
        ///		Server error.
        /// </summary>
		public string ServerError
		{
            get => _serverError;
			set => Set(ref _serverError, value, nameof(ServerError));
		}

		/// <summary>
        ///		Error to display when password's don't match.
        /// </summary>
		public string PasswordMatchError
		{
            get => _passwordMatchError;
			set => Set(ref _passwordMatchError, value, nameof(PasswordMatchError));
		}

		/// <summary>
        ///		Indicates if the registration was successful.
        /// </summary>
		public bool RegistrationSuccessful
		{
            get => _registrationSuccessful;
			set => Set(ref _registrationSuccessful, value, nameof(RegistrationSuccessful));
		}
		
        #endregion

        #region Commands

		/// <summary>
        ///		Command to go back to the login page.
        /// </summary>
		public RelayCommand GoBackCommand { get; }

		/// <summary>
        ///		Command for when the password changes.
        /// </summary>
		public RelayCommand<object> PasswordChangedCommand { get; }

		/// <summary>
        ///		Command for when the verify password changes.
        /// </summary>
		public RelayCommand<object> VerifyPasswordChangedCommand { get; }

		/// <summary>
        ///		Command to register the user.
        /// </summary>
		public RelayCommand RegisterCommand { get; }

		/// <summary>
        ///		Goes back to the login page.
        /// </summary>
		private void GoBack()
		{
			MessengerInstance.Send(new NotificationMessage<string>("GoBack", "GoBack"));
        }

		/// <summary>
        ///		Sets the password property.
        /// </summary>
        /// <param name="parameter"> The password box. </param>
		private void PasswordChanged(object parameter)
		{
            var passwordBox = (PasswordBox)parameter;
			Password = passwordBox.Password;
		}

		/// <summary>
        ///		Sets the verify password property.
        /// </summary>
        /// <param name="parameter"> The verify password box. </param>
		private void VerifyPasswordChanged(object parameter)
		{
			var passwordBox = (PasswordBox)parameter;
			VerifyPassword = passwordBox.Password;
		}

		/// <summary>
        ///		Registers the user.
        /// </summary>
		private async Task Register()
		{
            var user = new User()
            {
				Username = Username,
                NickName = Nickname,
				Email = Email,
			};
			var model = new RegisterModel()
			{
				User = user,
				Password = Password
			};

			try
			{
				// call data service to register user
				RegistrationSuccessful = _userAccountService.RegisterUser(model);

				if (!RegistrationSuccessful)
					ServerError = "Unable to register user.";
                else
                {
					// show the registration successful message for two seconds
                    await Task.Delay(2000);
					// go back to login page
					GoBack();
                }
			}
			catch (Exception)
			{
				ServerError = "Something went wrong during your request, please try again in a moment.";
			}
		}

		/// <summary>
        ///		Determines if the register command can be fired.
        /// </summary>
        /// <returns></returns>
		private bool CanRegister()
		{
			var passwordMatch = Password == VerifyPassword;

            if (!passwordMatch)
                PasswordMatchError = "Passwords do not match.";
            else
                PasswordMatchError = string.Empty;

			return passwordMatch &&
				   !string.IsNullOrEmpty(Username) && 
				   !string.IsNullOrEmpty(Password) && 
				   !string.IsNullOrEmpty(VerifyPassword) && 
				   !string.IsNullOrEmpty(Email);
		}

        #endregion
    }
}
