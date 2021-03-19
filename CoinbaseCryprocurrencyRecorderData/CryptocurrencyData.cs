using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class CryptocurrencyData
    {
        // constructor
        public CryptocurrencyData(string newMarket)
        {
            market = newMarket;
        }

        public string timeReceived { get; set; }
        public string market { get; set; }
        public decimal price { get; set; }
    }
}
