using System.Text.RegularExpressions;
using System.Windows;
 using System.Windows.Controls;
 using System.Windows.Input;
using System.Windows.Media;
using Simple_Paint.ViewModel;

 namespace Simple_Paint.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            
            InitializeComponent();
            SimplePaintViewModel f = DataContext as SimplePaintViewModel;
            for (int i = 0; i < 20; i++)
            {
                Button newBtn = new Button
                {
                    Name = "b" +i.ToString(),
                    Background = new SolidColorBrush(Color.FromRgb(f.Colors[i].Color.R,f.Colors[i].Color.G,f.Colors[i].Color.B)),
                };
                if (i > 9)
                {
                    Grid.SetRow(newBtn, 1);
                    Grid.SetColumn(newBtn, i - 10);
                }
                else
                {
                    Grid.SetRow(newBtn,0);
                    Grid.SetColumn(newBtn,i);
                }
                newBtn.Command = f.Cbc;
                newBtn.CommandParameter = i;
                ColorPanel.Children.Add(newBtn);
            }
            KeyGesture undo = new KeyGesture(Key.Z, ModifierKeys.Control);
            KeyBinding undoBinding = new KeyBinding(f.Buc, undo);
            this.InputBindings.Add(undoBinding);
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckIfInputIsNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}