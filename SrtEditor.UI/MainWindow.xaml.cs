using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace SrtEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new();

            if (folderDialog.ShowDialog() == true)
            {
                FolderPath.Text = folderDialog.FolderName;
            }
        }

        private void RenamerPage_PreviewDragOver(object sender, DragEventArgs e)
        {
            var dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            var droppedPaths = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effects = dataPresent && droppedPaths.Length == 1 && Directory.Exists(droppedPaths[0])
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            e.Handled = true;
        }

        private void RenamerPage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var droppedPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (droppedPaths.Length == 1 && Directory.Exists(droppedPaths[0]))
                {
                    FolderPath.Text = droppedPaths[0];
                }
                else
                {
                    MessageBox.Show("Please drop only one folder.", "Invalid Drop", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
