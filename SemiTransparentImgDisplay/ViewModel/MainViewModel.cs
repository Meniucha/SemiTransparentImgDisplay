using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using SemiTransparentImgDisplay.Model;
using SemiTransparentImgDisplay.Services.Dialog;
using SemiTransparentImgDisplay.Services.Image;
using SemiTransparentImgDisplay.Services.Serialization;

namespace SemiTransparentImgDisplay.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IDisplayService _displayService;
        private readonly IFileDialogService fileDialogService;
        private readonly ISerializer _serializer;

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
        /// Saves the currently open <see cref="IDisplayableImage"/>s
        /// </summary>
        public RelayCommand SaveCurrentImagesCommand { get; set; }

        /// <summary>
        /// Loads the stored <see cref="IDisplayableImage"/>s
        /// </summary>
        public RelayCommand LoadStoredImagesCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDisplayService displayService,
                             IFileDialogService fileDialogService,
                             ISerializer serializer)
        {
            OpenImageCommand = new RelayCommand(OnOpenImage);
            CloseAllImagesCommand = new RelayCommand(OnCloseAllImages);
            CloseImageCommand = new RelayCommand<IDisplayableImage>(OnCloseImage);
            SaveCurrentImagesCommand = new RelayCommand(OnSaveCurrentImages);
            LoadStoredImagesCommand = new RelayCommand(OnLoadStoredImages);
            this._displayService = displayService;
            this.fileDialogService = fileDialogService;
            this._serializer = serializer;
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

        /// <summary>
        /// Handler for the <see cref="SaveCurrentImagesCommand"/>
        /// </summary>
        private void OnSaveCurrentImages()
        {
            Properties.Settings.Default.CurrentImages = _serializer.Serialize(Displayables);
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handler for the <see cref="LoadStoredImagesCommand"/>
        /// </summary>
        private void OnLoadStoredImages()
        {
            var images = _serializer.Deserialize<ObservableCollection<IDisplayableImage>>(Properties.Settings.Default.CurrentImages);

            //Foreach to not mess up references between displayService.Displayables and this.Displayables
            
            if (images != null)
            {
                foreach (var displayableImage in images)
                {
                    _displayService.Add(displayableImage).Display();
                }
            }
        }



    }
}