using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
        private readonly IFileDialogService _fileDialogService;
        private readonly ISerializer _serializer;
        private readonly DispatcherTimer _timer;

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
        /// Creates and displays a <see cref="IDisplayableImage"/> if the data provided in <see cref="DragEventArgs"/> is an image file/files
        /// </summary>
        public RelayCommand<DragEventArgs> DisplayableImageFromPathCommand { get; set; }

        /// <summary>
        /// Saves <see cref="GridView"/>s column order and width
        /// </summary>
        public RelayCommand<GridView> SaveImageGridViewColumnsCommand { get; set; }

        /// <summary>
        /// Loads <see cref="GridView"/>s column order and width
        /// </summary>
        public RelayCommand<GridView> LoadImageGridViewColumnsCommand { get; set; }

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
            DisplayableImageFromPathCommand = new RelayCommand<DragEventArgs>(OnDisplayableImageFromPath);
            SaveImageGridViewColumnsCommand = new RelayCommand<GridView>(OnSaveImageGridViewColumns);
            LoadImageGridViewColumnsCommand = new RelayCommand<GridView>(OnLoadImageGridViewColumns);
            this._displayService = displayService;
            this._fileDialogService = fileDialogService;
            this._serializer = serializer;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(Properties.Settings.Default.AutoSaveInterval);
            _timer.Tick += (s, e) => OnSaveCurrentImages();
            _timer.Start();
        }

        /// <summary>
        /// Handler for the <see cref="LoadImageGridViewColumnsCommand"/>
        /// </summary>
        /// <param name="grid"></param>
        private void OnLoadImageGridViewColumns(GridView grid)
        {
            GridViewColumnCollectionModel columnsModel =
                _serializer.Deserialize<GridViewColumnCollectionModel>(Properties.Settings.Default.GridViewColumns);

            if (columnsModel != null && !columnsModel.Any(cm => double.IsNaN(cm.Width)))
            {
                //sets column width and order, Reverse() called to clone because the original IEnumerable is modified inside the foreach loop
                foreach (var gridViewColumn in grid.Columns.Reverse())
                {
                    var correspondingModel = columnsModel.First(cm => cm.Header.Equals(gridViewColumn.Header));
                    gridViewColumn.Width = correspondingModel.Width;
                    grid.Columns.Move(grid.Columns.IndexOf(gridViewColumn), columnsModel.IndexOf(correspondingModel));
                }
            }
        }

        /// <summary>
        /// Handler for the <see cref="SaveImageGridViewColumnsCommand"/>
        /// </summary>
        /// <param name="grid"></param>
        private void OnSaveImageGridViewColumns(GridView grid)
        {
            GridViewColumnCollectionModel columns = new GridViewColumnCollectionModel(grid.Columns);
            Properties.Settings.Default.GridViewColumns = _serializer.Serialize(columns);
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// handler for the <see cref="DisplayableImageFromPathCommand"/>
        /// </summary>
        /// <param name="e"></param>
        private void OnDisplayableImageFromPath(DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data is string[] paths)
            {
                foreach (var path in paths)
                {
                    if (IsValidImagePath(path))
                    {
                        _displayService.CreateAndAdd(path).Display();
                    }
                }
            }


            //checks if the provided path is a path to an image
            bool IsValidImagePath(string path)
            {
                return Path.HasExtension(path) &&
                       Properties.Settings.Default.ImageFileFormats.IndexOf(Path.GetExtension(path), StringComparison.InvariantCultureIgnoreCase) >= 0;
            }
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
            var images = _fileDialogService.ShowImageSelectionDialog();

            foreach (var image in images)
            {
                _displayService.CreateAndAdd(image).Display();
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
                    if (File.Exists(displayableImage.Path))
                    {
                        _displayService.Add(displayableImage).Display();
                    }
                    else
                    {
                        _displayService.Displayables.Remove(displayableImage);
                    }
                }
            }
        }



    }
}