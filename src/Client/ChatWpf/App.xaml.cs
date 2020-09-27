using AutoMapper;
using ChatWpf.Models;
using ChatWpf.ViewModels;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Windows;
using Shared.Models.Models;

namespace ChatWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public App()
		{
			DispatcherHelper.Initialize();
			
			SetupMapping();
		}

		/// <summary>
        ///		Setup auto mapper.
        /// </summary>
		private void SetupMapping()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<ChatRoom, ChatRoomViewModel>()
					.ForMember(dest => dest.ChatMessages, opt => opt.MapFrom(src => new ObservableCollection<ChatMessage>(src.ChatMessages)))
					.ForMember(dest => dest.Users, opt => opt.MapFrom(src => new ObservableCollection<User>(src.Users)))
                    .ForMember(dest => dest.ChatRoomModel, opt => opt.MapFrom(src => src));

				cfg.CreateMap<ChatMessage, ChatMessageModel>().ReverseMap();
                cfg.CreateMap<UserModel, User>().ReverseMap();
                cfg.CreateMap<ChatRoom, ChatRoomModel>().ReverseMap();
                cfg.CreateMap<UserModel, User>().ReverseMap();
                cfg.CreateMap<UserRoomModel, User>();
            });
        }
    }
}
