using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageProcessingLibrary;
using Microsoft.Win32;

namespace Image_Processing_application.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private OpenFileDialog dialog;
        private BitmapImage originalImage;
        private BitmapImage convertedImage;
        private string durationTime;
        private string fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadImage
        {
            get { return new RelayCommand(LoadImageAction); }
        }

        public ICommand ConvertImage
        {
            get { return new RelayCommand(ConvertImageAction); }
        }

        public BitmapImage OriginalImage
        {
            get { return this.originalImage; }
            set
            {
                this.originalImage = value;
                this.OnPropertyChanged(nameof(this.OriginalImage));
            }
        }

        public BitmapImage ConvertedImage
        {
            get { return this.convertedImage; }
            set
            {
                this.convertedImage = value;
                this.OnPropertyChanged(nameof(this.ConvertedImage));
            }
        }

        public string DurationTime
        {
            get { return this.durationTime; }
            set
            {
                this.durationTime = value;
                this.OnPropertyChanged(nameof(this.DurationTime));
            }
        }

        public MainWindowViewModel(OpenFileDialog dialog)
        {
            this.DurationTime = "00:00:00.0000";
            this.dialog = dialog;
        }

        private void LoadImageAction()
        {
            if (dialog.ShowDialog() == true)
            {
                this.OriginalImage = new BitmapImage(new Uri(dialog.FileName));
                this.fileName = dialog.FileName;
            }
        }

        private void ConvertImageAction()
        {
            string fileNameForCovertedImage = PrepareFileNameForConvertedImage();
            ProcessImage(fileNameForCovertedImage);
            this.ConvertedImage = new BitmapImage(new Uri(fileNameForCovertedImage));
        }

        private string PrepareFileNameForConvertedImage()
        {
            int ExtensionIndex = this.fileName.LastIndexOf(".");
            return this.fileName.Substring(0, ExtensionIndex) + "_converted" + this.fileName.Substring(ExtensionIndex, this.fileName.Length - ExtensionIndex);
        }

        private void ProcessImage(string fileNameForCovertedImage)
        {
            ImageProcessing processing = new ImageProcessing();
            Image image = processing.OpenImage(this.fileName);
            DateTime startTime = DateTime.Now;
            Image convertedImage = processing.ToMainColors(image);
            DateTime endTime = DateTime.Now;
            processing.SaveImage(convertedImage, fileNameForCovertedImage);
            DisplayDurationTime(startTime, endTime);
        }

        private void DisplayDurationTime(DateTime startTime, DateTime endTime)
        {
            this.DurationTime = string.Format("{0:hh}:{0:mm}:{0:ss}.{0:ffff}", (endTime - startTime));
        }

        public void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
