using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.ViewModel
{
    /// <summary>
    /// <para>ViewModel used in <see cref="ImageWindow"/></para>
    /// <para>Should never be created as a singleton instance as multiple windows of type <see cref="ImageWindow"/> may be created and each of them shall contain unique properties</para>
    /// </summary>
    public class ImageWindowViewModel
    {
        public ImageSource ImageSource { get; set; }

        public RelayCommand<Window> CloseCommand { get; set; }

        public ImageWindowViewModel()
        {
            CloseCommand = new RelayCommand<Window>(OnClose);
        }

        private void OnClose(Window window)
        {
            window.Close();
        }
    }
}
