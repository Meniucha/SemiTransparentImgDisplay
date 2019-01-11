using System;
using System.Windows;
using System.Windows.Media;

namespace SemiTransparentImgDisplay.Model
{
    /// <summary>
    /// Object that can be displayed and closed
    /// </summary>
    public interface IDisplayableImage
    {
        /// <summary>
        /// Rendering object of the <see cref="IDisplayableImage"/>
        /// </summary>
        Window DisplayHandler { get; }

        /// <summary>
        /// Opacity of the <see cref="IDisplayableImage"/>
        /// </summary>
        /// <returns></returns>
        double Opacity { get; set; }

        /// <summary>
        /// Defines if the <see cref="IDisplayableImage"/> is clickable
        /// </summary>
        bool Clickable { get; set; }

        /// <summary>
        /// Defines if the <see cref="IDisplayableImage"/> should stay on top
        /// </summary>
        bool StayOnTop { get; set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Displays the <see cref="IDisplayableImage"/>
        /// </summary>
        void Display();

        /// <summary>
        /// Closes the <see cref="IDisplayableImage"/>
        /// </summary>
        void Close();

        /// <summary>
        /// Raised when the close method is called
        /// <para></para>
        /// Sender is this instance of the <see cref="IDisplayableImage"/>
        /// </summary>
        event EventHandler Closed;
    }
}
