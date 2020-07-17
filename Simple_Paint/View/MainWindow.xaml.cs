﻿using System;
 using System.Text.RegularExpressions;
using System.Windows;
 using System.Windows.Controls;
 using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
 using Simple_Paint.Command;
 using Simple_Paint.ViewModel;

 namespace Simple_Paint.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                Button newBtn = new Button
                {
                    Name = "b" +i.ToString(),
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(SimplePaintViewModel.Colors[i].Color.R,SimplePaintViewModel.Colors[i].Color.G,SimplePaintViewModel.Colors[i].Color.B)),
                };
                if (i > 9)
                {
                    Grid.SetRow(newBtn, 1);
                    Grid.SetColumn(newBtn, i - 10);
                }
                else
                {
                    Grid.SetRow(newBtn,0);
                    Grid.SetColumn(newBtn,i);
                }

                newBtn.Click += ColourButton_OnClick;
                ColorPanel.Children.Add(newBtn);
                

            }
            RoutedCommand cmdredo = new RoutedCommand();
            cmdredo.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdredo, ReturnMove_OnClick));
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Cursor = Cursors.Pen;
                ImageSource bit = image.Source; 
                BitmapSource bitmapSource = (BitmapSource) bit;
                int x = (int) (e.GetPosition(image).X * bitmapSource.PixelWidth / image.ActualWidth);
                int y = (int) (e.GetPosition(image).Y * bitmapSource.PixelHeight / image.ActualHeight);
                SimplePaintViewModel.PaintPixel(x, y);
            }
        }


        private void ColourButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            int index =  Convert.ToInt32(b.Name.Substring(1,b.Name.Length-1));
            
            SimplePaintViewModel.CurrentColour[0] = SimplePaintViewModel.Colors[index].Color.B;
            SimplePaintViewModel.CurrentColour[1] = SimplePaintViewModel.Colors[index].Color.G;
            SimplePaintViewModel.CurrentColour[2] = SimplePaintViewModel.Colors[index].Color.R;
        }
        
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckIfInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ReturnMove_OnClick(object sender, RoutedEventArgs e)
        {
            SimplePaintViewModel.UndoMove();
        }

        private void SaveTemp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SimplePaintViewModel.StartSavingTemp();
        }
        
    }
}