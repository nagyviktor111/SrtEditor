using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace SrtEditor.UI.UserControls
{
    public partial class SrtFileBrowser : UserControl
    {
        public SrtFileBrowser()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Subtitles (*.srt) | *.srt"
            };

            bool? result = openFileDialog.ShowDialog();
            FilePaths.Text = result == true ? openFileDialog.FileName : string.Empty;
        }
    }
}
