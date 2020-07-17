using System;
using System.Windows.Input;
using Simple_Paint.View;

namespace Simple_Paint.Command
{
    public class NewImageCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NewPageInput n = new NewPageInput();
            n.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}