using System;
using System.Windows;
using System.Windows.Input;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class ButtonCreateCommand : ICommand
    {
        private readonly NewPageInputViewModel _newPageInputViewModel;

        public ButtonCreateCommand(NewPageInputViewModel newPageInputViewModel)
        {
            _newPageInputViewModel = newPageInputViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SimplePaintViewModel.CreateNewImage(_newPageInputViewModel.Width,_newPageInputViewModel.Height);
            Application.Current.Windows[1]?.Close();
        }

        public event EventHandler CanExecuteChanged;
    }
}