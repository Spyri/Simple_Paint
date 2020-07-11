using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Simple_Paint.ViewModel;

namespace Simple_Paint.View
{
    public partial class NewPageInput : Window
    {
        public NewPageInput()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            int width = Convert.ToInt32(Width.Text);
            int height = Convert.ToInt32(Height.Text);
            SimplePaintViewModel.NewImage(width,height);
            Close();
        }
    }
}