using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class RecordedData
    {
        public List<List<CryptocurrencyData>> theRecordedData { set;  get; }

        // constructor
        public RecordedData()
        {
            theRecordedData = new List<List<CryptocurrencyData>>();
        }

        public void updateRecordedData(CryptocurrencyData newData)
        {
            bool dataAdded = false;
            foreach (var item in theRecordedData)
            {
                if (item[0].market == newData.market)
                {
                    item.Add(newData);
                    dataAdded = true;
                    break;
                }
            }

            if (dataAdded == false)
            {
                // the recorded data does not yet have a list for this currency type
                List<CryptocurrencyData> newCryptocurrencyList = new List<CryptocurrencyData>();
                newCryptocurrencyList.Add(newData);

                theRecordedData.Add(newCryptocurrencyList);
            }
        }
    }
}
