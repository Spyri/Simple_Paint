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
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.Command;
using Simple_Paint.ViewModel;
using Image = System.Drawing.Image;

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
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

        private void Green_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

        private void Black_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

        private void White_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

       private void Blue_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

        private void DarkGreen_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(100);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
        }

        private void Fuchsia_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.currentColour[0] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[1] = Convert.ToByte(0);
            SimplePaintViewModel.currentColour[2] = Convert.ToByte(255);
            SimplePaintViewModel.currentColour[3] = Convert.ToByte(0);
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


        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapSource b = CreateFromPng(openFileDialog.FileName);
                SimplePaintViewModel.ImageData = new byte[b.PixelHeight* b.PixelWidth*(b.Format.BitsPerPixel/8)];
                b.CopyPixels(SimplePaintViewModel.ImageData,b.PixelWidth*(b.Format.BitsPerPixel/8),0);
                SimplePaintViewModel.CreateImage(b);
            }
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
            // Open a Stream and decode a PNG image
			
            Stream imageStreamSource = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read );

            PngBitmapDecoder decoder = new PngBitmapDecoder( imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default );

            BitmapSource bmpSource = decoder.Frames[ 0 ];

            return bmpSource;
        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.NewImage();
        }
    }
}