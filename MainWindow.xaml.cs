﻿using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using ImageProcessingLibrary;

namespace Image_Processing_application
{
    public partial class MainWindow : Window
    {
        string fileName;
        string fileNameForCovertedImage;

        public MainWindow()
        {
            InitializeComponent();
            displayTimer.Text = "00:00:00.0000";
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.png;*.bmp|" +
              "JPG (*.jpg)|*.jpg" +
              "PNG (*.png)|*.png" +
              "BMP (*.bmp)|*.bmp";
            if (op.ShowDialog() == true)
            {
                displayImage.Source = new BitmapImage(new Uri(op.FileName));
                fileName = op.FileName;
            }
        }

        private void Convert_Button_Click(object sender, RoutedEventArgs e)
        {
            fileNameForCovertedImage = FileNameForConvertedImage();
            ImageProcess();
            displayConvertedImage.Source = new BitmapImage(new Uri(fileNameForCovertedImage));
        }

        private void ImageProcess()
        {
            ImageProcessing processing = new ImageProcessing();
            Image image = processing.OpenImage(fileName);
            DateTime start = DateTime.Now;
            Image convertedImage = processing.ToMainColors(image);
            DateTime end = DateTime.Now;
            processing.SaveImage(convertedImage, fileNameForCovertedImage);
            Timer(start, end);
        }

        private string FileNameForConvertedImage()
        {
            int fileExtensionIndex = fileName.LastIndexOf(".");
            return fileName.Substring(0, fileExtensionIndex) + "_converted" + fileName.Substring(fileExtensionIndex, fileName.Length - fileExtensionIndex);
        }

        private void Timer(DateTime start, DateTime end)
        {
            displayTimer.Text = String.Format("{0:hh}:{0:mm}:{0:ss}.{0:ffff}", (end - start));
        }
    }
}
