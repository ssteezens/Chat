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

			SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
			SimpleIoc.Default.Register<IChatMessageDataService, ChatMessageDataService>();
			SimpleIoc.Default.Register<IChatRoomDataService, ChatRoomDataService>();
			SimpleIoc.Default.Register<INavigationService, NavigationService>();
			SimpleIoc.Default.Register<IQueueService, QueueService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<ChatRoomViewModel>();
            SimpleIoc.Default.Register<AddChatRoomViewModel>();
        }

		/// <summary>
        ///		Main view model.
        /// </summary>
        public MainViewModel MainVm => ServiceLocator.Current.GetInstance<MainViewModel>();

		/// <summary>
        ///		Login view model.
        /// </summary>
		public LoginViewModel LoginVm => ServiceLocator.Current.GetInstance<LoginViewModel>();

		public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}