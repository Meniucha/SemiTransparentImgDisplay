using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using SemiTransparentImgDisplay.ViewModel;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.Services.Image
{
    /// <inheritdoc />
    public class ImageDisplayService : IImageDisplayService
    {
        /// <inheritdoc />
        public ObservableCollection<IDisplayable> Displayables { get; set; }

        /// <inheritdoc />
        public IDisplayable Create(string path)
        {
            ImageWindow window = new ImageWindow();
            ImageWindowViewModel vm = new ImageWindowViewModel();
            vm.ImageSource = new BitmapImage(new Uri(path));
            window.DataContext = vm;
            
            
            IDisplayable displayable = new Displayable(window);
            Displayables.Add(displayable);

            return displayable;
        }

        public void CloseAll()
        {
            foreach (IDisplayable displayable in Displayables)
            {
                displayable.Close();
            }
            Displayables.Clear();
        }

        /// <inheritdoc />
        public ImageDisplayService()
        {
            Displayables = new ObservableCollection<IDisplayable>();
        }
    }
}
