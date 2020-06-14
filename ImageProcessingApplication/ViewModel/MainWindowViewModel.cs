using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageProcessingApplication.Model;
using Microsoft.Win32;

namespace ImageProcessingApplication.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private OpenFileDialog dialog;
        private BitmapImage originalImage;
        private BitmapImage convertedImage;
        private string durationTime;
        private string fileName;
        private bool isAsync = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadImage
        {
            get { return new RelayCommand(LoadImageAction); }
        }

        public ICommand ConvertImage
        {
            get { return new RelayCommand(ConvertImageAction); }
        }

        public ICommand ConvertImageAsync
        {
            get { return new RelayCommand(ConvertImageAsyncAction); }
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
            ImageProcessingModel imageProcessingModel = new ImageProcessingModel();
            string fileNameForCovertedImage = imageProcessingModel.ProcessImage(this.fileName, this.isAsync);
            this.ConvertedImage = new BitmapImage(new Uri(fileNameForCovertedImage));
            this.DurationTime = string.Format("{0:hh}:{0:mm}:{0:ss}.{0:ffff}", imageProcessingModel.durationTimeSpan);
        }

        private void ConvertImageAsyncAction()
        {
            this.isAsync = true;
            ConvertImageAction();
            this.isAsync = false;
        }

        public void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
