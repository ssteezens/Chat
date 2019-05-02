using ChatWpf.Services.UI.Interfaces;
using System;
using System.Windows.Controls;

namespace ChatWpf.Services.UI
{
    public class NavigationService : INavigationService
    {
		/// <summary>
        ///		Navigate to the specified uri.
        /// </summary>
        /// <param name="uri"> The uri to navigate to. </param>
		public void NavigateToUri(string uri)
		{
			NavigationFrame.Navigate(new Uri(uri, UriKind.Relative));
		}

		/// <summary>
        ///		Navigate to the specified page.
        /// </summary>
        /// <param name="page"> The page to navigate to. </param>
		public void NavigateToPage(Page page)
		{
			NavigationFrame.NavigationService.Navigate(page);
		}

		/// <summary>
        ///		Sets the navigation from for navigation.s
        /// </summary>
        /// <param name="frame"> The navigation frame to set. </param>
		public void SetNavigationFrame(Frame frame)
		{
            NavigationFrame = frame;
		}

		/// <summary>
        ///		Frame to facilitate navigation.
        /// </summary>
		Frame NavigationFrame { get; set; }
	}
}
