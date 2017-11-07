using System;
using System.Windows.Controls;

namespace OneDriveAspergerTest {
    internal class Drawing_Controller {
        private readonly Settings_Entity settings;

        internal Drawing_Controller(Settings_Entity settings) {
            this.settings = settings;
        }

        internal void UpdateBrushSize(InkCanvas canvas, int amount) {
            settings.brushSizeX += amount;
            settings.brushSizeY += amount;

            canvas.DefaultDrawingAttributes.Width = settings.brushSizeX;
            canvas.DefaultDrawingAttributes.Height = settings.brushSizeY;
        }

        internal void UpdateOpacity(string amount) {
            settings.opacity = (byte) ((Double.Parse(amount) - 0) / (1 - 0));
        }
    }
}
