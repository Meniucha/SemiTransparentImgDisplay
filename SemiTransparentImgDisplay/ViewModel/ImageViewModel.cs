using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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

        public RelayCommand<ImageWindow> MaintainAspectRatioCommand { get; set; }

        public ImageViewModel()
        {
            CloseCommand = new RelayCommand<Window>(OnClose);
            MaintainAspectRatioCommand = new RelayCommand<ImageWindow>(OnMaintainAspectRatio);
        }

        /// <summary>
        /// Handler for <see cref="CloseCommand"/>
        /// </summary>
        /// <param name="window"></param>
        private void OnClose(Window window)
        {
            window.Close();
        }

        private void OnMaintainAspectRatio(ImageWindow window)
        {
            double aspectRatio = window.Image.Source.Width / window.Image.Source.Height;

            window.Width = aspectRatio * window.Height;
        }
    }
}
