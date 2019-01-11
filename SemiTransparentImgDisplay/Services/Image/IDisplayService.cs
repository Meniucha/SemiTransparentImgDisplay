using System;
using System.Collections.ObjectModel;
using SemiTransparentImgDisplay.Model;

namespace SemiTransparentImgDisplay.Services.Image
{
    /// <summary>
    /// Creates and manages numerous <see cref="IDisplayableImage"/>s
    /// </summary>
    public interface IDisplayService
    {
        /// <summary>
        /// Collection containing the created <see cref="IDisplayableImage"/>s
        /// </summary>
        ObservableCollection<IDisplayableImage> Displayables { get; }

        /// <summary>
        /// Creates a new <see cref="IDisplayableImage"/> and stores it in <see cref="Displayables"/>
        /// </summary>
        /// <param name="path">Path to a image</param>
        /// <returns>Newly created <see cref="IDisplayableImage"/></returns>
        IDisplayableImage Create(string path);

        /// <summary>
        /// Closes and removes all <see cref="IDisplayableImage"/>s from the <see cref="Displayables"/> collection
        /// </summary>
        void RemoveAndCloseAll();
    }
}
