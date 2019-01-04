namespace SemiTransparentImgDisplay.Services.Image
{
    /// <summary>
    /// Object that can be displayed and closed
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// Data context of the object
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Displays the <see cref="IDisplayable"/>
        /// </summary>
        void Display();

        /// <summary>
        /// Closes the <see cref="IDisplayable"/>
        /// </summary>
        void Close();

        double Opacity();
    }
}
