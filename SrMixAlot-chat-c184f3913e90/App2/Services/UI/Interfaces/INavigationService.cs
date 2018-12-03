using System;

namespace Chat.Services.UI.Interfaces
{
    /// <summary>
    ///     Interface for a navigation service.
    /// </summary>
    public interface INavigationService
	{
		/// <summary>
		///     Navigate to a source page.
		/// </summary>
		/// <param name="sourcePage"> Page type to navigate to. </param>
		void Navigate(Type sourcePage);

		/// <summary>
		///     Navigate to a source page with an additional parameter.
		/// </summary>
		/// <param name="sourcePage"> The page type to navigate to. </param>
		/// <param name="parameter"> Parameter to send with navigation. </param>
		void Navigate(Type sourcePage, object parameter);

		/// <summary>
		///     Go back to the last page.
		/// </summary>
		void GoBack();
	}
}
