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
