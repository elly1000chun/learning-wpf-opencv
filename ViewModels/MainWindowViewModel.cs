using System;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using learning_wpf_opencv.Services;

namespace learning_wpf_opencv.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    private readonly IFileDialogService fileDialogService;
    private readonly IImageLoaderService imageLoaderService;
    private string openedFilePath = string.Empty;
    private ImageSource? openedImage;

    public MainWindowViewModel(
        IFileDialogService fileDialogService,
        IImageLoaderService imageLoaderService)
    {
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

    public IRelayCommand OpenFileCommand { get; }

    public IRelayCommand SmoothCommand { get; }

    private void OpenFile()
    {
        var filePath = fileDialogService.ShowOpenImageFileDialog();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            return;
        }

        try
        {
            OpenedFilePath = filePath;
            OpenedImage = imageLoaderService.LoadImage(filePath);
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

    private static void Smooth()
    {
    }
}
