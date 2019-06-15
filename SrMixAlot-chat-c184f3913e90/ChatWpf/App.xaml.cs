using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using ChatWpf.Models;
using ChatWpf.ViewModels;
using ChatWpf.Views;
using GalaSoft.MvvmLight.Threading;

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

			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<ChatRoom, ChatRoomViewModel>()
					.ForMember(dest => dest.ChatMessages, opt => opt.MapFrom(src => new ObservableCollection<ChatMessage>(src.ChatMessages)))
					.ForMember(dest => dest.Users, opt => opt.MapFrom(src => new ObservableCollection<User>(src.Users)));

				cfg.CreateMap<ChatMessage, ChatMessage>().ReverseMap();
			});
        }
    }
}
