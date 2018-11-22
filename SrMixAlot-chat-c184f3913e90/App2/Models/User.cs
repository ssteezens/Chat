using GalaSoft.MvvmLight;

namespace Chat.Models
{
    public class User : ObservableObject
    {
        /// <summary>
        ///     Nickname for the user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The file path to user's profile photo
        /// </summary>
        public string ImageFilePath { get; set; }
    }
}
