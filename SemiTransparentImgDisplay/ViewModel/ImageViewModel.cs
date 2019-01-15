using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemiTransparentImgDisplay.Model;
using SemiTransparentImgDisplay.Services.Image;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.ViewModel
{
    /// <summary>
    /// <para>ViewModel used in <see cref="ImageWindow"/></para>
    /// </summary>
    public class ImageViewModel : ViewModelBase
    {
        /// <summary>
        /// Closes the passed window and removes the <see cref="IDisplayableImage"/> corresponding to passed window, from the <see cref="IDisplayService.Displayables"/>
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; }

        public ImageViewModel()
        {
            CloseCommand = new RelayCommand<Window>(OnClose);
        }

        /// <summary>
        /// Handler for <see cref="CloseCommand"/>
        /// </summary>
        /// <param name="window"></param>
        private void OnClose(Window window)
        {
            window.Close();
        }   
    }
}
