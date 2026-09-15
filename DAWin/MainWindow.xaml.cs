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
using DAWin.Core;
using DAWin.Audio;

namespace DAWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AudioEngine audioEngine_;
        public MainWindow()
        {
         
            InitializeComponent();

            audioEngine_ = new AudioEngine();

            // Logging
            Logger.OnLog += HandleLogMessage;

            // Engine initialization
            audioEngine_.InitializeAudioEngine();
            AudioDeviceCB.ItemsSource = audioEngine_.AudioDevices;
        }

        private void HandleLogMessage(string message) 
        {
            ConsoleOutput.Text += Environment.NewLine + message;

            ConsoleScrollViewer.ScrollToEnd();
        }
        private void ClearConsole_Click(Object sender, RoutedEventArgs e) 
        {
            ConsoleOutput.Text = string.Empty;
        }
        private void DetectDevices_Click(Object sender, RoutedEventArgs e) 
        {
            audioEngine_.DetectAudioDevices();
        }
        private void InitializeDevice_Click(Object sender, RoutedEventArgs e) 
        {
            AudioDevice? selectedDevice = AudioDeviceCB.SelectedItem as AudioDevice;

            if (selectedDevice == null) 
            {
                Logger.LogWarning("No audio devices selected.", 2200);
                return;
            }
            audioEngine_.InitializeActiveDevice(selectedDevice);
        }
    }
}