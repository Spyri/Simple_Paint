using System;
using System.Windows;
using System.Windows.Input;
using Simple_Paint.View;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class OpenSaveWindowCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public OpenSaveWindowCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SaveInput save = new SaveInput();
            SimplePaintViewModel.ToSave = _simplePaintViewModel.Imagesource;
            save.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}