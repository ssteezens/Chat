using System.Threading.Tasks;

namespace ChatWpf.ViewModels
{
	/// <summary>
    ///		Base model for a ViewModel.
    /// </summary>
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
	{
		private bool _isBusy;

		/// <summary>
        ///		Gets or sets whether the view model is busy.
        /// </summary>
		public bool IsBusy
		{
            get => _isBusy;
			set => Set(ref _isBusy, value, nameof(IsBusy));
		}

		/// <summary>
        ///		Virtual method for loading data asynchronously.
        /// </summary>
		public virtual Task LoadData()
		{
			return Task.CompletedTask;
		}
	}
}
