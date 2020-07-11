using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.Annotations;
using Simple_Paint.Command;

namespace Simple_Paint.ViewModel
{
    public sealed class SimplePaintViewModel : INotifyPropertyChanged
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
        public static System.Windows.Controls.Image ToSave { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        
        public static int BytesPerPixel { get; set; }
        public static int Stride { get; set; }
        public static ImageCommand Ic { get; set; }
        public static byte[] CurrentColour { get; set; }
        public static string Ptr { get; set; }


        public SimplePaintViewModel()
        {
            CurrentColour = new byte[3];
            CurrentColour = new[] {Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0)};
            Ic = new ImageCommand(this);
            NewImage(100,100);
        }

        public void CreateNewImage()
        {
            Imagesource = BitmapSource.Create(Width,Height,400,400, PixelFormats.Bgr24, null,ImageData,Stride);
        }
        
        public void UpdateImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth, Imagesource.PixelHeight, Imagesource.DpiX,
                    Imagesource.DpiY, Imagesource.Format, Imagesource.Palette, ImageData, Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8));
        }
        
        public void LoadImage(BitmapSource image)
        {
            Imagesource = BitmapSource.Create(image.PixelWidth,image.PixelHeight,image.DpiX,image.DpiY,image.Format, image.Palette,ImageData,Stride);
        }

        public static void CreateImage(BitmapSource i = null)
        {
            if (i == null)
            {
                Ic.clearAll();
                Ic.CreateImage();
            }
            else
            {
                Width = i.PixelWidth;
                Height = i.PixelHeight;
                BytesPerPixel= i.Format.BitsPerPixel/8;
                Stride = BytesPerPixel * i.PixelWidth;
                Ic.CreateImage(i);
            }
        }
        public static void NewImage(int width, int height)
        {
            Width = width;
            Height = height;
            BytesPerPixel = 3;
            Stride = width * BytesPerPixel;
            ImageData = new byte[height * Stride];
            Ic.clearAll();
            Ic.CreateNewImage();
        }

        public static void paint_Pixel(int x, int y)
        {
            Ic.paint_Pixel(x,y,Getptr());
        }

        public static int Getptr()
        {
            // ptr = Dicke des Stiftes
            int i = Convert.ToInt32(Ptr);
            return i;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}