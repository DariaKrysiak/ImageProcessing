using System;
using System.Drawing;
using ImageProcessingLibrary;

namespace Image_Processing_application.Model
{
    class ImageProcessingModel
    {
        public TimeSpan durationTimeSpan;

        private string PrepareFileNameForConvertedImage(string fileName)
        {
            int ExtensionIndex = fileName.LastIndexOf(".");
            return fileName.Substring(0, ExtensionIndex) + "_converted" + fileName.Substring(ExtensionIndex, fileName.Length - ExtensionIndex);
        }

        public string ProcessImage(string fileName)
        {
            string fileNameForCovertedImage = this.PrepareFileNameForConvertedImage(fileName);
            ImageProcessing processing = new ImageProcessing();
            Image image = processing.OpenImage(fileName);
            DateTime startTime = DateTime.Now;
            Image convertedImage = processing.ToMainColors(image);
            durationTimeSpan = DateTime.Now - startTime;
            processing.SaveImage(convertedImage, fileNameForCovertedImage);
            return fileNameForCovertedImage;
        }
    }
}
