using Microsoft.Win32;
using SrtEditor.Domain.NameEditor;
using SrtEditor.NameEditor;
using System;
using System.Collections.Generic;
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

        private void FolderBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new();

            if (folderDialog.ShowDialog() == true)
            {
                FolderPath.Text = folderDialog.FolderName;
            }
        }

        private void NameEditorPage_PreviewDragOver(object sender, DragEventArgs e)
        {
            var dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            var droppedPaths = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effects = dataPresent && droppedPaths.Length == 1 && Directory.Exists(droppedPaths[0])
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            e.Handled = true;
        }

        private void NameEditorPage_Drop(object sender, DragEventArgs e)
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

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            RenameButton.IsEnabled = false;
            try
            {
                var options = BuildOptions();

                // test
                var runner = new FileNameEditorRunner();
                var preview = runner.GetNewNames(options);
                PreviewListBox.ItemsSource = BuildItemSource(preview);
                // end test

                //MessageBox.Show("Done!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                RenameButton.IsEnabled = true;
            }
        }

        private static List<string> BuildItemSource(IEnumerable<RenameItem> preview)
        {
            var list = new List<string>();

            foreach (RenameItem item in preview)
            {
                list.Add("-- " + item.OldName);
                list.Add("+ " + item.NewName);
            }

            return list;
        }

        private NameEditorOptions BuildOptions()
        {
            return new NameEditorOptions
            {
                FolderPath = FolderPath.Text,
                CopyVideoNames = CopyCheckBox.IsChecked == true,
                ExtendFileNames = ExtendCheckBox.IsChecked == true,
                Extension = Extension.Text,
            };
        }
    }
}
