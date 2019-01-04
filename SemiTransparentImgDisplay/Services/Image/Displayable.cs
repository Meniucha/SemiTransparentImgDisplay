using SemiTransparentImgDisplay.Windows;
using System.Windows;
using System.Windows.Media;

namespace SemiTransparentImgDisplay.Services.Image
{
    public class Displayable : IDisplayable
    {
        private readonly ImageWindow _window;

        /// <inheritdoc />
        public object DataContext
        {
            get => _window.DataContext;
            set => _window.DataContext = value;
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

        public Displayable(ImageSource imageSource)
        {
            _window = new ImageWindow();
            _window.Image.Source = imageSource;
        }
    }
}
