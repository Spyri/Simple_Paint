using System;
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
                SimplePaintViewModel.makepixelblack(x, y, pt);
            }
        }
    }
}