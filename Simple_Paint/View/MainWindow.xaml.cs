using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.ViewModel;
using Image = System.Drawing.Image;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Simple_Paint.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Cursor = Cursors.Pen;
                ImageSource bit = image.Source;
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (e.GetPosition(image).X * bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (e.GetPosition(image).Y * bitmapSource.PixelHeight / image.ActualHeight);
                SimplePaintViewModel.paint_Pixel(x, y);
            }
        }


        private void Red_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(255);
        }

        private void Green_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(0);
        }

        private void Black_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(0); 
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(0); 
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(0);
        }

        private void White_OnClick(object sender, RoutedEventArgs e)
        { 
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(255); 
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(255);
        }

        private void Blue_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(255);
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(0);
        }

        private void DarkGreen_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(100);
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(0);
        }

        private void Fuchsia_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CurrentColour[0] = Convert.ToByte(255); 
            SimplePaintViewModel.CurrentColour[1] = Convert.ToByte(0); 
            SimplePaintViewModel.CurrentColour[2] = Convert.ToByte(255);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CreateImage();  
        }

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg;|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Console.WriteLine(openFileDialog.SafeFileName);
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

        public static void OpenImage(BitmapSource b)
        {
            SimplePaintViewModel.ImageData = new byte[b.PixelHeight* b.PixelWidth*(b.Format.BitsPerPixel/8)];
            b.CopyPixels(SimplePaintViewModel.ImageData,b.PixelWidth*(b.Format.BitsPerPixel/8),0);
            SimplePaintViewModel.CreateImage(b);
        }

        private static BitmapSource CreateFromPng( string path )
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
        private static BitmapSource CreateFromJpeg( string path )
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

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            NewPageInput n = new NewPageInput();
            n.Show();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckIfInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            SaveInput save = new SaveInput();
            SimplePaintViewModel.ToSave = image;
            save.Show();
        }
    }
}