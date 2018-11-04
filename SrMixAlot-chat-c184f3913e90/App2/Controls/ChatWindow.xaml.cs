using Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Chat.Controls
{
    public sealed partial class ChatWindow : UserControl
    {
        public static readonly DependencyProperty ChatEntriesProperty = 
            DependencyProperty.Register(
                "ChatEntries", 
                typeof(ChatWindow), 
                typeof(List<ChatEntryViewModel>), 
                new PropertyMetadata(null)
            );
        
        public ChatWindow()
        {
            this.InitializeComponent();
        }

        public List<ChatEntryViewModel> ChatEntries
        {
            get => (List<ChatEntryViewModel>)GetValue(ChatEntriesProperty);
            set => SetValue(ChatEntriesProperty, value);
        }
    }
}
