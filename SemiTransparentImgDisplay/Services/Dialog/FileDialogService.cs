using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            dialog.Filter = Properties.Settings.Default["ImageFileFormats"].ToString();

            return dialog.ShowDialog() ?? false ? dialog.FileNames : Array.Empty<string>();
        }
    }
}
