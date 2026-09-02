using System.Windows;
using learning_wpf_opencv.Services;
using learning_wpf_opencv.ViewModels;

namespace learning_wpf_opencv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(
                new FileDialogService(),
                new OpenCvImageLoaderService(),
                new OcvImageProcessingService());
        }
    }
}
