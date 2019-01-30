using System.Runtime.Remoting.Contexts;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemiTransparentImgDisplay.Helpers.Windows;
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
        private readonly IDisplayService _displayService;

        /// <summary>
        /// Closes the passed window and removes the <see cref="IDisplayableImage"/> corresponding to passed window, from the <see cref="IDisplayService.Displayables"/>
        /// </summary>
        public RelayCommand<Window> CloseCommand { get; set; }

        /// <summary>
        /// Toggles the maximized state of the passed Window
        /// </summary>
        public RelayCommand<Window> ToggleMaximizeWindowCommand { get; set; }

        /// <summary>
        /// Sets the passed window to extransparent
        /// </summary>
        public RelayCommand<Window> SetExTransparentCommand { get; set; }

        public ImageViewModel(IDisplayService displayService)
        {
            _displayService = displayService;
            CloseCommand = new RelayCommand<Window>(OnClose);
            ToggleMaximizeWindowCommand = new RelayCommand<Window>(OnToggleMaximizeWindow);
            SetExTransparentCommand = new RelayCommand<Window>(OnSetExTransparent);
        }


        /// <summary>
        /// Handler for the <see cref="SetExTransparentCommand"/>
        /// </summary>
        /// <param name="window"></param>
        private void OnSetExTransparent(Window window)
        {
            _displayService.GetDisplayableImageByDisplayHandlerReference(window).Clickable = false;
        }

        /// <summary>
        /// Handler for the <see cref="ToggleMaximizeWindowCommand"/>
        /// </summary>
        /// <param name="window"></param>
        private void OnToggleMaximizeWindow(Window window)
        {
            var displayable = _displayService.GetDisplayableImageByDisplayHandlerReference(window);

            if (displayable != null)
            {
                displayable.IsMaximized = !displayable.IsMaximized;
            }
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
