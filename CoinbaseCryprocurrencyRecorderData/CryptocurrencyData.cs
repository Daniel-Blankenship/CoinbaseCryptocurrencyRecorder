using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinbasePro;
using CoinbasePro.Shared.Types;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class CryptocurrencyData
    {
        // constructor
        public CryptocurrencyData(string newMarket)
        {
            market = newMarket;
        }

        // checks if the cryptocurrency market string matches a product enumerable used by the CoinbasePro library
        public bool parseProduct()
        {
            ProductType parsedProduct;

            if (Enum.TryParse(market, out parsedProduct) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string timeReceived { get; set; }
        public string market { get; set; }
        public decimal price { get; set; }
    }

    
}
