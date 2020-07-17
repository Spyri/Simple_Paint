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
        

        public void ClearInit()
        {
            for (int i = 0; i < SimplePaintViewModel.ImageData.Length; i++)
            {
                SimplePaintViewModel.ImageData[i] = byte.MaxValue;
            }
        }

        public void SaveImage()
        {
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource, _simplePaintViewModel.GetStride()));
        }

        public void PaintPixel(int x,int y, int pt)
        {
            int j = 0;
            int pixelWidth = _simplePaintViewModel.BytesPerPixel * pt;
            int pixel = pt;
            y = y + 1;
            int xArrayPosition = x * _simplePaintViewModel.BytesPerPixel;
            int actualPosition;
            int yArrayPosition;
            
            for (int pixelOverY = pixel; pixelOverY > 0; pixelOverY--)
            {
                yArrayPosition = (y - pixelOverY) * _simplePaintViewModel.GetStride();
                actualPosition = yArrayPosition + xArrayPosition;
                for (int i = actualPosition; i < actualPosition + pixelWidth && i < yArrayPosition+_simplePaintViewModel.GetStride(); i++)
                {
                    if (i < 0 || i > SimplePaintViewModel.ImageData.Length) continue;
                    SimplePaintViewModel.ImageData[i] = SimplePaintViewModel.CurrentColour[j];
                    j++; 
                    if (j == _simplePaintViewModel.BytesPerPixel) j = 0;
                }
            }
            UpdateImage();
        }
        

        public void CreateNewImage(int width, int height)
        {
            _simplePaintViewModel.BytesPerPixel = 3;
            _simplePaintViewModel.SetStride(width * _simplePaintViewModel.BytesPerPixel);
            _simplePaintViewModel.Width = width;
            _simplePaintViewModel.Height = height;
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Width,_simplePaintViewModel.Height,400,400, PixelFormats.Bgr24, null,SimplePaintViewModel.ImageData,_simplePaintViewModel.GetStride());
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.GetStride()));
            _simplePaintViewModel.FirstImage = _simplePaintViewModel.Imagesource;
        }

        private void UpdateImage()
        {
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Imagesource.PixelWidth, _simplePaintViewModel.Imagesource.PixelHeight, _simplePaintViewModel.Imagesource.DpiX,
                _simplePaintViewModel.Imagesource.DpiY, _simplePaintViewModel.Imagesource.Format, _simplePaintViewModel.Imagesource.Palette, SimplePaintViewModel.ImageData, _simplePaintViewModel.Imagesource.PixelWidth*(_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8));
        }
        
    }
}