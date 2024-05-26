using SrtTimeEditor.BL;
using SrtTimeEditor.BL.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SrtTimeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SrtTimeEditorBL _app;

        public MainWindow()
        {
            InitializeComponent();
            _app = new SrtTimeEditorBL();
        }

        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            var options = BuildOptions();
            var isValid = ValidateOptions(options);

            if (isValid)
            {
                _app.Generate(options);
            }
            else
            {
                MessageBox.Show("Invalid inputs!");
            }
        }

        private bool ValidateOptions(SrtOptions options)
        {
            // TODO
            return false;
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
