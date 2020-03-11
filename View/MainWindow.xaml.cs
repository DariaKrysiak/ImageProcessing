using System.Windows;
using Microsoft.Win32;
using ImageProcessingApplication.ViewModel;

namespace ImageProcessingApplication.View
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
