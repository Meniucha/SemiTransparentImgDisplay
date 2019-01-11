using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemiTransparentImgDisplay.Model;
using SemiTransparentImgDisplay.Services.Dialog;
using SemiTransparentImgDisplay.Services.Image;
using IDialogService = GalaSoft.MvvmLight.Views.IDialogService;

namespace SemiTransparentImgDisplay.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private readonly IDisplayService _displayService;
        private readonly IFileDialogService fileDialogService;

        /// <summary>
        /// Property for binding purposes
        /// </summary>
        public ObservableCollection<IDisplayableImage> Displayables
        {
            get => _displayService.Displayables;
        }

        /// <summary>
        /// Creates and displays an image using the <see cref="IDisplayService"/>
        /// </summary>
        public RelayCommand OpenImageCommand { get; set; }

        /// <summary>
        /// Closes all <see cref="IDisplayableImage"/>s contained within <see cref="IDisplayService.Displayables"/>
        /// </summary>
        public RelayCommand CloseAllImagesCommand { get; set; }

        /// <summary>
        /// Closes the passed <see cref="IDisplayableImage"/>
        /// </summary>
        public RelayCommand<IDisplayableImage> CloseImageCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDisplayService displayService,
                             IFileDialogService fileDialogService)
        {
            OpenImageCommand = new RelayCommand(OnOpenImage);
            CloseAllImagesCommand = new RelayCommand(OnCloseAllImages);
            CloseImageCommand = new RelayCommand<IDisplayableImage>(OnCloseImage);
            this._displayService = displayService;
            this.fileDialogService = fileDialogService;
        }

        /// <summary>
        /// Handler for the <see cref="CloseImageCommand"/>
        /// </summary>
        /// <param name="img"></param>
        private void OnCloseImage(IDisplayableImage img)
        {
            img.Close();
        }

        /// <summary>
        /// Handler for the <see cref="CloseAllImagesCommand"/>
        /// </summary>
        private void OnCloseAllImages()
        {
            _displayService.RemoveAndCloseAll();
        }

        /// <summary>
        /// Handler for the <see cref="OpenImageCommand"/>
        /// </summary>
        private void OnOpenImage()
        {
            var images = fileDialogService.ShowImageSelectionDialog();

            foreach (var image in images)
            {
                _displayService.Create(image).Display();
            }
        }



        

        
    }
}