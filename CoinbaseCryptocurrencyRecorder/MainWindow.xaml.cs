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
using System.Windows.Threading;
using CoinbaseCryprocurrencyRecorderData;

namespace CoinbaseCryptocurrencyRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileManager theFileManager;

        SettingsPage theSettingsPage;
        HomePage theHomePage;

        public Settings theSettings;
        public RecordedData theRecordedData;

        private DispatcherTimer updateTimer;
        private DispatcherTimer saveTimer;

        public Client theClient;



        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Coinbase Cryptocurrency Recorder";

            // initialize variables
            theFileManager = new FileManager();
            theSettings = theFileManager.LoadSettings();

            // initialize pages
            theSettingsPage = new SettingsPage();
            theHomePage = new HomePage();

            _mainFrame.Navigate(theHomePage);

            theClient = new Client(theSettings.Cryptocurrencies);
            theRecordedData = new RecordedData();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(theSettings.UpdateInterval);
            updateTimer.Tick += UpdateTimerTicked;
            updateTimer.Start();

            saveTimer = new DispatcherTimer();
            saveTimer.Interval = TimeSpan.FromSeconds(theSettings.SaveInterval);
            saveTimer.Tick += SaveTimerTicked;
            saveTimer.Start();


        }

        private void UpdateTimerTicked(object sender, EventArgs e)
        {
            theHomePage.cryptocurrencyList = Client.priceList;
            theHomePage.cryptocurrencyDisplayListBox.Items.Refresh();
        }

        private void SaveTimerTicked(object sender, EventArgs e)
        {
            theRecordedData = theFileManager.LoadRecordedData();

            foreach (CryptocurrencyData price in theHomePage.cryptocurrencyList)
            {
                theRecordedData.updateRecordedData(price);
            }

            theFileManager.SaveRecordedData(theRecordedData);
        }

        // make changes to reflect new settings
        public void UpdateSettings()
        {
            // update timer intervals
            updateTimer.Interval = TimeSpan.FromSeconds(theSettings.UpdateInterval);
            saveTimer.Interval = TimeSpan.FromSeconds(theSettings.SaveInterval);

            // update the client's cryptocurrency list
            theClient.UpdateCryptocurrencyList(theSettings.Cryptocurrencies);

            Console.WriteLine("Settings updated");
        }

        private void Settings_Click(object sender, System.EventArgs e)
        {
            _mainFrame.Navigate(theSettingsPage);
        }

        private void Home_Click(object sender, System.EventArgs e)
        {
            _mainFrame.Navigate(theHomePage);
        }

        private void NavigateEvent(object sender, System.EventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }
    }
}
