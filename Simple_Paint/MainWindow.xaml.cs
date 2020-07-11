using System;
using System.IO;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.Command;
using Simple_Paint.ViewModel;
using Image = System.Drawing.Image;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Simple_Paint
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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ImageSource bit = image.Source;
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (e.GetPosition(image).X * bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (e.GetPosition(image).Y * bitmapSource.PixelHeight / image.ActualHeight);
                SimplePaintViewModel.paint_Pixel(x, y);
            }
        }


        private void Red_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(255);
        }

        private void Green_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
        }

        private void Black_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0); 
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0); 
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
        }

        private void White_OnClick(object sender, RoutedEventArgs e)
        { 
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255); 
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(255);
        }

        private void Blue_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
        }

        private void DarkGreen_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(100);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
        }

        private void Fuchsia_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255); 
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0); 
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(255);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CreateImage();  
        }

        private void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    encoder.Save(stream);
                    stream.Close();
            }
        }
        private void SaveFileJpeg_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                encoder.Save(stream);
                stream.Close();
            }
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
                    openImage(b);
                }
                else
                {
                    BitmapSource jpeg = CreateFromJpeg(openFileDialog.FileName);
                    openImage(jpeg);
                }
            }
        }

        public static void openImage(BitmapSource b)
        {
            SimplePaintViewModel.ImageData = new byte[b.PixelHeight* b.PixelWidth*(b.Format.BitsPerPixel/8)];
            b.CopyPixels(SimplePaintViewModel.ImageData,b.PixelWidth*(b.Format.BitsPerPixel/8),0); 
            Console.WriteLine("Format: {0}  Width: {1}  Length: {2} BytesPerPixel: {3}", b.Format,b.PixelWidth,b.PixelHeight,b.Format.BitsPerPixel/8);
            SimplePaintViewModel.CreateImage(b);
        }
        
        public static byte[] BitmapSourceToByteArray(BitmapSource bitmap)
        {
            var encoder = new PngBitmapEncoder(); // or any other BitmapEncoder
            
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.GetBuffer();
            }
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
            if (bmpSource.Format != PixelFormats.Indexed8)
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

        private void Point_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}