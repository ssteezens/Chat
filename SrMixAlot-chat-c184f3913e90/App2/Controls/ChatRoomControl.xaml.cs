using Chat.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Chat.Controls
{
    public sealed partial class ChatRoomControl : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", 
                typeof(ChatRoomViewModel), typeof(ChatRoomControl), null);

        public ChatRoomViewModel ViewModel
        {
            get => (ChatRoomViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public ChatRoomControl()
        {
            this.InitializeComponent();
        }
    }
}
