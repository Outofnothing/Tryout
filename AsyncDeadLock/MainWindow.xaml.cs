using System.Windows;

namespace AsyncDeadLock
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

        private void _button_Click(object sender, RoutedEventArgs e)
        {
            // Deadlock
            Task task = WaitAsync();
            task.Wait();
        }

        async Task WaitAsync()
        {
            await Task.Delay(1000);
        }
    }
}