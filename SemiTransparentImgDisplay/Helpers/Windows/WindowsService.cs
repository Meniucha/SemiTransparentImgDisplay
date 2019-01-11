using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace SemiTransparentImgDisplay.Helpers.Windows
{
    public static class WindowsService
    {
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static bool IsExTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            return (GetWindowLong(wih.Handle, GWL_EXSTYLE) & WS_EX_TRANSPARENT) == WS_EX_TRANSPARENT;
        }

        public static void SetWindowExTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            var extendedStyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            SetWindowLong(wih.Handle, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        public static void SetWindowExNonTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            var extendedStyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            SetWindowLong(wih.Handle, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
        }
    }
}
