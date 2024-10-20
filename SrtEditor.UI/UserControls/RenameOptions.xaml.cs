using System;
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

        private void RenameOptions_Changed(object sender, System.Windows.RoutedEventArgs e)
        {
            OptionsChangedHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
