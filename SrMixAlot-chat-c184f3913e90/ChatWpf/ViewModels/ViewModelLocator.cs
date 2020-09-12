/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ChatWpf"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using ChatWpf.Services.Connection;
using ChatWpf.Services.Connection.Interfaces;
using ChatWpf.Services.Data;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.UI;
using ChatWpf.Services.UI.Interfaces;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using RabbitMQ.Client;
using ServiceStack;
using System.Configuration;
using System.Net;

namespace ChatWpf.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var baseUrl = ConfigurationManager.AppSettings["api_base_url"];
			var jsonClient = new JsonServiceClient(baseUrl);

			// TODO: refine this, maybe filter on sender
			// TODO: wire this up using middleware
			ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var connectionFactory = new ConnectionFactory()
            {
                HostName = ConfigurationManager.AppSettings["json_listener_host_name"],
                UserName = ConfigurationManager.AppSettings["json_listener_username"],
                Password = ConfigurationManager.AppSettings["json_listener_password"]
            };

            SimpleIoc.Default.Register<IConnectionFactory>(() => connectionFactory);
            SimpleIoc.Default.Register<IJsonServiceClient>(() => jsonClient);
			SimpleIoc.Default.Register<IUserAccountService, UserAccountService>();
			SimpleIoc.Default.Register<IChatMessageDataService, ChatMessageDataService>();
			SimpleIoc.Default.Register<IChatRoomDataService, ChatRoomDataService>();
			SimpleIoc.Default.Register<INavigationService, NavigationService>();
			SimpleIoc.Default.Register<IQueueService, QueueService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<ChatRoomViewModel>();
            SimpleIoc.Default.Register<AddChatRoomViewModel>();
            SimpleIoc.Default.Register<UserProfileViewModel>();
            SimpleIoc.Default.Register<AddUserViewModel>();
        }

		/// <summary>
        ///		Main view model.
        /// </summary>
        public MainViewModel MainVm => ServiceLocator.Current.GetInstance<MainViewModel>();

		/// <summary>
        ///		Login view model.
        /// </summary>
		public LoginViewModel LoginVm => ServiceLocator.Current.GetInstance<LoginViewModel>();

		/// <summary>
        ///		Register view model.
        /// </summary>
		public RegisterViewModel RegisterVm => ServiceLocator.Current.GetInstance<RegisterViewModel>();

        /// <summary>
        ///     User profile view model.
        /// </summary>
        public UserProfileViewModel UserProfileVm => ServiceLocator.Current.GetInstance<UserProfileViewModel>();

        /// <summary>
        ///     Add user view model.
        /// </summary>
        public AddUserViewModel AddUserVm => ServiceLocator.Current.GetInstance<AddUserViewModel>();

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}