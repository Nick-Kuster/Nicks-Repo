using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using XMLToJSON.BLL;
using XMLToJSON.UserControls;
using Path = System.IO.Path;

namespace XMLToJSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string SettingsFileName = "XMLToJsonUserSettings.config";

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        public ICommand OpenFileDialogCommand
        {
            get;set;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UserSettingsManager<XMLToJSONUserSettings> settingsManager = new UserSettingsManager<XMLToJSONUserSettings>(SettingsFileName);
            
            XMLToJSONUserSettings settings = settingsManager.LoadSettings();

            FilePath.Text = settings?.FilePath;
            Endpoint.Text = settings?.Endpoint;
        }

        public void OpenFileDialog()
        {
            Console.Write("hi");
        }

        public bool OpenFileDialogCanExecute()
        {
            return true;
        }

        private void OpenFileDialogClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialg();
        }

        private void SaveSettingsClicked(object sender, RoutedEventArgs e)
        {
            UserSettingsManager<XMLToJSONUserSettings> settingsManager = new UserSettingsManager<XMLToJSONUserSettings>(SettingsFileName);
            XMLToJSONUserSettings settings = new XMLToJSONUserSettings()
            {
                FilePath = FilePath.Text,
                Endpoint = Endpoint.Text
            };
            settingsManager.SaveSettings(settings);
        }

        private async void SendToEndpointClicked(object sender, RoutedEventArgs e)
        {
            XMLToJSONManager xmlToJSON = new XMLToJSONManager();
            LoadingPanel.Visibility = Visibility.Visible;
            await xmlToJSON.SendToEndpoint();
            xmlToJSON.SaveJSONToXML();
            LoadingPanel.Visibility = Visibility.Collapsed;
        }

        private void OpenFileDialg()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FilePath.Text == string.Empty 
                            ? "C:\\" 
                            : Path.GetDirectoryName(FilePath.Text);
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath.Text = openFileDialog.FileName;
            }            
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
