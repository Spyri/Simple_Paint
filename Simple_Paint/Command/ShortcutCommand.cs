using System;
using System.Windows.Input;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class ShortcutCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public ShortcutCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            else SimplePaintViewModel.RedoMove();
        }

        public event EventHandler CanExecuteChanged;
    }
}