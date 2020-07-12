using System;
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
            _simplePaintViewModel.SaveTempImage();
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
            _simplePaintViewModel.UpdateImage();
        }

        public void ReturnMove()
        {
            _simplePaintViewModel.ShowOlderImage();
            
        }

        public void CreateNewImage()
        {
            _simplePaintViewModel.CreateNewImage();
        }

        public void CreateImage(BitmapSource image = null)
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