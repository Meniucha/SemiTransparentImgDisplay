using System;
using System.Windows;
using Newtonsoft.Json;

namespace SemiTransparentImgDisplay.Model
{
    /// <summary>
    /// Object that can be displayed and closed
    /// TODO Simplify dependencies by making ImageWindow inherit from IDisplayableImage
    /// TODO    -----------------------------------------------------------------
    /// </summary>
    public interface IDisplayableImage
    {
        /// <summary>
        /// Rendering object of the <see cref="IDisplayableImage"/>
        /// </summary>
        [JsonIgnore]
        Window DisplayHandler { get; }

        /// <summary>
        /// Horizontal position of the <see cref="IDisplayableImage"/>
        /// </summary>
        double PosX { get; set; }

        /// <summary>
        /// Vertical position of the <see cref="IDisplayableImage"/>
        /// </summary>
        double PosY { get; set; }

        /// <summary>
        /// Width of the <see cref="IDisplayableImage"/>
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Height of the <see cref="IDisplayableImage"/>
        /// </summary>
        double Height { get; set; }

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
        /// Defines if the <see cref="IDisplayableImage"/> is maximized
        /// </summary>
        bool IsMaximized { get; set; }

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
