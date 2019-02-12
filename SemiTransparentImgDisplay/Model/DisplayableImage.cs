using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using SemiTransparentImgDisplay.Helpers.Windows;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.Model
{
    /// <inheritdoc cref="IDisplayableImage" />
    /// INotifyPropertyChanged is currently implemented only for clickable and isMaximized
    public class DisplayableImage : ObservableObject, IDisplayableImage
    {
        private readonly ImageWindow _window;
        private bool _clickable;
        private bool _isMaximized;

        public static DisplayableImage Empty => new DisplayableImage("");

        /// <inheritdoc />
        public Window DisplayHandler => _window;

        /// <inheritdoc />
        public double PosX
        {
            get => _window.Left;
            set => _window.Left = value;
        }

        /// <inheritdoc />
        public double PosY
        {
            get => _window.Top;
            set => _window.Top = value;
        }

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

                Set(() => Clickable, ref _clickable, value);
            }
        }

        /// <inheritdoc />
        public bool StayOnTop
        {
            get => _window.Topmost;
            set => _window.Topmost = value;
        }

        public bool IsMaximized
        {
            get => _isMaximized;
            set
            {
                _window.WindowState = value ? WindowState.Maximized : WindowState.Normal;
                Set(() => IsMaximized, ref _isMaximized, value);
            }
        }

        /// <inheritdoc />
        public string Path { get; set; }

        /// <inheritdoc />
        public double Width
        {
            get => _window.Width;
            set => _window.Width = value;
        }

        /// <inheritdoc />
        public double Height
        {
            get => _window.Height;
            set => _window.Height = value;
        }

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
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                _window.Image.Source = new BitmapImage(new Uri(path));
            }
            Path = path;
            _clickable = !WindowsService.IsExTransparent(_window);

            _window.Closed += (s, e) => Closed?.Invoke(this, e);

            //Ensures that the WindowExTransparent is set correctly after the window is displayed
            _window.Loaded += (s, e) => Clickable = Clickable;
        }

        [JsonConstructor]
        public DisplayableImage(double opacity, double posX, double posY, bool clickable, bool stayOnTop, string path, double height, double width) : this(path)
        {
            Opacity = opacity;
            Clickable = clickable;
            StayOnTop = stayOnTop;
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
        }
    }
}
