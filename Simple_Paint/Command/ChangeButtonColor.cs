using System;
using System.Windows.Input;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class ChangeButtonColor : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public ChangeButtonColor(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int i = (int) parameter;
            _simplePaintViewModel.CurrentColour[0] = _simplePaintViewModel.Colors[i].Color.B;
            _simplePaintViewModel.CurrentColour[1] = _simplePaintViewModel.Colors[i].Color.G;
            _simplePaintViewModel.CurrentColour[2] = _simplePaintViewModel.Colors[i].Color.R;
        }

        public event EventHandler CanExecuteChanged;
    }
}