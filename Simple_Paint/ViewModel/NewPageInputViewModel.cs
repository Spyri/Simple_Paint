﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
 using System.Windows;
 using Simple_Paint.Annotations;
using Simple_Paint.Command;
using Simple_Paint.View;

namespace Simple_Paint.ViewModel
{
    public class NewPageInputViewModel : INotifyPropertyChanged
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public ButtonCreateCommand b { get; set; }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPageInputViewModel()
        {
            b = new ButtonCreateCommand(this);
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}