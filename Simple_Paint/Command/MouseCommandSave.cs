using System;
using System.Windows.Input;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class MouseCommandSave : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public MouseCommandSave(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource, _simplePaintViewModel.GetStride()));
        }

        public event EventHandler CanExecuteChanged;
    }
}