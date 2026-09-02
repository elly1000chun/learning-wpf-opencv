using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using learning_wpf_opencv.Services;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace learning_wpf_opencv.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    private readonly IFileDialogService fileDialogService;
    private readonly IImageLoaderService imageLoaderService;
    private string openedFilePath = string.Empty;
    private ImageSource? openedImage;
    private ImageSource? displayingImage;

    public MainWindowViewModel(
        IFileDialogService fileDialogService,
        IImageLoaderService imageLoaderService)
    {
        Debug.WriteLine(MethodBase.GetCurrentMethod());

        this.fileDialogService = fileDialogService;
        this.imageLoaderService = imageLoaderService;

        OpenFileCommand = new RelayCommand(OpenFile);
        SmoothCommand = new RelayCommand(Smooth);
    }

    public string OpenedFilePath
    {
        get => openedFilePath;
        private set => SetProperty(ref openedFilePath, value);
    }

    public ImageSource? OpenedImage
    {
        get => openedImage;
        private set => SetProperty(ref openedImage, value);
    }
    public ImageSource? DisplayingImage
    {
        get => displayingImage;
        private set => SetProperty(ref displayingImage, value);
    }

    public IRelayCommand OpenFileCommand { get; }

    public IRelayCommand SmoothCommand { get; }

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
            DisplayingImage = OpenedImage;
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
        var ips = new OcvImageProcessingService();

        if (OpenedImage != null) {
            DisplayingImage = ips.Smooth(OpenedImage, 9);
        }
    }
}
