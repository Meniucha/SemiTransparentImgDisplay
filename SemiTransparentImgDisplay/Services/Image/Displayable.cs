using System.Windows;

namespace SemiTransparentImgDisplay.Services.Image
{
    public class Displayable : IDisplayable
    {
        private readonly Window _window;

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

        public Displayable(Window window)
        {
            this._window = window;
        }
    }
}
