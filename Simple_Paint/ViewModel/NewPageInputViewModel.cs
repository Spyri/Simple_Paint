﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
 using Simple_Paint.Command;
 using Simple_Paint.Properties;

 namespace Simple_Paint.ViewModel
{
    public class NewPageInputViewModel : INotifyPropertyChanged
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public ButtonCreateCommand B { get; set; }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPageInputViewModel()
        {
            B = new ButtonCreateCommand(this);
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}