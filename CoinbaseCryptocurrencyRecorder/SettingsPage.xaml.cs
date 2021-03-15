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

        public SettingsPage()
        {
            InitializeComponent();
            cryptocurrencyList = new ObservableCollection<Cryptocurrency>();
            cryptocurrenciesListBox.ItemsSource = cryptocurrencyList;
        }

        private void AddCryptocurrency_Click(object sender, RoutedEventArgs e)
        {
            cryptocurrencyList.Add(new Cryptocurrency(addCryptocurrencyBox.Text));
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
    }
}
