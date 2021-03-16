using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this class is used for holding settings data
namespace CoinbaseCryprocurrencyRecorderData
{
    public class Settings
    {
        // variables backing properties
        private int _updateInterval;
        private int _saveInterval;

        // constructor
        public Settings(int updateInterval, int saveInterval, List<string> cryptocurrencies)
        {
            if ( cryptocurrencies == null || cryptocurrencies.Count < 1)
            {
                throw new ArgumentNullException("cryptocurrencies can not be empty or null.");
            }

            UpdateInterval = updateInterval;
            SaveInterval = saveInterval;
            Cryptocurrencies = cryptocurrencies;
        }

        // properties
        public List<string> Cryptocurrencies { get; set; }

        // custom properties
        public int UpdateInterval
        {
            get { return _updateInterval; }
            set
            {
                if (value < 10)
                {
                    throw new ArgumentOutOfRangeException("updateInterval cannot be less than 10 seconds");
                }

                _updateInterval = value;
            }
        }

        public int SaveInterval
        {
            get { return _saveInterval; }
            set
            {
                if (value < 10)
                {
                    throw new ArgumentOutOfRangeException("saveInterval cannot be less than 10 seconds");
                }

                _saveInterval = value;
            }
        }

    }
}
