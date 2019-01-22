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
        private const int SPI_GETDRAGFULLWINDOWS = 0x0026;

        /// <summary>
        /// Retrieves the value specified by <paramref name="index"/> from <paramref name="hwnd"/>
        /// </summary>
        /// <param name="hwnd">Handle to a window</param>
        /// <param name="index">
        /// The zero-based offset to the value to be retrieved
        /// <para>Visit https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowlonga for more info</para>
        /// </param>
        /// <returns>Requested value</returns>
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        /// <summary>
        /// Sets the value in <paramref name="hwnd"/> specified by <paramref name="index"/> to <paramref name="newStyle"/>
        /// </summary>
        /// <param name="hwnd">Handle to a window</param>
        /// <param name="index">
        /// The zero-based offset to the value to be retrieved
        /// <para>Visit https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowlonga for more info</para>
        /// </param>
        /// <returns>Previous value</returns>
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters.
        /// <para>
        /// Visit https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-systemparametersinfoa for more info
        /// </para>
        /// </summary>
        /// <returns>True if succeeded</returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern bool GetParametersInfo(int uiAction, int uiParam, out bool pvParam, int fWinIni);

        /// <summary>
        /// Checks if the window is transparent (in terms of mouse input)
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static bool IsExTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            return (GetWindowLong(wih.Handle, GWL_EXSTYLE) & WS_EX_TRANSPARENT) == WS_EX_TRANSPARENT;
        }

        /// <summary>
        /// Sets the window to be transparent (in terms of mouse input)
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowExTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            var extendedStyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            SetWindowLong(wih.Handle, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        /// <summary>
        /// Sets the window to not be transparent (in terms of mouse input)
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowExNonTransparent(Window window)
        {
            var wih = new WindowInteropHelper(window);
            var extendedStyle = GetWindowLong(wih.Handle, GWL_EXSTYLE);
            SetWindowLong(wih.Handle, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
        }

        /// <summary>
        /// Retrieves the 'Show window visual content while dragging' from the windows user settings
        /// </summary>
        /// <returns>Indicates if 'Show window visual content while dragging' is enabled</returns>
        public static bool ShowWindowContentWhileDraggingIsEnabled()
        {
            if (GetParametersInfo(SPI_GETDRAGFULLWINDOWS, 0, out bool b, 0))
            {
                return b;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
