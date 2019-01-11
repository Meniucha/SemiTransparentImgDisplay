using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using SemiTransparentImgDisplay.Model;
using SemiTransparentImgDisplay.ViewModel;
using SemiTransparentImgDisplay.Windows;

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
            Displayables.Add(displayableImage);
            displayableImage.Closed += OnImageClosed;
            return displayableImage;
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
