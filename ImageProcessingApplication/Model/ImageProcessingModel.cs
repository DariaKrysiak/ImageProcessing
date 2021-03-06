﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using ImageProcessingLibrary;

namespace ImageProcessingApplication.Model
{
    public class ImageProcessingModel
    {
        public TimeSpan durationTimeSpan;

        private string PrepareFileNameForConvertedImage(string fileName)
        {
            int ExtensionIndex = fileName.LastIndexOf(".");
            return fileName.Substring(0, ExtensionIndex) + "_converted" + fileName.Substring(ExtensionIndex, fileName.Length - ExtensionIndex);
        }

        public string ProcessImage(string fileName, bool isAsync)
        {
            string fileNameForCovertedImage = this.PrepareFileNameForConvertedImage(fileName);
            ImageProcessing processing = new ImageProcessing();
            Image image = processing.OpenImage(fileName);
            DateTime startTime = DateTime.Now;
            Image convertedImage = isAsync 
                ? processing.ToMainColorsAsync(image) 
                : processing.ToMainColors(image);
            durationTimeSpan = DateTime.Now - startTime;
            processing.SaveImage(convertedImage, fileNameForCovertedImage);
            return fileNameForCovertedImage;
        }
    }
}
