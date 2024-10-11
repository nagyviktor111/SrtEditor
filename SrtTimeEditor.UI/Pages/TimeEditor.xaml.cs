using SrtTimeEditor.Domain;
using SrtTimeEditor.Program;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SrtTimeEditor.UI.Pages
{
    public partial class TimeEditor : Page
    {
        public TimeEditor()
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
                        SrtFileBrowserInstance.FilePaths.Text = firstFile;
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
                CreatedFileLocation = (bool)CreatedFileNameEditorInstance.OverwriteOriginal.IsChecked!
                    ? CreatedFileLocation.OverwriteOriginal
                    : CreatedFileLocation.SameFolderDifferentName,
                TimeScaleDifference = new TimeScaleDifference
                {
                    Movie1 = TimeSpan.Parse(TimeScaleEditorInstance.Movie1.Text),
                    Movie2 = TimeSpan.Parse(TimeScaleEditorInstance.Movie2.Text),
                    Subtitle1 = TimeSpan.Parse(TimeScaleEditorInstance.Subtitle1.Text),
                    Subtitle2 = TimeSpan.Parse(TimeScaleEditorInstance.Subtitle2.Text)
                },
                Delay = double.Parse(DelayInputInstance.Delay.Text),
                FilePaths = SrtFileBrowserInstance.FilePaths.Text
            };
        }
    }
}
