using System;
using System.Windows.Input;
using System.Windows.Media;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class StretchCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public StretchCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _simplePaintViewModel.ImageStretched = _simplePaintViewModel.ImageStretched == Stretch.Uniform ? Stretch.Fill : Stretch.Uniform;
        }

        public event EventHandler CanExecuteChanged;
    }
}