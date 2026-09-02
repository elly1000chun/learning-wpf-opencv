using Microsoft.Win32;

namespace learning_wpf_opencv.Services;

public sealed class FileDialogService : IFileDialogService
{
    public string? ShowOpenImageFileDialog()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Open Image File",
            Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|Bitmap Files (*.bmp)|*.bmp",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
