using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Schema;
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
            _simplePaintViewModel.UpdateImage();
        }

        public void paint_Pixel(int x,int y, int pt)
        {
            int j = 0;
            int loop = pt;
            while (loop != 0)
            {
                int temp = (x + (y-loop) * SimplePaintViewModel.height) * 4;
                for (int i = temp; i < temp + 4 * pt; i++)
                {
                    if (i < 0) continue;
                    SimplePaintViewModel.ImageData[i] = SimplePaintViewModel.currentColour[j];
                    j++;
                    if (j == 4) j = 0;
                }

                _simplePaintViewModel.UpdateImage();
                loop = loop - 1;
            }
        }
    }
}