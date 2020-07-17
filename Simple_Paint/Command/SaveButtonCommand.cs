using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class SaveButtonCommand : ICommand
    {
        private readonly SavePageViewModel _savePageViewModel;

        public SaveButtonCommand(SavePageViewModel savePageViewModel)
        {
            _savePageViewModel = savePageViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_savePageViewModel.PngSaveButton)
            {
                    SaveAsPNG();
            }

            if (_savePageViewModel.JpegSaveButton)
            {
                SaveAsJPEG();
            }
            Application.Current.Windows[1]?.Close();
            
            
           
        }

        private void SaveAsPNG()
        { 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(SimplePaintViewModel.ToSave));
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
                encoder.Frames.Add(BitmapFrame.Create(SimplePaintViewModel.ToSave));
                FileStream stream = new FileStream(saveFileDialog.FileName.Substring(0,saveFileDialog.FileName.Length-4) + ".jpg", FileMode.Create);
                encoder.Save(stream);
                stream.Close();
            }
        }
        public event EventHandler CanExecuteChanged;
    }
}