using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using ImageProcessingLibrary;
using Image_Processing_application.ViewModel;

namespace Image_Processing_application.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();         
            DataContext = new MainWindowViewModel(CreateDialog());
        }

        private OpenFileDialog CreateDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select a picture";
            dialog.Filter = "All supported graphics|*.jpg;*.png;*.bmp|" +
              "JPG (*.jpg)|*.jpg" +
              "PNG (*.png)|*.png" +
              "BMP (*.bmp)|*.bmp";
            return dialog;
        }
    }
}
