using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.ViewModel;
using Image = System.Windows.Controls.Image;

namespace Simple_Paint.Command
{
    public class MouseCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public MouseCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Mouse.SetCursor(Cursors.Pen);
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            { 
                var image = (Image) Mouse.DirectlyOver;
                ImageSource bit = image.Source; 
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (Mouse.GetPosition(image).X*bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (Mouse.GetPosition(image).Y* bitmapSource.PixelHeight / image.ActualHeight);
                PaintPixel(x,y);
                Mouse.SetCursor(Cursors.Pen);
            }
        }

        public void PaintPixel(int x,int y)
        {
            int pt = _simplePaintViewModel.Getptr();
            int bytesPerPixel = _simplePaintViewModel.GetBytesPerPixel();
            int j = 0;
            int xPixelWidth = bytesPerPixel * pt;
            int stride = _simplePaintViewModel.GetStride();
            y = y + 1;
            int xArrayPosition = x * bytesPerPixel;
            int actualPosition;
            int yArrayPosition;
            byte[] pixelData = _simplePaintViewModel.GetImageData();
            for (int pixelOverY = pt; pixelOverY > 0; pixelOverY--)
            {
                yArrayPosition = (y - pixelOverY) * stride;
                actualPosition = yArrayPosition + xArrayPosition;
                for (int i = actualPosition; i < actualPosition + xPixelWidth && i < yArrayPosition+stride; i++)
                {
                    if (i < 0 || i > pixelData.Length-1) continue;
                    pixelData[i] = _simplePaintViewModel.CurrentColour[j];
                    j++; 
                    if (j == bytesPerPixel) j = 0;
                }
            }
            _simplePaintViewModel.SetImageData(pixelData);
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Imagesource.PixelWidth,
                _simplePaintViewModel.Imagesource.PixelHeight, _simplePaintViewModel.Imagesource.DpiX,
                _simplePaintViewModel.Imagesource.DpiY, _simplePaintViewModel.Imagesource.Format,
                _simplePaintViewModel.Imagesource.Palette, pixelData,
                stride);
        }
        public event EventHandler CanExecuteChanged;
    }
}