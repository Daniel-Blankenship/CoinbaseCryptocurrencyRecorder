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

        private DispatcherTimer updateTimer;
        private DispatcherTimer saveTimer;

        public Client aClient;



        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Coinbase Cryptocurrency Recorder";

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(3);
            updateTimer.Tick += UpdateTimerTicked;
            updateTimer.Start();

            // initialize variables
            theFileManager = new FileManager();
            theSettings = theFileManager.LoadSettings();

            // initialize pages
            theSettingsPage = new SettingsPage();
            theHomePage = new HomePage();

            _mainFrame.Navigate(theHomePage);

            aClient = new Client(theSettings.Cryptocurrencies);
           

        }

        private void UpdateTimerTicked(object sender, EventArgs e)
        {
            theHomePage.cryptocurrencyList = Client.priceList;
            theHomePage.cryptocurrencyDisplayListBox.Items.Refresh();

            foreach (CryptocurrencyData price in theHomePage.cryptocurrencyList)
            {
                Console.WriteLine(price.market + " " + price.price);
            }
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
