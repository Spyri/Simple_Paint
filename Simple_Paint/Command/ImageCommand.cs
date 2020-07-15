using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.ViewModel;


namespace Simple_Paint.Command
{
    public class ImageCommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public ImageCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public void ClearAll()
        {
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.FirstImagee.CopyPixels(SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride,0); 
            _simplePaintViewModel.Imagesource = _simplePaintViewModel.FirstImagee; 
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,SimplePaintViewModel.Stride));
        }

        public void clearInit()
        {
            for (int i = 0; i < SimplePaintViewModel.ImageData.Length; i++)
            {
                SimplePaintViewModel.ImageData[i] = Convert.ToByte(255);
            }
        }

        public void SaveImage()
        {
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource, _simplePaintViewModel.Imagesource.PixelWidth * (_simplePaintViewModel.Imagesource.Format.BitsPerPixel / 8)));
        }

        public void paint_Pixel(int x,int y, int pt)
        {
            int j = 0;
            int loop = pt;
            y = y + 1;
            while (loop != 0)
            {
                int temp = ((y-loop) * SimplePaintViewModel.Stride) + x*SimplePaintViewModel.BytesPerPixel;
                for (int i = temp; i < temp + SimplePaintViewModel.BytesPerPixel * pt && i < (y-loop)*SimplePaintViewModel.Stride+SimplePaintViewModel.Width*SimplePaintViewModel.BytesPerPixel; i++)
                {
                    if (i < 0 || i > SimplePaintViewModel.ImageData.Length) continue;
                    SimplePaintViewModel.ImageData[i] = SimplePaintViewModel.CurrentColour[j];
                    j++;
                    if (j == SimplePaintViewModel.BytesPerPixel) j = 0;
                }
                loop = loop - 1;
            }
            UpdateImage();
        }

        public void ReturnMove()
        {
            if (_simplePaintViewModel.ImageSave.Count-1 >= 0)
            {
                if(_simplePaintViewModel.ImageSave.Count-1 == 0)
                {
                    _simplePaintViewModel.ImageSave.Clear();
                    _simplePaintViewModel.FirstImagee.CopyPixels(SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride,0);
                    _simplePaintViewModel.Imagesource = _simplePaintViewModel.FirstImagee;
                    _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,SimplePaintViewModel.Stride));
                    return;
                }
                _simplePaintViewModel.ImageSave.Pop();
                var tempImage = _simplePaintViewModel.ImageSave.Peek();
                SimplePaintViewModel.ImageData = tempImage.TempPixelData;
                _simplePaintViewModel.Imagesource = BitmapSource.Create(tempImage.Tempbmp.PixelWidth, tempImage.Tempbmp.PixelHeight,tempImage.Tempbmp.DpiX,tempImage.Tempbmp.DpiY,tempImage.Tempbmp.Format,tempImage.Tempbmp.Palette,SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride);
            }
        }

        public void CreateNewImage()
        {
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.Imagesource = BitmapSource.Create(SimplePaintViewModel.Width,SimplePaintViewModel.Height,400,400, PixelFormats.Bgr24, null,SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride);
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,SimplePaintViewModel.Stride));
            _simplePaintViewModel.FirstImagee = _simplePaintViewModel.Imagesource;
        }
        
        public void UpdateImage()
        {
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Imagesource.PixelWidth, _simplePaintViewModel.Imagesource.PixelHeight, _simplePaintViewModel.Imagesource.DpiX,
                _simplePaintViewModel.Imagesource.DpiY, _simplePaintViewModel.Imagesource.Format, _simplePaintViewModel.Imagesource.Palette, SimplePaintViewModel.ImageData, _simplePaintViewModel.Imagesource.PixelWidth*(_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8));
        }

        public void CreateImage(BitmapSource image = null)
        {
            if(image == null)
                UpdateImage();
            else
            {
                _simplePaintViewModel.ImageSave.Clear();
                _simplePaintViewModel.Imagesource = BitmapSource.Create(image.PixelWidth,image.PixelHeight,image.DpiX,image.DpiY,image.Format, image.Palette,SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride);
                _simplePaintViewModel.FirstImagee = _simplePaintViewModel.Imagesource;
                _simplePaintViewModel.ImageSave.Push((new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.Imagesource.PixelWidth*_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8)));
            }
        }

        public void ChangeStretched()
        {
            _simplePaintViewModel.ImageStreched = _simplePaintViewModel.ImageStreched == Stretch.Uniform ? Stretch.Fill : Stretch.Uniform;
        }
    }
}