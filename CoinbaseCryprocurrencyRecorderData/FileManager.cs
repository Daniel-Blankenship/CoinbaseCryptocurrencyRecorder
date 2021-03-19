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
        private string recordedDataPath = "RecordedData.Json";

        private List<string> defaultCryprocurrencyList;
        private Settings defaultSettings;

        // default constructor
        public FileManager()
        {

            defaultCryprocurrencyList = new List<string>()
            {
                "BtcUsd",
                "LtcUsd"
            };

            defaultSettings = new Settings(5, 300, defaultCryprocurrencyList);
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

        public void SaveRecordedData(RecordedData aRecordedDataObject)
        {
            string json = JsonConvert.SerializeObject(aRecordedDataObject, Formatting.Indented);

            // write to the file
            using (StreamWriter sw = File.CreateText(recordedDataPath))
            {
                sw.WriteLine(json);
            }
        }

        public RecordedData LoadRecordedData()
        {
            // if the recorded data file does not exist, return an empty object
            if (!File.Exists(recordedDataPath))
            {
                RecordedData aRecordedDataObject = new RecordedData();
                return aRecordedDataObject;
            }
            else
            {
                string fileData = File.ReadAllText(recordedDataPath);
                RecordedData aRecordedDataObject = JsonConvert.DeserializeObject<RecordedData>(fileData);
                return aRecordedDataObject;
            }
        }


        public string[] ReadFile(string fileName)
        {
            if (fileName != "RecorderData.json" && fileName != "Settings.json")
            {
                // an unknown file name was received
                throw new ArgumentException("An unknown file name was passed to the file reader");
            }

            string[] fileData = System.IO.File.ReadAllLines(fileName);

            return fileData;
        }
    }
}
