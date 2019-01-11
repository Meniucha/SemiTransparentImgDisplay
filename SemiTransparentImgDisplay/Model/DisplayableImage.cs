using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SemiTransparentImgDisplay.Annotations;
using SemiTransparentImgDisplay.Helpers.Windows;
using SemiTransparentImgDisplay.Services.Image;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.Model
{
    /// <inheritdoc />
    public class DisplayableImage : IDisplayableImage
    {
        private readonly ImageWindow _window;
        private bool _clickable;

        /// <inheritdoc />
        public Window DisplayHandler => _window;

        /// <inheritdoc />
        public double Opacity
        {
            get => _window.Opacity;
            set => _window.Opacity = value;
        }

        /// <inheritdoc />
        public bool Clickable
        {
            get => _clickable;
            set
            {
                if (value)
                {
                    WindowsService.SetWindowExNonTransparent(_window);
                }
                else
                {
                    WindowsService.SetWindowExTransparent(_window);
                }
                _clickable = value;
            }
        }

        /// <inheritdoc />
        public bool StayOnTop
        {
            get => _window.Topmost;
            set => _window.Topmost = value;
        }

        /// <inheritdoc />
        public string Path { get; set; }

        /// <inheritdoc />
        public void Display()
        {
            _window.Show();
        }

        /// <inheritdoc />
        public void Close()
        {
            _window.Close();
        }

        /// <inheritdoc />
        public event EventHandler Closed;

        public DisplayableImage(string path)
        {
            _window = new ImageWindow();
            _window.Image.Source = new BitmapImage(new Uri(path));
            Path = path;
            _clickable = !WindowsService.IsExTransparent(_window);

            _window.Closed += (s, e) => Closed?.Invoke(this, e);
        }

    }
}
