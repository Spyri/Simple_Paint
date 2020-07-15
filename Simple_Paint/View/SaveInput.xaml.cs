using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.ViewModel;

namespace Simple_Paint.View
{
    public partial class SaveInput : Window
    {
        public SaveInput()
        {
            InitializeComponent();
            
        }

        private void CheckWhichType(object sender, RoutedEventArgs e)
        {
            if (PNGSave.IsChecked == true)
            {
                SaveAsPNG();
            }

            if (JPEGSave.IsChecked == true)
            {
                SaveAsJPEG();
            }
            Close();
        }
        private void SaveAsPNG()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)SimplePaintViewModel.ToSave.Source));
                FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                encoder.Save(stream);
                stream.Close();
            }
        }
        private void SaveAsJPEG()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files ( *.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)SimplePaintViewModel.ToSave.Source));
                FileStream stream = new FileStream(saveFileDialog.FileName.Substring(0,saveFileDialog.FileName.Length-4) + ".jpg", FileMode.Create);
                encoder.Save(stream);
                stream.Close();
            }
        }
    }
}