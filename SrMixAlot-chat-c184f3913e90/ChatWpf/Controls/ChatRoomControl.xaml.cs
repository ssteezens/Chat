using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatWpf.ViewModels;

namespace ChatWpf.Controls
{
    /// <summary>
    /// Interaction logic for ChatRoomControl.xaml
    /// </summary>
    public partial class ChatRoomControl : UserControl
    {
        public ChatRoomControl()
        {
            InitializeComponent();

            // todo: refactor into control
            var window = Application.Current.Windows[0];

            ThePopup.PlacementTarget = window;

            window.LocationChanged += (sender, args) =>
            {
                var offset = ThePopup.HorizontalOffset;
                ThePopup.HorizontalOffset = offset + 1;
                ThePopup.HorizontalOffset = offset;
            };

            window.SizeChanged += (sender, args) =>
            {
                var offset = ThePopup.HorizontalOffset;
                ThePopup.HorizontalOffset = offset + 1;
                ThePopup.HorizontalOffset = offset;

                ThePopup.Width = window.Width;
                ThePopup.Height = window.Height;
            };
        }

		public ChatRoomViewModel Vm { get; set; }
    }
}
