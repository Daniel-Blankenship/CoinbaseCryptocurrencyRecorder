using CoinbaseCryprocurrencyRecorderData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CoinbaseCryptocurrencyRecorder
{
    public class Cryptocurrency
    {
        public string market { get; set; }

        public Cryptocurrency(string newMarket)
        {
            market = newMarket;
        }

    }

    public partial class SettingsPage : Page
    {
        private ObservableCollection<Cryptocurrency> cryptocurrencyList;

        private string addCryptocurrencyBoxDefaultText = "Add new markets here";

        private DispatcherTimer timer;
        private FileManager fileManagerObject;

        public SettingsPage()
        {
            InitializeComponent();

            saveLoadLabel.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += TimerTicked;

            cryptocurrencyList = new ObservableCollection<Cryptocurrency>();
            cryptocurrenciesListBox.ItemsSource = cryptocurrencyList;

            addCryptocurrencyBox.Text = addCryptocurrencyBoxDefaultText;
            settingsInfoLabel.Content = "";

            fileManagerObject = new FileManager();

            foreach (string cryptocurrency in ((MainWindow)Application.Current.MainWindow).theSettings.Cryptocurrencies)
            {
                cryptocurrencyList.Add(new Cryptocurrency(cryptocurrency));
            }

            updateIntervalBox.Text = ((MainWindow)Application.Current.MainWindow).theSettings.UpdateInterval.ToString();
            saveIntervalBox.Text = ((MainWindow)Application.Current.MainWindow).theSettings.SaveInterval.ToString();
        }

        private void AddCryptocurrency_Click(object sender, RoutedEventArgs e)
        {
            CryptocurrencyData aCryptocurrencyDataObject = new CryptocurrencyData(addCryptocurrencyBox.Text);

            if (addCryptocurrencyBox.Text == addCryptocurrencyBoxDefaultText)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "To add a cryptocurrency, you must type it into the add new market field";
                return;
            }
            else if (addCryptocurrencyBox.Text.Length > 10)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: A cryptocurrency market shouldn't be longer than 10 characters";
                return;
            }
            else if (addCryptocurrencyBox.Text.Length < 1)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The add new market field was left empty";
                return;
            }
            else if (aCryptocurrencyDataObject.parseProduct() == false)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The market is not supported";
                return;
            }

            cryptocurrencyList.Add(new Cryptocurrency(addCryptocurrencyBox.Text));
            addCryptocurrencyBox.Text = "";

            settingsInfoLabel.Visibility = Visibility.Hidden;
        }

        private void AddCryptocurrencyBox_Click(object sender, RoutedEventArgs e)
        {
            // clear the default text from the box when it is clicked so it is ready to accept information
            if (addCryptocurrencyBox.Text == addCryptocurrencyBoxDefaultText)
            {
                addCryptocurrencyBox.Text = "";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                Cryptocurrency aCryptocurrency = button.DataContext as Cryptocurrency;
                cryptocurrencyList.Remove(aCryptocurrency);
            }
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // if the settings fields have invalid data, do not save it
            if (!verifyFields())
            {
                return;
            }

            // converts the observable list of Cryptocurrency to a list of String
            List<string> listToSave = new List<string>();
            foreach(Cryptocurrency i in cryptocurrencyList)
            {
                listToSave.Add(i.market);
            }

            // updates the settings object with the new data
            ((MainWindow)Application.Current.MainWindow).theSettings.Cryptocurrencies = listToSave;
            ((MainWindow)Application.Current.MainWindow).theSettings.SaveInterval = int.Parse(saveIntervalBox.Text);
            ((MainWindow)Application.Current.MainWindow).theSettings.UpdateInterval = int.Parse(updateIntervalBox.Text);

            fileManagerObject.SaveSettings(((MainWindow)Application.Current.MainWindow).theSettings);
            ((MainWindow)Application.Current.MainWindow).UpdateSettings();


            saveLoadLabel.Content = "Settings Saved";
            saveLoadLabel.Visibility = Visibility.Visible;
            timer.Start();

            settingsInfoLabel.Visibility = Visibility.Hidden;
        }

        private void ReloadSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // load the settings from Settings.json
            ((MainWindow)Application.Current.MainWindow).theSettings = fileManagerObject.LoadSettings();

            // update the cryptocurrency list with the loaded settings
            cryptocurrencyList.Clear();
            foreach (string i in ((MainWindow)Application.Current.MainWindow).theSettings.Cryptocurrencies)
            {
                Cryptocurrency aNewCryptocurrency = new Cryptocurrency(i);
                cryptocurrencyList.Add(aNewCryptocurrency);
            }

            // update the settings fields with the loaded settings
            saveIntervalBox.Text = ((MainWindow)Application.Current.MainWindow).theSettings.SaveInterval.ToString();
            updateIntervalBox.Text = ((MainWindow)Application.Current.MainWindow).theSettings.UpdateInterval.ToString();

            ((MainWindow)Application.Current.MainWindow).UpdateSettings();

            saveLoadLabel.Content = "Settings loaded from file";
            saveLoadLabel.Visibility = Visibility.Visible;
            timer.Start();
        }

        private void TimerTicked(object sender, EventArgs e)
        {
            saveLoadLabel.Visibility = Visibility.Hidden;
            timer.Stop();
        }

        // checks fields for valid data, and notifies the user if something is wrong
        private bool verifyFields()
        {
            // check update interval field
            if (!updateIntervalBox.Text.All(Char.IsDigit))
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The update interval field must only contain digits";
                return false;
            }
            else if (updateIntervalBox.Text.Length > 10)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The update interval shouldn't be longer than 10 numbers";
                return false;
            }
            else if (updateIntervalBox.Text.Length < 1)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The update interval field was left empty";
                return false;
            }

            // check save interval field
            if (!saveIntervalBox.Text.All(Char.IsDigit))
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The save interval field must only contain digits";
                return false;
            }
            else if (saveIntervalBox.Text.Length > 10)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The save interval shouldn't be longer than 10 numbers";
                return false;
            }
            else if (saveIntervalBox.Text.Length < 1)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "Invalid data entered: The save interval field was left empty";
                return false;
            }

            // make sure there is at least one item in the cryptocurrency list
            if (cryptocurrencyList.Count < 1)
            {
                settingsInfoLabel.Visibility = Visibility.Visible;
                settingsInfoLabel.Content = "There needs to be at least one market in the markets list";
                return false;
            }

            return true;
        }
    }
}
