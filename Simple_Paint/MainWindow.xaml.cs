using System;
using System.Management.Instrumentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.Command;
using Simple_Paint.ViewModel;

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
            int pt = point.SelectedIndex + 1;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ImageSource bit = image.Source;
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (e.GetPosition(image).X * bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (e.GetPosition(image).Y * bitmapSource.PixelHeight / image.ActualHeight);
                SimplePaintViewModel.paint_Pixel(x, y, pt);
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
          SimplePaintViewModel.clear();  
        }
    }
}