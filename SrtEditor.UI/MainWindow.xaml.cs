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
        private readonly FileNameEditorRunner _runner;
        public MainWindow()
        {
            InitializeComponent();
            _runner = new FileNameEditorRunner();
            FolderBrowserInstance.FolderPathChangedHandler += UpdatePreview!;
            RenameOptionsInstance.OptionsChangedHandler += UpdatePreview!;
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
                    FolderBrowserInstance.FolderPath.Text = droppedPaths[0];
                    UpdatePreview(sender, e);
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
                _runner.Run(options);
                MessageBox.Show("Done!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void UpdatePreview(object sender, EventArgs e)
        {
            try
            {
                var options = BuildOptions();
                var preview = _runner.GetNewNames(options);
                PreviewListBox.ItemsSource = BuildItemSource(preview);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static List<string> BuildItemSource(IEnumerable<RenameItem> preview)
        {
            var list = new List<string>();

            foreach (RenameItem item in preview)
            {
                list.Add("-- " + item.OldName);
                list.Add("+ " + item.NewName);
                list.Add("*************************");
            }

            return list;
        }

        private NameEditorOptions BuildOptions()
        {
            return new NameEditorOptions
            {
                FolderPath = FolderBrowserInstance.FolderPath.Text,
                CopyVideoNames = RenameOptionsInstance.CopyCheckBox.IsChecked == true,
                ExtendFileNames = RenameOptionsInstance.ExtendCheckBox.IsChecked == true,
                Extension = RenameOptionsInstance.Extension.Text,
            };
        }
    }
}
