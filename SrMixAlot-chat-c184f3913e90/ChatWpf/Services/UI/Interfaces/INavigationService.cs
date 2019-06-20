using System.Windows.Controls;

namespace ChatWpf.Services.UI.Interfaces
{
    public interface INavigationService
	{
		/// <summary>
		///		Navigate to the specified uri.
		/// </summary>
		/// <param name="uri"> The uri to navigate to. </param>
        void NavigateToUri(string uri);

		/// <summary>
		///		Navigate to the specified page.
		/// </summary>
		/// <param name="page"> The page to navigate to. </param>
        void NavigateToPage(Page page);

		/// <summary>
        ///		Navigate to the last page.
        /// </summary>
		void GoBack();

		/// <summary>
		///		Sets the navigation from for navigation.s
		/// </summary>
		/// <param name="frame"> The navigation frame to set. </param>
        void SetNavigationFrame(Frame frame);
	}
}
