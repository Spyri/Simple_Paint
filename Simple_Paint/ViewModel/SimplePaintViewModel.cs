﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
 using System.Windows.Input;
 using System.Windows.Media;
using System.Windows.Media.Imaging;
using Simple_Paint.Annotations;
using Simple_Paint.Command;
using Simple_Paint.View;

namespace Simple_Paint.ViewModel
{
    public class SimplePaintViewModel : INotifyPropertyChanged
    {
        private BitmapSource _imagesource;
        private Stretch _imageStretched;

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

        public Stretch ImageStretched
        {
            get => _imageStretched;
            set
            {
                if (value == _imageStretched) return;
                _imageStretched = value;
                OnPropertyChanged();
            }
        }

        private byte[] ImageData { get; set; }
        public static BitmapSource ToSave { get; set; }
        private int Width { get; set; }

        private int Height { get; set; }

        private int BytesPerPixel { get; set; }
        private int Stride { get; set; }
        private static ImageCommand Ic { get; set; }
        public static byte[] CurrentColour { get; set; }
        public string Ptr { get; set; }
        public static SolidColorBrush[] Colors { get; private set; }
        public Stack<TempImage> ImageSave { get; private set; }
        private BitmapSource FirstImage { get; set; }
        public static ButtonUndoCommand Buc { get; set; }
        public StretchCommand Sc { get; set; }
        public NewImageCommand NiC { get; set; }
        public ClearCommand ClearCommand { get; set; }
        public OpenFileCommand OpenFileCommand { get; set; }
        public OpenSaveWindowCommand Oswc { get; set; }

        public SimplePaintViewModel()
        {
            Oswc = new OpenSaveWindowCommand(this);
            BytesPerPixel = 3;
            OpenFileCommand = new OpenFileCommand(this);
            ClearCommand = new ClearCommand(this);
            NiC = new NewImageCommand(this);
            Sc = new StretchCommand(this);
            Buc = new ButtonUndoCommand(this);
            Colors = new SolidColorBrush[20];
            Colors[0] = Brushes.Black;
            Colors[1] = Brushes.Gray;
            Colors[2] = Brushes.Brown;
            Colors[3] = Brushes.Red;
            Colors[4] = Brushes.DarkOrange;
            Colors[5] = Brushes.Yellow;
            Colors[6] = Brushes.Green;
            Colors[7] = Brushes.Aqua;
            Colors[8] = Brushes.Blue;
            Colors[9] = Brushes.DarkViolet;
            Colors[10] = Brushes.White;
            Colors[11] = Brushes.LightSlateGray;
            Colors[12] = Brushes.RosyBrown;
            Colors[13] = Brushes.Plum;
            Colors[14] = Brushes.Orange;
            Colors[15] = Brushes.Bisque;
            Colors[16] = Brushes.LightGreen;
            Colors[17] = Brushes.LightBlue;
            Colors[18] = Brushes.CadetBlue;
            Colors[19] = Brushes.Violet;
            ImageSave = new Stack<TempImage>();
            CurrentColour = new byte[3];
            CurrentColour = new[] {byte.MinValue,byte.MinValue,byte.MinValue};
            Ic = new ImageCommand(this);
            Width = 100;
            Height = 100;
            NewImage();
            ImageStretched = Stretch.Uniform;
        }

        public byte[] GetImageData()
        {
            return ImageData;
        }

        public void SetImageData(byte[] imageData)
        {
            ImageData = imageData;
        }
        public BitmapSource GetFirstImage()
        {
            return FirstImage;
        }

        public void SetFirstImage(BitmapSource firstImage)
        {
            FirstImage = firstImage;
        }

        public int GetBytesPerPixel()
        {
            return BytesPerPixel;
        }

        public void SetBytesPerPixel(int bytesPerPixel)
        {
            BytesPerPixel = bytesPerPixel;
            ;
        }

        public int GetWidth()
        {
            return Width;
        }

        public void SetWidth(int width)
        {
            Width = width;
        }

        public int GetHeight()
        {
            return Height;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }
        public static void StartSavingTemp()
        {
            Ic.SaveImage();
        }

        public int GetStride()
        {
            return Stride;
        }
 
        public void SetStride(int stride)
        {
            Stride = stride;
        }
        private void NewImage()
        {
            ImageData = new byte[Height * Width*3];
            Ic.ClearInit(Width,Height);
            Ic.CreateNewImage(Width,Height);
        }
        public static void PaintPixel(int x, int y)
        {
            Ic.PaintPixel(x,y);
        }
        public static void UndoMove()
        {
            object e = null;
            Buc.Execute(e);
        }

        public int Getptr()
        {
            // ptr = Dicke des Stiftes
            int i = Convert.ToInt32(Ptr);
            return i;
        }
        
        public static void CreateNewImage(int width, int height)
        {
            Ic.ClearInit(width,height);
            Ic.CreateNewImage(width,height);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}