using SrtTimeEditor.Domain;
using SrtTimeEditor.Program;
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

        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            var options = BuildOptions();

            if (_app.IsValid(options))
            {
                _app.Run(options);
            }
            else
            {
                MessageBox.Show("Invalid inputs!");
            }
        }

        private SrtOptions BuildOptions()
        {
            return new SrtOptions
            {
                // TODO
            };
        }
    }
}
