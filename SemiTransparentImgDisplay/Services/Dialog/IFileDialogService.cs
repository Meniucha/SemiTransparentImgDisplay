using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiTransparentImgDisplay.Services.Dialog
{
    public interface IFileDialogService
    {
        /// <summary>
        /// Shows a Image selection dialog and returns selected images
        /// </summary>
        /// <returns>Selected images</returns>
        string[] ShowImageSelectionDialog();
    }
}
