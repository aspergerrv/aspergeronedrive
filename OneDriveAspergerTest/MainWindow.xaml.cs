using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OneDriveAspergerTest {
    public partial class MainWindow : Window {
        private readonly Settings_Entity settings;
        private readonly OneDrive_Controller oneDriveController;
        private readonly Drawing_Controller drawingController;
        private readonly DriveCanvas_Model driveCanvasModel;

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            WindowState = WindowState.Maximized;

            oneDriveController = new OneDrive_Controller(new Authentification_Entity());
            settings = new Settings_Entity(new byte[] { 21, 21, 21 }, 10, 10);
            drawingController = new Drawing_Controller(settings);
            driveCanvasModel = new DriveCanvas_Model();
        }

        private void UpdateCanvas(object sender, MouseEventArgs e) {
            DriveCanvas.DefaultDrawingAttributes.Color = Color.FromArgb(
                settings.opacity, 
                settings.color.Color.R, 
                settings.color.Color.G, 
                settings.color.Color.B
            );

            DriveCanvas.DefaultDrawingAttributes.Width = settings.brushSizeX;
            DriveCanvas.DefaultDrawingAttributes.Height = settings.brushSizeY;
        }

        private void ShowColorGrid(object sender, RoutedEventArgs e) {
            ColorGrid.Visibility = Visibility.Visible;
        }

        private void Erase(object sender, RoutedEventArgs e) {
            Button eraseButton = (Button) sender;

            if (DriveCanvas.EditingMode != InkCanvasEditingMode.EraseByStroke) {
                DriveCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                eraseButton.Background = new SolidColorBrush(Colors.Purple);

            } else {
                DriveCanvas.EditingMode = InkCanvasEditingMode.Ink;
                eraseButton.Background = new SolidColorBrush(Colors.Black);
            }
        }
        private void ChangeColor(object sender, RoutedEventArgs e) {
            settings.color.Color = ((SolidColorBrush) ((Button)sender).Background).Color;
            ColorGrid.Visibility = Visibility.Collapsed;
        }

        private void ChangeBrushSize(object sender, RoutedEventArgs e) {
            if (sender is TextBox) {
                Regex regex = new Regex("[0-9][0-9]*");

                if (regex.IsMatch(BrushSizeX.Text)) {
                    settings.brushSizeX = Double.Parse(BrushSizeX.Text);

                } else if (regex.IsMatch(BrushSizeY.Text)) {
                    settings.brushSizeY = Double.Parse(BrushSizeY.Text);
                }
            } else {
                if (((KeyEventArgs)e).Key == Key.Add) {
                    drawingController.UpdateBrushSize(DriveCanvas, 2);

                } else if (((KeyEventArgs)e).Key == Key.Subtract) {
                    drawingController.UpdateBrushSize(DriveCanvas, -2);
                }
            }
        }

        private void OpenSaveSettings(object sender, RoutedEventArgs e) {
            SaveSettings.Visibility = Visibility.Visible;
        }

        private async void Save(object sender, RoutedEventArgs e) {
            FileStream image = driveCanvasModel.CreatePNG(DriveCanvas, settings.resolution);

            await oneDriveController.SaveSketch(image);
        }

        private void UpdateOpacity(object sender, RoutedEventArgs e) {
            drawingController.UpdateOpacity(((TextBox) sender).Text);
        }

        private void OpenBrushSettings(object sender, RoutedEventArgs e) {
            BrushSettings.Visibility = Visibility.Visible;
        }

        private void UpdateResolution(object sender, RoutedEventArgs e) {
            settings.resolution = int.Parse(((TextBox) sender).Text);
        }
    }
}
