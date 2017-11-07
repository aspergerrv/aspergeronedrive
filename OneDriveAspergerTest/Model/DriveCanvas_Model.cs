using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OneDriveAspergerTest {
    internal class DriveCanvas_Model {
        internal FileStream CreatePNG(InkCanvas canvas, int resolution) {
            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            int resolutionScaleFactor = resolution / 96;

            RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap(
                    resolutionScaleFactor * ((int)size.Width + 1),
                    resolutionScaleFactor * ((int)size.Height + 1),
                    resolutionScaleFactor * 96d,
                    resolutionScaleFactor * 96d,
                    PixelFormats.Default
                );

            renderBitmap.Render(canvas);

            FileStream outStream = new FileStream(@"tester", FileMode.Create);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            encoder.Save(outStream);

            outStream.Position = 0;
            canvas.LayoutTransform = transform;

            return outStream;
        }
    }
}
