using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
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

        private void AssociatedObjectLoadedMaintainAspectRatio(object sender, EventArgs e)
        {
            if (sender is ImageWindow window)
            {
                double aspectRatio = window.Image.Source.Width / window.Image.Source.Height;
                double newWidth = aspectRatio * window.Height;

                window.Width = newWidth;
            }
        }

        private void AssociatedObjectSizeChangedMaintainAspectRatio(object sender, SizeChangedEventArgs e)
        {
            if (sender is ImageWindow window)
            {
                double aspectRatio = window.Image.Source.Width / window.Image.Source.Height;

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
