using System.Threading.Tasks;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///     Base class for a view model.
    /// </summary>
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
	{
		private bool _isBusy;

        /// <summary>
        ///     Gets or sets whether this view model is busy.
        /// </summary>
		public bool IsBusy
		{
            get => _isBusy;
			set => Set(ref _isBusy, value, nameof(IsBusy));
		}

        public virtual Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }
	}
}
