using System.Windows.Media.Imaging;

namespace Simple_Paint.ViewModel
{
    public class TempImage
    {
        public BitmapSource Tempbmp { get; set; }
        public byte[] TempPixelData { get; set; }
        
        public TempImage(BitmapSource b,int stride)
        {
            TempPixelData = new byte[b.PixelHeight*stride];
            Tempbmp = b;
            b.CopyPixels(TempPixelData,b.PixelWidth*b.Format.BitsPerPixel/8,0);
        }
    }
}