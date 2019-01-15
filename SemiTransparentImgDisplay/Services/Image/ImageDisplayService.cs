using System;
using System.Collections.ObjectModel;
using System.Linq;
using SemiTransparentImgDisplay.Model;

namespace SemiTransparentImgDisplay.Services.Image
{
    /// <inheritdoc />
    public class ImageDisplayService : IDisplayService
    {
        /// <inheritdoc />
        public ObservableCollection<IDisplayableImage> Displayables { get; set; }

        /// <inheritdoc />
        public IDisplayableImage Create(string path)
        {    
            IDisplayableImage displayableImage = new DisplayableImage(path);
            return this.Add(displayableImage);
        }

        public IDisplayableImage Add(IDisplayableImage image)
        {
            Displayables.Add(image);
            image.Closed += OnImageClosed;
            return image;
        }

        /// <inheritdoc />
        public void RemoveAndCloseAll()
        {
            //Reverse() is called to make the Close and OnImageClosed methods not invalidate the Displayables enumerator
            foreach (IDisplayableImage displayable in Displayables.Reverse())
            {
                displayable.Close();
            }
            Displayables.Clear();
        }

        /// <inheritdoc />
        public ImageDisplayService()
        {
            Displayables = new ObservableCollection<IDisplayableImage>();
        }

        public void OnImageClosed(object s, EventArgs e)
        {
            Displayables.Remove(s as IDisplayableImage);
        }
    }
}
