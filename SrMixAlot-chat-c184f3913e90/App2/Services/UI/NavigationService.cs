using Chat.Services.UI.Interfaces;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Chat.Services.UI
{
	/// <summary>
    ///		Service for navigation between xaml views.
    /// </summary>
    public class NavigationService : INavigationService
    {
		/// <summary>
		///     Navigate to a source page.
		/// </summary>
		/// <param name="sourcePage"> Page type to navigate to. </param>
		public void Navigate(Type sourcePage)
		{
			var frame = (Frame)Window.Current.Content;

			frame.Navigate(sourcePage);
		}

		/// <summary>
		///     Navigate to a source page with an additional parameter.
		/// </summary>
		/// <param name="sourcePage"> The page type to navigate to. </param>
		/// <param name="parameter"> Parameter to send with navigation. </param>
		public void Navigate(Type sourcePage, object parameter)
		{
			var frame = (Frame)Window.Current.Content;

			frame.Navigate(sourcePage, parameter);
		}

		/// <summary>
		///     Go back to the last page.
		/// </summary>s
		public void GoBack()
		{
			var frame = (Frame)Window.Current.Content;

			frame.GoBack();
		}
    }
}
