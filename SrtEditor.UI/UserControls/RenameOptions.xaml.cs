using System;
using System.Windows;
using System.Windows.Controls;

namespace SrtEditor.UI.UserControls
{
    public partial class RenameOptions : UserControl
    {

        public event EventHandler? OptionsChangedHandler;

        public RenameOptions()
        {
            InitializeComponent();
        }

        private void RenameOptions_Changed(object sender, RoutedEventArgs e)
        {
            OptionsChangedHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
