using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SrtEditor.UI.UserControls
{
    public partial class DelayInput : UserControl
    {
        public DelayInput()
        {
            InitializeComponent();
        }

        private void Delay_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void Delay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
                textBox.SelectAll();
            }
        }
    }
}
