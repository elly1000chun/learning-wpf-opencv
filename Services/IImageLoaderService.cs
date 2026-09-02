using System.Windows.Media;

namespace learning_wpf_opencv.Services;

public interface IImageLoaderService
{
    ImageSource LoadImage(string filePath);
}
