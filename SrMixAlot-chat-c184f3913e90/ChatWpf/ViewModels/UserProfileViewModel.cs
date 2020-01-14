using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Shared.Models.Dto;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ChatWpf.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserProfileViewModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;

            SetProfileImageCommand = new RelayCommand(SetImage);
            SaveProfileCommand = new RelayCommand(SaveProfile);
            GoBackCommand = new RelayCommand(GoBack);
        }

        #region Properties

        private bool _windowIsVisible;

        public bool WindowIsVisible
        {
            get => _windowIsVisible;
            set => Set(ref _windowIsVisible, value, nameof(WindowIsVisible));
        }

        public string NickName
        {
            get => UserInstance.Current.NickName;
            set => UserInstance.Current.NickName = value;
        }

        public string ProfileImageData
        {
            get => UserInstance.Current.ProfileImageData;
            set => UserInstance.Current.ProfileImageData = value;
        }

        #endregion

        #region Commands

        /// <summary>
        ///     Command to set the profile image data.
        /// </summary>
        public RelayCommand SetProfileImageCommand { get; }

        /// <summary>
        ///     Command to save the profile image for the user.
        /// </summary>
        public RelayCommand SaveProfileCommand { get; }

        public RelayCommand GoBackCommand { get; }

        /// <summary>
        ///		Goes back to the login page.
        /// </summary>
        private void GoBack()
        {
            WindowIsVisible = false;
        }

        /// <summary>
        ///     Set the profile image data.  Opens a file picker and converts the selected image to base64.
        /// </summary>
        private void SetImage()
        {
            // open file picker
            var filePath = string.Empty;
            var fileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            
            if (fileDialog.ShowDialog() == true)
                filePath = fileDialog.FileName;

            if(filePath != string.Empty)
            {
                // get base64 image
                var bytes = File.ReadAllBytes(filePath);
                var downScaledBytes = DownscaleImage(bytes);

                ProfileImageData = Convert.ToBase64String(downScaledBytes);
                RaisePropertyChanged(ProfileImageData);
            }
        }

        public byte[] DownscaleImage(byte[] imageBytes)
        {
            var myMemStream = new MemoryStream(imageBytes);
            
            var fullSizeImg = Image.FromStream(myMemStream);
            var newImage = fullSizeImg.GetThumbnailImage(64, 64, null, IntPtr.Zero);
            var myResult = new MemoryStream();

            newImage.Save(myResult, ImageFormat.Png);  //Or whatever format you want.
            
            return myResult.ToArray();
        }

        /// <summary>
        ///     Calls the api to save the profile image data.
        /// </summary>
        private void SaveProfile()
        {
            // send api call
            var userDto = new UpdateUserDto()
            {
                NickName = NickName,
                ProfileImageData = ProfileImageData
            };

            var success = _userAccountService.UpdateUser(userDto);
        }

        #endregion
    }
}
