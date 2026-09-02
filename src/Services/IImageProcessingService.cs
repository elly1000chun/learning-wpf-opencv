using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace learning_wpf_opencv.Services;
public interface IImageProcessingService
{
    ImageSource Smooth(ImageSource input, int sigma);
}
