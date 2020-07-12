using System.Windows.Media.Imaging;

namespace Simple_Paint.ViewModel
{
    public class TempImage
    {
        public BitmapSource tempbmp { get; set; }
        public byte[] tempPixelData { get; set; }
        public static int newesPicIndex { get; set; }

        public TempImage()
        {
            newesPicIndex = 0;
        }
        public TempImage(BitmapSource b,int stride)
        {
            tempPixelData = new byte[b.PixelHeight*stride];
            tempbmp = b;
            b.CopyPixels(tempPixelData,b.PixelWidth*b.Format.BitsPerPixel/8,0);
            newesPicIndex++;
        }
    }
}