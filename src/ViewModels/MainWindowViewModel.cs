using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using learning_wpf_opencv.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace learning_wpf_opencv.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    private readonly IFileDialogService fileDialogService;
    private readonly IImageLoaderService imageLoaderService;
    private readonly IImageProcessingService imageProcessingService;
    private string openedFilePath = string.Empty;
    private ImageSource? openedImage;
    private ImageSource? displayingImage;
    private bool isProcessing;

    public MainWindowViewModel(
        IFileDialogService fileDialogService,
        IImageLoaderService imageLoaderService,
        IImageProcessingService imageProcessingService)
    {
        Debug.WriteLine(MethodBase.GetCurrentMethod());

        this.fileDialogService = fileDialogService;
        this.imageLoaderService = imageLoaderService;
        this.imageProcessingService = imageProcessingService;

        OpenFileCommand = new RelayCommand(OpenFile, () => IsNotProcessing);
        SmoothCommand = new RelayCommand(Smooth, () => IsNotProcessing && OpenedImage != null);
        SuperResolutionCommand = new AsyncRelayCommand(SuperResolutionAsync, () => IsNotProcessing && OpenedImage != null);
    }

    public string OpenedFilePath
    {
        get => openedFilePath;
        private set => SetProperty(ref openedFilePath, value);
    }

    public ImageSource? OpenedImage
    {
        get => openedImage;
        private set
        {
            if (SetProperty(ref openedImage, value))
            {
                SmoothCommand.NotifyCanExecuteChanged();
                SuperResolutionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public ImageSource? DisplayingImage
    {
        get => displayingImage;
        private set => SetProperty(ref displayingImage, value);
    }

    public bool IsProcessing
    {
        get => isProcessing;
        private set
        {
            if (SetProperty(ref isProcessing, value))
            {
                OnPropertyChanged(nameof(IsNotProcessing));
                OpenFileCommand.NotifyCanExecuteChanged();
                SmoothCommand.NotifyCanExecuteChanged();
                SuperResolutionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool IsNotProcessing => !IsProcessing;

    public IRelayCommand OpenFileCommand { get; }

    public IRelayCommand SmoothCommand { get; }

    public IAsyncRelayCommand SuperResolutionCommand { get; }

    private void OpenFile()
    {
        Debug.WriteLine(MethodBase.GetCurrentMethod());

        var filePath = fileDialogService.ShowOpenImageFileDialog();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            return;
        }

        try
        {
            OpenedFilePath = filePath;
            OpenedImage = imageLoaderService.LoadImage(filePath);
            DisplayingImage = null;
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                exception.Message,
                "Image Open Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void Smooth()
    {
        Debug.WriteLine(MethodBase.GetCurrentMethod());

        if (OpenedImage != null)
        {
            DisplayingImage = imageProcessingService.Smooth(OpenedImage, 9);
        }
    }

    private async Task SuperResolutionAsync()
    {
        Debug.WriteLine(MethodBase.GetCurrentMethod());

        if (OpenedImage == null)
        {
            return;
        }

        try
        {
            IsProcessing = true;
            DisplayingImage = await Task.Run(() => imageProcessingService.ApplySuperResolution(OpenedImage));
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                exception.Message,
                "Super Resolution Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        finally
        {
            IsProcessing = false;
        }
    }
}
