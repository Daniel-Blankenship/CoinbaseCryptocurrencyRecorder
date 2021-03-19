using CoinbasePro;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class Client
    {
        private CoinbaseProClient theCoinBaseProClient;

        private IWebSocket theWebSocket;

        private List<ProductType> theProductTypes;
        private List<ChannelType> theChanelTypes;

        public static ObservableCollection<CryptocurrencyData> priceList = new ObservableCollection<CryptocurrencyData>();


        // constructor
        public Client(List<string> cryptocurrencies)
        {
            // builds the pricelist using a list of cryprocurrency market strings
            foreach (string cryptocurrency in cryptocurrencies)
            {
                CryptocurrencyData newCryptocurrencyData = new CryptocurrencyData(cryptocurrency);
                priceList.Add(newCryptocurrencyData);
            }

            // add the ticker channel to the channel list
            theChanelTypes = new List<ChannelType>() { ChannelType.Ticker };

            theProductTypes = parseProducts(cryptocurrencies);

            theCoinBaseProClient = new CoinbasePro.CoinbaseProClient();

            theWebSocket = theCoinBaseProClient.WebSocket;


            theWebSocket.Start(theProductTypes, theChanelTypes);

            // event handler for ticker received
            theWebSocket.OnTickerReceived += WebSocket_OnTickerReceived;
        }

        // converts a list of cryptocurrency market setings to enumerables used by the CoinbasePro library
        private List<ProductType> parseProducts(List<string> cryptocurrencies)
        {
            List<ProductType> parsedProducts = new List<ProductType>();
            ProductType parsedProduct;

            foreach (string cryptocurrency in cryptocurrencies)
            {
                if (Enum.TryParse(cryptocurrency, out parsedProduct) == true)
                {
                    parsedProducts.Add(parsedProduct);
                }
                else
                {
                    throw new ArgumentException("The cryptocurrency: " + cryptocurrency + " is not recognized");
                }
            }

            return parsedProducts;
        }

        private static void WebSocket_OnTickerReceived(object sender, WebfeedEventArgs<Ticker> e)
        {
            // find the index in the price list of the market returned from the websocket
            int index = -1;
            for(int i = 0; i < priceList.Count; i++)
            {
                if (priceList[i].market == e.LastOrder.ProductId.ToString())
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new ArgumentException("A market: " + e.LastOrder.ProductId.ToString() +
                    " received from the websocket was not found in the price list");
            }

            // update the price
            priceList[index].price = e.LastOrder.Price;
        }
    }
}
