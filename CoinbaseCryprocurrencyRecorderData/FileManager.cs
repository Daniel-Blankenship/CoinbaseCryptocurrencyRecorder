using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseCryprocurrencyRecorderData
{
    public class FileManager
    {
        // default constructor
        public FileManager()
        {

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
