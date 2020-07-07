using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.SqlServer.Server;
using Simple_Paint.Annotations;
using Simple_Paint.Command;
using Image = System.Drawing.Image;

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
        
        public static int bytesPerPixel { get; set; }
        public static int stride { get; set; }
        public static ImageCommand IC { get; set; }
        public static byte[] currentColour { get; set; }

        public static string ptr { get; set; }


        public SimplePaintViewModel()
        {
            currentColour = new byte[4];
            currentColour = new[] {Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0)};
            IC = new ImageCommand(this);
            NewImage();
            //Imagesource = BitmapSource.Create(width,height,400,400, PixelFormats.Bgr24, null,ImageData,stride);
        }

        public void createNewImage()
        {
            Imagesource = BitmapSource.Create(width,height,400,400, PixelFormats.Bgr24, null,ImageData,stride);
        }
        
        public void UpdateImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth, Imagesource.PixelHeight, Imagesource.DpiX,
                    Imagesource.DpiY, Imagesource.Format, Imagesource.Palette, ImageData, Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8));
        }
        
        public void LoadImage(BitmapSource image)
        {
            Imagesource = BitmapSource.Create(image.PixelWidth,image.PixelHeight,image.DpiX,image.DpiY,image.Format, image.Palette,ImageData,stride);
        }

        public static void createImage(BitmapSource i = null)
        {
            if (i == null)
            {
                IC.clearAll();
                IC.createImage();
            }
            else
            {
                width = i.PixelWidth;
                height = i.PixelHeight;
                bytesPerPixel= i.Format.BitsPerPixel/8;
                stride = bytesPerPixel * i.PixelWidth;
                IC.createImage(i);
            }
        }
        public static void NewImage()
        {
            width = 100;
            height = 100;
            bytesPerPixel = 3;
            stride = width * bytesPerPixel;
            ImageData = new byte[height * stride];
            IC.clearAll();
            IC.createNewImage();
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