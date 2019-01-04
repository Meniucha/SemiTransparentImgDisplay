using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemiTransparentImgDisplay.Services.Image;

namespace SemiTransparentImgDisplay.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IImageDisplayService _imageDisplayService;

        /// <summary>
        /// Creates and displays an image using the <see cref="IImageDisplayService"/>
        /// </summary>
        public RelayCommand OpenImageCommand { get; set; }

        /// <summary>
        /// Closes all <see cref="IDisplayable"/>s contained within <see cref="IImageDisplayService.Displayables"/>
        /// </summary>
        public RelayCommand CloseDisplayedImageCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IImageDisplayService imageDisplayService)
        {
            OpenImageCommand = new RelayCommand(OnOpenImage);
            CloseDisplayedImageCommand = new RelayCommand(OnCloseDisplayedImages);
            this._imageDisplayService = imageDisplayService;
        }

        /// <summary>
        /// Handler for the <see cref="CloseDisplayedImageCommand"/>
        /// </summary>
        private void OnCloseDisplayedImages()
        {
            _imageDisplayService.CloseAll();
        }

        /// <summary>
        /// Handler for the <see cref="OpenImageCommand"/>
        /// </summary>
        private void OnOpenImage()
        {
            _imageDisplayService.Create(@"C:\Users\menid\OneDrive\Pulpit\zbiorniczki.png").Display();
        }


    }
}