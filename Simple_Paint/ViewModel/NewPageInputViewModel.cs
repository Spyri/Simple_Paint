﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Simple_Paint.Annotations;
using Simple_Paint.Command;
using Simple_Paint.View;

namespace Simple_Paint.ViewModel
{
    public class NewPageInputViewModel : INotifyPropertyChanged
    {
        public readonly SimplePaintViewModel _simplePaintViewModel;

        public NewPageInputViewModel(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
            
        }

        public int Width { get; set; }
        public int Height { get; set; }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPageInputViewModel()
        {
            
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}