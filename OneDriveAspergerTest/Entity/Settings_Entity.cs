using System.Windows.Media;

namespace OneDriveAspergerTest {
    internal class Settings_Entity {
        internal SolidColorBrush color;
        internal double brushSizeX;
        internal double brushSizeY;
        internal byte opacity;
        internal int resolution;

        internal Settings_Entity(byte[] rgb, double brushSizeX, double brushSizeY) {
            color = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));

            this.brushSizeX = brushSizeX;
            this.brushSizeY = brushSizeY;

            opacity = 255;
            resolution = 600;
        }
    }
}
