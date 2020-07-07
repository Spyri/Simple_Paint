using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Schema;
using Simple_Paint.ViewModel;
using Image = System.Drawing.Image;

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
            //_simplePaintViewModel.createNewImage();
        }

        public void paint_Pixel(int x,int y, int pt)
        {
            int j = 0;
            int loop = pt;
            while (loop != 0)
            {
                int temp = ((y-loop) * SimplePaintViewModel.stride) + x*SimplePaintViewModel.bytesPerPixel;
                for (int i = temp; i < temp + SimplePaintViewModel.bytesPerPixel * pt && i < ((y-loop)*SimplePaintViewModel.height+SimplePaintViewModel.width)*SimplePaintViewModel.bytesPerPixel; i++)
                {
                    if (i < 0 || i > SimplePaintViewModel.ImageData.Length) continue;
                    SimplePaintViewModel.ImageData[i] = SimplePaintViewModel.currentColour[j];
                    j++;
                    if (j == SimplePaintViewModel.bytesPerPixel) j = 0;
                }
                loop = loop - 1;
            }
            _simplePaintViewModel.UpdateImage();
        }

        public void createNewImage()
        {
            _simplePaintViewModel.createNewImage();
        }

        public void createImage(BitmapSource image = null)
        {
            if(image == null)
                _simplePaintViewModel.UpdateImage();
            else
            {
                _simplePaintViewModel.LoadImage(image);
            }
        }
    }
}