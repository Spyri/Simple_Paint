﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        public static byte[] ImageData { get; set; }
        public static System.Windows.Controls.Image ToSave { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int BytesPerPixel { get; set; }
        private int Stride { get; set; }
        private static ImageCommand Ic { get; set; }
        public static byte[] CurrentColour { get; set; }
        public static string Ptr { get; set; }
        public static SolidColorBrush[] Colors { get; private set; }

        public Stack<TempImage> ImageSave { get; private set; }
        public BitmapSource FirstImage { get; set; }
        public static ButtonUndoCommand Buc { get; set; }
        public StretchCommand Sc { get; set; }
        public NewImageCommand NiC { get; set; }
        public ClearCommand ClearCommand { get; set; }
        public OpenFileCommand OpenFileCommand { get; set; }
        public NewPageInput N { get; set; }
        
        public SimplePaintViewModel()
        {
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
            NewImage(100,100);
            ImageStretched = Stretch.Uniform;
        }
        public static void StartSavingTemp()
        {
            Ic.SaveImage();
        }

        public int GetStride()
        {
            return Stride;
        }
        
        public static void NewImage(int width, int height)
        {
            ImageData = new byte[height * width*3];
            Ic.ClearInit();
            Ic.CreateNewImage(width,height);
        }
        public static void PaintPixel(int x, int y)
        {
            Ic.PaintPixel(x,y,Getptr());
        }
        public static void UndoMove()
        {
            object e = null;
            Buc.Execute(e);
        }

        private static int Getptr()
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

        public void SetStride(int stride)
        {
            Stride = stride;
        }
    }
}