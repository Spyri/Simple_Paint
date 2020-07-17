﻿using System;
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
        

        public void ClearInit(int width, int height)
        {
            byte[] TempPixel = new byte[height * width*3];
            for (int i = 0; i < TempPixel.Length; i++)
            {
                TempPixel[i] = byte.MaxValue;
            }
            _simplePaintViewModel.SetImageData(TempPixel);
        }

        public void SaveImage()
        {
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource, _simplePaintViewModel.GetStride()));
        }
        

        public void PaintPixel(int x,int y)
        {
            int pt = _simplePaintViewModel.Getptr();
            int j = 0;
            int pixelWidth = _simplePaintViewModel.GetBytesPerPixel() * pt;
            int pixel = pt;
            y = y + 1;
            int xArrayPosition = x * _simplePaintViewModel.GetBytesPerPixel();
            int actualPosition;
            int yArrayPosition;
            byte[] PixelData = _simplePaintViewModel.GetImageData();
            for (int pixelOverY = pixel; pixelOverY > 0; pixelOverY--)
            {
                
                yArrayPosition = (y - pixelOverY) * _simplePaintViewModel.GetStride();
                actualPosition = yArrayPosition + xArrayPosition;
                for (int i = actualPosition; i < actualPosition + pixelWidth && i < yArrayPosition+_simplePaintViewModel.GetStride(); i++)
                {
                    if (i < 0 || i > PixelData.Length) continue;
                    PixelData[i] = SimplePaintViewModel.CurrentColour[j];
                    j++; 
                    if (j == _simplePaintViewModel.GetBytesPerPixel()) j = 0;
                }
            }
            _simplePaintViewModel.SetImageData(PixelData);
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Imagesource.PixelWidth, _simplePaintViewModel.Imagesource.PixelHeight, _simplePaintViewModel.Imagesource.DpiX,
                _simplePaintViewModel.Imagesource.DpiY, _simplePaintViewModel.Imagesource.Format, _simplePaintViewModel.Imagesource.Palette, _simplePaintViewModel.GetImageData(), _simplePaintViewModel.Imagesource.PixelWidth*(_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8));
        }
        

        public void CreateNewImage(int width, int height)
        {
            _simplePaintViewModel.SetStride(width * _simplePaintViewModel.GetBytesPerPixel());
            _simplePaintViewModel.SetWidth(width);
            _simplePaintViewModel.SetHeight(height);
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.GetWidth(),_simplePaintViewModel.GetHeight(),400,400, PixelFormats.Bgr24, null,_simplePaintViewModel.GetImageData(),_simplePaintViewModel.GetStride());
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.GetStride()));
            _simplePaintViewModel.SetFirstImage(_simplePaintViewModel.Imagesource);
        }
        
        
    }
}