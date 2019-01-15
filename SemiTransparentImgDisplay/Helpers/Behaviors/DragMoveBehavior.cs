using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SemiTransparentImgDisplay.Helpers.Behaviors
{
    public class DragMoveBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObjectDragMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectDragMove;
        }

        private void AssociatedObjectDragMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is Window window)
            {
                window.DragMove();
            }
        }
    }
}
