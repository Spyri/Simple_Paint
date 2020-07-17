using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class OpenFileCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public OpenFileCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg;|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.SafeFileName.Contains(".png"))
                {
                    BitmapSource b = CreateFromPng(openFileDialog.FileName);
                    OpenImage(b);
                }
                else
                {
                    BitmapSource jpeg = CreateFromJpeg(openFileDialog.FileName);
                    OpenImage(jpeg);
                }
            }
        }

        private void OpenImage(BitmapSource b)
        {
            SimplePaintViewModel.ImageData = new byte[b.PixelHeight* b.PixelWidth*(b.Format.BitsPerPixel/8)];
            b.CopyPixels(SimplePaintViewModel.ImageData,b.PixelWidth*(b.Format.BitsPerPixel/8),0);
            _simplePaintViewModel.Width = b.PixelWidth;
            _simplePaintViewModel.Height = b.PixelHeight;
            _simplePaintViewModel.BytesPerPixel= b.Format.BitsPerPixel/8;
            _simplePaintViewModel.SetStride(_simplePaintViewModel.BytesPerPixel * b.PixelWidth);
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.Imagesource = BitmapSource.Create(b.PixelWidth,b.PixelHeight,b.DpiX,b.DpiY,b.Format, b.Palette,SimplePaintViewModel.ImageData,_simplePaintViewModel.GetStride());
            _simplePaintViewModel.FirstImage = _simplePaintViewModel.Imagesource;
            _simplePaintViewModel.ImageSave.Push((new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.Imagesource.PixelWidth*_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8)));
        }

        private BitmapSource CreateFromJpeg(string path)
        {
            Stream imageStreamSource = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read );
            JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource,BitmapCreateOptions.PreservePixelFormat,BitmapCacheOption.Default);

            BitmapSource bmpSource = decoder.Frames[ 0 ];

            FormatConvertedBitmap b = new FormatConvertedBitmap();
            if (bmpSource.Format != PixelFormats.Bgr24)
            {
                b.BeginInit();
                b.Source = bmpSource;
                b.DestinationFormat = PixelFormats.Bgr24;
                b.EndInit();
                return b;
            }
            return bmpSource;
        }

        private BitmapSource CreateFromPng(string path)
        {
            Stream imageStreamSource = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read );
            PngBitmapDecoder decoder = new PngBitmapDecoder( imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default );
            BitmapSource bmpSource = decoder.Frames[ 0 ];
            
            if (bmpSource.Format != PixelFormats.Bgr24)
            {
                FormatConvertedBitmap b = new FormatConvertedBitmap();
                b.BeginInit();
                b.Source = bmpSource;
                b.DestinationFormat = PixelFormats.Bgr24;
                b.EndInit();
                return b;
            }
            return bmpSource;
        }

        public event EventHandler CanExecuteChanged;
    }
}