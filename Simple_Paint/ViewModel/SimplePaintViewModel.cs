using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.SqlServer.Server;
using Simple_Paint.Annotations;
using Simple_Paint.Command;

namespace Simple_Paint.ViewModel
{
    public class SimplePaintViewModel : INotifyPropertyChanged
    {
        private BitmapSource _imagesource;

        public BitmapSource Imagesource
        {
            get => _imagesource;
            set
            {
                if (Equals(value, _imagesource)) return;
                _imagesource = value;
                OnPropertyChanged();
            }
        }

        public static byte[] ImageData { get; set; }
        public static int width { get; set; }
        public static int height { get; set; }
        public static int stride { get; set; }
        public static ImageCommand IC { get; set; }
        
        

        public SimplePaintViewModel()
        {
            
            width = 1000;
            height = 1000;
            IC = new ImageCommand(this);
            stride = 4 * width;
            ImageData = new byte[height*stride];
            IC.makeblack(ImageData);
            Imagesource = BitmapSource.Create(width,height,800,800, PixelFormats.Bgr32, null,ImageData,stride);
        }
        
        public void UpdateImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth,Imagesource.PixelHeight,Imagesource.DpiX,Imagesource.DpiY,Imagesource.Format,Imagesource.Palette,ImageData,Imagesource.PixelWidth*4);
        }

        public static void makepixelblack(int x, int y, int pt)
        {
            IC.makepixelblack(x,y,pt);
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}