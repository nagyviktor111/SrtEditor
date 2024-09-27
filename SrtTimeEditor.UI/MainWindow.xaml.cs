using Microsoft.Win32;
using SrtTimeEditor.Domain;
using SrtTimeEditor.Program;
using System;
using System.IO;
using System.Windows;

namespace SrtTimeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SrtTimeEditorRunner _app;

        public MainWindow()
        {
            InitializeComponent();
            _app = new SrtTimeEditorRunner();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var options = BuildOptions();

            if (_app.IsValid(options))
            {
                _app.Run(options);
                MessageBox.Show("Done!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please check your values inputs!", "Invalid inputs", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Subtitles (*.srt) | *.srt",
                Multiselect = false // for now
            };
            
            bool ? result = openFileDialog.ShowDialog();
            FilePaths.Text = result == true ? openFileDialog.FileName : string.Empty;
        }

        private void MainGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;
        }

        private void MainGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (droppedFiles != null && droppedFiles.Length > 0)
                {
                    string firstFile = droppedFiles[0];
                    string extension = Path.GetExtension(firstFile).ToLower();

                    if (extension == ".srt")
                    {
                        FilePaths.Text = firstFile;
                    }
                    else
                    {
                        MessageBox.Show("Please drop a .srt file only.", "Invalid File Type", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private SrtOptions BuildOptions()
        {
            return new SrtOptions
            {
                CreatedFileLocation = (bool)OverwriteOriginal.IsChecked!
                    ? CreatedFileLocation.OverwriteOriginal
                    : CreatedFileLocation.SameFolderDifferentName,
                TimeScaleDifference = new TimeScaleDifference 
                {
                    Movie1 = TimeSpan.Parse(Movie1.Text),
                    Movie2 = TimeSpan.Parse(Movie2.Text),
                    Subtitle1 = TimeSpan.Parse(Subtitle1.Text),
                    Subtitle2 = TimeSpan.Parse(Subtitle2.Text)
                },
                Delay = double.Parse(Delay.Text),
                FilePaths = FilePaths.Text,
            };
        }
    }
}
