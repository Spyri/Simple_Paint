using System;
using System.Windows.Input;
using System.Windows.Media;
using Simple_Paint.View;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class NewImageCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;
        public static NewPageInput i;

        public NewImageCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NewPageInput n = new NewPageInput();
            n.Show();
            i = n;
        }

        public event EventHandler CanExecuteChanged;
    }
}