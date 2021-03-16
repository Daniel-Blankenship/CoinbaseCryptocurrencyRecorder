using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class FileManager
    {
        private string settingsPath = "Settings.Json";

        private List<string> defaultCryprocurrencyList;
        private Settings defaultSettings;

        // default constructor
        public FileManager()
        {
            defaultCryprocurrencyList = new List<string>()
            {
                "BTC-USD",
                "LTC-USD"
            };

            defaultSettings = new Settings(300, 300, defaultCryprocurrencyList);
        }

        public void SaveSettings(Settings aSettingsObject)
        {
            string json = JsonConvert.SerializeObject(aSettingsObject, Formatting.Indented);

            // write to the file
            using (StreamWriter sw = File.CreateText(settingsPath))
            {
                sw.WriteLine(json);
            }
        }

        public Settings LoadSettings()
        {
            // if the settings file does not exist, create a default settings file
            if(!File.Exists(settingsPath))
            {
                SaveSettings(defaultSettings);
                return defaultSettings;
            }
            else
            {
                string fileData = File.ReadAllText(settingsPath);
                Settings aSettingsObject = JsonConvert.DeserializeObject<Settings>(fileData);
                return aSettingsObject;
            }
        }

        public string[] ReadFile(string fileName)
        {
            if (fileName != "API.json" && fileName != "RecorderData.json" && fileName != "Settings.json")
            {
                // an unknown file name was received
                throw new ArgumentException("An unknown file name was passed to the file reader");
            }

            // System.IO.File.Exists
            // FileData = System.IO.File.ReadAllLines(@"Ideas.txt");
            string[] fileData = System.IO.File.ReadAllLines(fileName);

            // view data that was read in console for testing purposes
            foreach (string line in fileData)
            {
                Console.WriteLine(line);
            }

            return fileData;
        }
    }


}
