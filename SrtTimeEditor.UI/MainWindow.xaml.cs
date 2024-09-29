using Microsoft.Win32;
using SrtTimeEditor.Domain;
using SrtTimeEditor.Program;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SrtTimeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateButton.IsEnabled = false;
            try
            {
                var options = BuildOptions();
                var runner = new SrtTimeEditorRunner(options);
                runner.Run();
                MessageBox.Show("Done!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                GenerateButton.IsEnabled = true;
            }
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
                FilePaths = FilePaths.Text
            };
        }
    }
}
