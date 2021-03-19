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
using CoinbaseCryprocurrencyRecorderData;

namespace CoinbaseCryptocurrencyRecorder
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public ObservableCollection<CryptocurrencyData> cryptocurrencyList;

        public HomePage()
        {
            InitializeComponent();

            cryptocurrencyList = new ObservableCollection<CryptocurrencyData>();
            //cryptocurrencyDisplayListBox.ItemsSource = cryptocurrencyList;
            cryptocurrencyDisplayListBox.ItemsSource = Client.priceList;

            foreach (string cryptocurrency in ((MainWindow)Application.Current.MainWindow).theSettings.Cryptocurrencies)
            {
                cryptocurrencyList.Add(new CryptocurrencyData(cryptocurrency));
            }
        }
    }
}
