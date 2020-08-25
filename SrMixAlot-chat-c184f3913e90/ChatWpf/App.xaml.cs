using AutoMapper;
using ChatWpf.Models;
using ChatWpf.ViewModels;
using GalaSoft.MvvmLight.Threading;
using Shared.Models.Dto;
using System.Collections.ObjectModel;
using System.Windows;

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

				cfg.CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
				cfg.CreateMap<ChatRoom, ChatRoomDto>().ReverseMap();
				cfg.CreateMap<UserDto, User>().ReverseMap();
			});
        }
    }
}
