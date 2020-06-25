using System;
using System.ComponentModel;
using System.Data;
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
        private string _ptr;

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
        public static byte[] currentColour { get; set; }

        public static string ptr { get; set; }


        public SimplePaintViewModel()
        {
            currentColour = new byte[4];
            currentColour = new[] {Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0)};
            width = 900;
            height = 900;
            IC = new ImageCommand(this);
            stride = 4 * width;
            ImageData = new byte[height*stride];
            Imagesource = BitmapSource.Create(width,height,800,800, PixelFormats.Bgr32, null,ImageData,stride);
            IC.clearAll();
            Imagesource = BitmapSource.Create(width,height,800,800, PixelFormats.Bgr32, null,ImageData,stride);
        }
        
        public void UpdateImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth,Imagesource.PixelHeight,Imagesource.DpiX,Imagesource.DpiY,Imagesource.Format,Imagesource.Palette,ImageData,Imagesource.PixelWidth*4);
        }

        public static void paint_Pixel(int x, int y)
        {
            IC.paint_Pixel(x,y,getprt());
        }

        public static int getprt()
        {
            int i = Convert.ToInt32(ptr);
            return i;
        }

        public static void clear()
        {
            IC.clearAll();
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}