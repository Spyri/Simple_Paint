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
            byte[] tempPixel = new byte[height * width*3];
            for (int i = 0; i < tempPixel.Length; i++)
            {
                tempPixel[i] = byte.MaxValue;
            }
            _simplePaintViewModel.SetImageData(tempPixel);
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