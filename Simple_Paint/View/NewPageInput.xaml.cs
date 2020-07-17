using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Simple_Paint.View
{
    public partial class NewPageInput
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
        
    }
}