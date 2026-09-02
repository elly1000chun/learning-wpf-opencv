using OpenCvSharp;
using OpenCvSharp.DnnSuperres;
using OpenCvSharp.WpfExtensions;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace learning_wpf_opencv.Services;

public sealed class OcvImageProcessingService : IImageProcessingService
{
    public ImageSource Smooth(ImageSource input, int sigma)
    {
        // 2. 가우시안 블러 (Kernel 크기 3x3, 시그마X = 0)
        var dstGaussian = new Mat();
        Cv2.GaussianBlur(ImageSourceToMat(input), dstGaussian, new OpenCvSharp.Size(sigma, sigma), 0);

        return dstGaussian.ToBitmapSource();
    }

    public ImageSource ApplySuperResolution(ImageSource input)
    {
        // 다운로드한 AI 모델 경로
        string modelPath = Path.Combine(
                AppContext.BaseDirectory,
                "resources",
                "models",
                "EDSR_x4.pb");
        string modelName = "edsr";       // 모델 이름 ("edsr", "espcn", "fsrcnn", "lapsrn")
        int scale = 4;                   // 확대 배율

        // 2. 슈퍼 해상도 객체 생성 및 설정
        using var sr = new DnnSuperResImpl();
        sr.ReadModel(modelPath);
        sr.SetModel(modelName, scale);

        // 3. 업스케일링 수행
        using Mat result = new Mat();
        sr.Upsample(ImageSourceToMat(input), result);

        return result.ToBitmapSource();
    }

    public static Mat ImageSourceToMat(ImageSource imageSource)
    {
        // 1. Cast ImageSource to BitmapSource
        if (!(imageSource is BitmapSource bitmapSource))
        {
            throw new ArgumentException("ImageSource must be a BitmapSource");
        }

        // 2. Initialize a Mat with the corresponding dimensions
        // Most WPF images use BGR or BGRA format (8 bits per channel)
        int width = bitmapSource.PixelWidth;
        int height = bitmapSource.PixelHeight;
        int channels = bitmapSource.Format.BitsPerPixel / 8;

        MatType matType = channels == 4 ? MatType.CV_8UC4 : MatType.CV_8UC3;
        Mat mat = new Mat(height, width, matType);

        // 3. Copy the pixel byte data into the Mat memory allocation
        int stride = width * channels;
        bitmapSource.CopyPixels(Int32Rect.Empty, mat.Data, stride * height, stride);

        return mat;
    }
}
