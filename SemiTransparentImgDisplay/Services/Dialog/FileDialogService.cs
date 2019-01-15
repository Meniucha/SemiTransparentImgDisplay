using System;
using Microsoft.Win32;

namespace SemiTransparentImgDisplay.Services.Dialog
{
    /// <inheritdoc />
    public class FileDialogService : IFileDialogService
    {
        /// <inheritdoc />
        public string[] ShowImageSelectionDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.CheckFileExists = true;
            //Properties.Settings
            dialog.Filter = Properties.Settings.Default.ImageFileFormats;

            return dialog.ShowDialog() ?? false ? dialog.FileNames : Array.Empty<string>();
        }
    }
}
