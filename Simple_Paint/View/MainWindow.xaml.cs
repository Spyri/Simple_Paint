﻿using System;
using System.IO;
 using System.Text.RegularExpressions;
using System.Windows;
 using System.Windows.Controls;
 using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Simple_Paint.ViewModel;

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
            for (int i = 0; i < 20; i++)
            {
                Button newBtn = new Button
                {
                    Name = "b" +i.ToString(),
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SimplePaintViewModel.Colors[i].Color.R,SimplePaintViewModel.Colors[i].Color.G,SimplePaintViewModel.Colors[i].Color.B)),
                };
                if (i > 9)
                {
                    Grid.SetRow(newBtn, 1);
                    Grid.SetColumn(newBtn, i - 10);
                }
                else
                {
                    Grid.SetRow(newBtn,0);
                    Grid.SetColumn(newBtn,i);
                }

                newBtn.Click += Red_OnClick;
                ColorPanel.Children.Add(newBtn);
                

            }
            RoutedCommand cmdredo = new RoutedCommand();
            cmdredo.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdredo, ReturnMove_OnClick));
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Cursor = Cursors.Pen;
                ImageSource bit = image.Source; 
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (e.GetPosition(image).X * bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (e.GetPosition(image).Y * bitmapSource.PixelHeight / image.ActualHeight);
                SimplePaintViewModel.paint_Pixel(x, y);
            }
        }


        private void Red_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            int index =  Convert.ToInt32(b.Name.Substring(1,b.Name.Length-1));
            
            
            SimplePaintViewModel.CurrentColour[0] = SimplePaintViewModel.Colors[index].Color.B;
            SimplePaintViewModel.CurrentColour[1] = SimplePaintViewModel.Colors[index].Color.G;
            SimplePaintViewModel.CurrentColour[2] = SimplePaintViewModel.Colors[index].Color.R;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.CreateImage();  
        }

        #region Load Image
        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
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
        private static void OpenImage(BitmapSource b) 
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
        #endregion
        

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

        private void ReturnMove_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.UndoMove();
        }

        private void SaveTemp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SimplePaintViewModel.StartSavingTemp();
        }

        private void ChangeStretched_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.ChangeStretched();
        }
    }
}