using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.Annotations;
using Simple_Paint.Command;
using Image = System.Drawing.Image;

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

        public List<TempImage> ImageSave { get; set; }


        public SimplePaintViewModel()
        {
            ImageSave = new List<TempImage>();
            new TempImage();
            CurrentColour = new byte[3];
            CurrentColour = new[] {Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0)};
            Ic = new ImageCommand(this);
            NewImage(100,100);
        }

        public void CreateNewImage()
        {
            for (int j = ImageSave.Count-1; j >= 0; j--)
            {
                ImageSave.Remove(ImageSave[j]);
            }
            Imagesource = BitmapSource.Create(Width,Height,400,400, PixelFormats.Bgr24, null,ImageData,Stride);
            ImageSave.Add(new TempImage(Imagesource,Stride));
        }
        
        public void UpdateImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth, Imagesource.PixelHeight, Imagesource.DpiX,
                    Imagesource.DpiY, Imagesource.Format, Imagesource.Palette, ImageData, Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8));
            //ImageSave.Add(new TempImage(Imagesource,Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8)));
        }
        public void SaveTempImage()
        {
            Imagesource = BitmapSource.Create(Imagesource.PixelWidth, Imagesource.PixelHeight, Imagesource.DpiX,
                Imagesource.DpiY, Imagesource.Format, Imagesource.Palette, ImageData, Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8));
            ImageSave.Add(new TempImage(Imagesource,Imagesource.PixelWidth*(Imagesource.Format.BitsPerPixel/8)));
        }

        public static void startSavingTemp()
        {
            Ic.SaveImage();
        }
        
        public void LoadImage(BitmapSource image)
        {
            for (int j = ImageSave.Count-1; j >= 0; j--)
            {
                ImageSave.Remove(ImageSave[j]);
            }
            Imagesource = BitmapSource.Create(image.PixelWidth,image.PixelHeight,image.DpiX,image.DpiY,image.Format, image.Palette,ImageData,Stride);
            ImageSave.Add(new TempImage(Imagesource,Imagesource.PixelWidth*Imagesource.Format.BitsPerPixel/8));
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

        public static void RedoMove()
        {
            if(TempImage.newesPicIndex>=1)
            {
                TempImage.newesPicIndex--;
                Ic.ReturnMove();
            }
        }

        public void ShowOlderImage()
        {
            TempImage e = ImageSave[TempImage.newesPicIndex];
            ImageData = e.tempPixelData;
            Imagesource = BitmapSource.Create(e.tempbmp.PixelWidth,e.tempbmp.PixelHeight,e.tempbmp.DpiX,e.tempbmp.DpiY,e.tempbmp.Format,e.tempbmp.Palette,e.tempPixelData,e.tempbmp.PixelWidth*e.tempbmp.Format.BitsPerPixel/8);
            for (int i = ImageSave.Count-1; i > TempImage.newesPicIndex; i--)
            {
                ImageSave.Remove(ImageSave[i]);
            }
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