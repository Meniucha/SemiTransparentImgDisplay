using System.Collections.ObjectModel;

namespace SemiTransparentImgDisplay.Services.Image
{
    /// <summary>
    /// Creates and manages numerous <see cref="IDisplayable"/>s
    /// </summary>
    public interface IImageDisplayService
    {
        /// <summary>
        /// Collection containing all the created <see cref="IDisplayable"/>s
        /// </summary>
        ObservableCollection<IDisplayable> Displayables { get; set; }

        /// <summary>
        /// Creates a new <see cref="IDisplayable"/> and stores it in <see cref="Displayables"/>
        /// </summary>
        /// <param name="path">Path to a image</param>
        /// <returns>Newly created <see cref="IDisplayable"/></returns>
        IDisplayable Create(string path);

        /// <summary>
        /// Closes and removes all <see cref="IDisplayable"/>s from the <see cref="Displayables"/> collection
        /// </summary>
        void CloseAll();
    }
}
