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
            IDisplayable displayable = new Displayable(new BitmapImage(new Uri(path)));
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
