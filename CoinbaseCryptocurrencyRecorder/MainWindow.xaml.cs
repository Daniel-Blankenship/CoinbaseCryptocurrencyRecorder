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
using CoinbaseCryprocurrencyRecorderData;

namespace CoinbaseCryptocurrencyRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsPage theSettingsPage;
        Settings theSettings;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Coinbase Cryptocurrency Recorder";

            // initialize pages
            theSettingsPage = new SettingsPage();

            
        }

        private void Settings_Click(object sender, System.EventArgs e)
        {
            // used to set the displayed page
            _mainFrame.Navigate(theSettingsPage);
        }

        private void NavigateEvent(object sender, System.EventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }
    }
}
