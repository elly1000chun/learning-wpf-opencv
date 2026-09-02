using System;
using System.Windows.Media;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace learning_wpf_opencv.Services;

public sealed class OpenCvImageLoaderService : IImageLoaderService
{
    public ImageSource LoadImage(string filePath)
    {
        using var image = Cv2.ImRead(filePath, ImreadModes.Color);

        if (image.Empty())
        {
            throw new InvalidOperationException("The selected image file could not be opened.");
        }

        var imageSource = image.ToBitmapSource();
        imageSource.Freeze();

        return imageSource;
    }
}
