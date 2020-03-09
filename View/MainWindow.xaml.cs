using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using ImageProcessingLibrary;

namespace Image_Processing_application.View
{
    public partial class MainWindow : Window
    {
        private string fileName;

        public MainWindow()
        {
            InitializeComponent();
            durationTime.Text = "00:00:00.0000";
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select a picture";
            dialog.Filter = "All supported graphics|*.jpg;*.png;*.bmp|" +
              "JPG (*.jpg)|*.jpg" +
              "PNG (*.png)|*.png" +
              "BMP (*.bmp)|*.bmp";
            if (dialog.ShowDialog() == true)
            {
                originalImage.Source = new BitmapImage(new Uri(dialog.FileName));
                fileName = dialog.FileName;
            }
        }

        private void Convert_Button_Click(object sender, RoutedEventArgs e)
        {
            string fileNameForCovertedImage = PrepareFileNameForConvertedImage();
            ProcessImage(fileNameForCovertedImage);
            convertedImage.Source = new BitmapImage(new Uri(fileNameForCovertedImage));
        }

        private void ProcessImage(string fileNameForCovertedImage)
        {
            ImageProcessing processing = new ImageProcessing();
            Image image = processing.OpenImage(fileName);
            DateTime startTime = DateTime.Now;
            Image convertedImage = processing.ToMainColors(image);
            DateTime endTime = DateTime.Now;
            processing.SaveImage(convertedImage, fileNameForCovertedImage);
            DisplayDurationTime(startTime, endTime);
        }

        private string PrepareFileNameForConvertedImage()
        {
            int ExtensionIndex = fileName.LastIndexOf(".");
            return fileName.Substring(0, ExtensionIndex) + "_converted" + fileName.Substring(ExtensionIndex, fileName.Length - ExtensionIndex);
        }

        private void DisplayDurationTime(DateTime startTime, DateTime endTime)
        {
            durationTime.Text = string.Format("{0:hh}:{0:mm}:{0:ss}.{0:ffff}", (endTime - startTime));
        }
    }
}
