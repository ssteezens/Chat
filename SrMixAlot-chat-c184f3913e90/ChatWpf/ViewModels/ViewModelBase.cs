namespace ChatWpf.ViewModels
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        private bool _isBusy;

        /// <summary>
        ///     Gets or sets whether the view model is busy.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value, nameof(IsBusy));
        }
    }
}
