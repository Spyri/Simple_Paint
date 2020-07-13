using System;
using System.Collections.Generic;
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

        public void clearAll()
        {
            for (int i = 0; i < SimplePaintViewModel.ImageData.Length; i++)
            {
                SimplePaintViewModel.ImageData[i] = Convert.ToByte(255);
            }
        }

        public void SaveImage()
        {
            UpdateImage();
            _simplePaintViewModel.ImageSave.Add(new TempImage(_simplePaintViewModel.Imagesource, _simplePaintViewModel.Imagesource.PixelWidth * (_simplePaintViewModel.Imagesource.Format.BitsPerPixel / 8)));
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
            --TempImage.NewesPicIndex;
            TempImage e = _simplePaintViewModel.ImageSave[TempImage.NewesPicIndex];
            SimplePaintViewModel.ImageData = e.TempPixelData;
            _simplePaintViewModel.Imagesource = BitmapSource.Create(e.Tempbmp.PixelWidth,e.Tempbmp.PixelHeight,e.Tempbmp.DpiX,e.Tempbmp.DpiY,e.Tempbmp.Format,e.Tempbmp.Palette,e.TempPixelData,e.Tempbmp.PixelWidth*e.Tempbmp.Format.BitsPerPixel/8);
            _simplePaintViewModel.ImageSave.RemoveAt(TempImage.NewesPicIndex);
            if (TempImage.NewesPicIndex == 0)
            {
                _simplePaintViewModel.ImageSave.Add(new TempImage(_simplePaintViewModel.Imagesource,SimplePaintViewModel.Stride));
            }
        }

        public void CreateNewImage()
        {
            _simplePaintViewModel.ImageSave = new List<TempImage>();
            for (int j = _simplePaintViewModel.ImageSave.Count-1; j >= 0; j--)
            {
                _simplePaintViewModel.ImageSave.RemoveAt(j);
            }
            TempImage.NewesPicIndex = 0;
            _simplePaintViewModel.Imagesource = BitmapSource.Create(SimplePaintViewModel.Width,SimplePaintViewModel.Height,400,400, PixelFormats.Bgr24, null,SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride);
            _simplePaintViewModel.ImageSave.Add(new TempImage(_simplePaintViewModel.Imagesource,SimplePaintViewModel.Stride));
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
                for (int j = _simplePaintViewModel.ImageSave.Count-1; j >= 0; j--)
                {
                    _simplePaintViewModel.ImageSave.Remove(_simplePaintViewModel.ImageSave[j]);
                }
                TempImage.NewesPicIndex = 0;
                _simplePaintViewModel.Imagesource = BitmapSource.Create(image.PixelWidth,image.PixelHeight,image.DpiX,image.DpiY,image.Format, image.Palette,SimplePaintViewModel.ImageData,SimplePaintViewModel.Stride);
                _simplePaintViewModel.ImageSave.Add(new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.Imagesource.PixelWidth*_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8));;
            }
        }

        public void ChangeStretched()
        {
            _simplePaintViewModel.ImageStreched = _simplePaintViewModel.ImageStreched == Stretch.Uniform ? Stretch.Fill : Stretch.Uniform;
        }
    }
}