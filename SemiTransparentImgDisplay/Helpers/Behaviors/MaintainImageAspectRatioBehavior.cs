using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using SemiTransparentImgDisplay.Helpers.Windows;
using SemiTransparentImgDisplay.Windows;

namespace SemiTransparentImgDisplay.Helpers.Behaviors
{
    public class MaintainImageAspectRatioBehavior : Behavior<ImageWindow>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SizeChanged += AssociatedObjectSizeChangedMaintainAspectRatio;
            AssociatedObject.Loaded += AssociatedObjectLoadedMaintainAspectRatio;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= AssociatedObjectSizeChangedMaintainAspectRatio;
            AssociatedObject.Loaded -= AssociatedObjectLoadedMaintainAspectRatio;
        }


        /// <summary>
        /// Maintains the aspect ratio of <see cref="ImageWindow.Image"/>
        /// <para></para>
        /// This method should be used as a handler only for the Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectLoadedMaintainAspectRatio(object sender, EventArgs e)
        {
            if (sender is ImageWindow window)
            {
                double aspectRatio = window.Image.Source.Width / window.Image.Source.Height;
                double newWidth = aspectRatio * window.Height;

                window.Width = newWidth;
            }
        }

        /// <summary>
        /// Maintains the aspect ratio of <see cref="ImageWindow.Image"/>
        /// <para></para>
        /// Using <see cref="WindowsService.ShowWindowContentWhileDraggingIsEnabled"/> determines the approach in keeping the aspect ratio in order to minimize flickering while resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectSizeChangedMaintainAspectRatio(object sender, SizeChangedEventArgs e)
        {
            if (sender is ImageWindow window)
            {
                double aspectRatio = window.Image.Source.Width / window.Image.Source.Height;
                if (WindowsService.ShowWindowContentWhileDraggingIsEnabled())
                {
                    window.Height = e.NewSize.Height;
                    window.Width = e.NewSize.Height * aspectRatio;
                }
                else
                {
                    double percentWidthChange = Math.Abs(e.NewSize.Width - e.PreviousSize.Width) / e.PreviousSize.Width;
                    double percentHeightChange = Math.Abs(e.NewSize.Height - e.PreviousSize.Height) / e.PreviousSize.Height;

                    if (percentWidthChange > percentHeightChange)
                    {
                        window.Height = e.NewSize.Width / aspectRatio;
                    }
                    else
                    {
                        window.Width = e.NewSize.Height * aspectRatio;
                    }
                }

                
            }
        }
    }
}
