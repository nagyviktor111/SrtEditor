using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SrtEditor.UI.UserControls
{
    public partial class FolderBrowser : UserControl
    {
        public event EventHandler? FolderPathChangedHandler;

        public FolderBrowser()
        {
            InitializeComponent();
        }

        private void FolderBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new();

            if (folderDialog.ShowDialog() == true)
            {
                FolderPath.Text = folderDialog.FolderName;
                FolderPathChangedHandler?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
