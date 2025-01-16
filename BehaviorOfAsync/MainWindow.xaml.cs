using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BehaviorOfAsync
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

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            TryOpenFile();
        }

        private async void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            TryReadFile();
        }

        private async Task TryOpenFile()
        {
            using var file = File.OpenWrite("file.txt");
            await Task.Delay(2000);
            await file.WriteAsync(Encoding.UTF8.GetBytes("Hello, World!"));
        }

        private async Task TryReadFile()
        {
            using var file = File.OpenRead("file.txt");
            var buffer = new byte[file.Length];
            await file.ReadAsync(buffer, 0, buffer.Length);
            MessageBox.Show(Encoding.UTF8.GetString(buffer));
        }
    }
}