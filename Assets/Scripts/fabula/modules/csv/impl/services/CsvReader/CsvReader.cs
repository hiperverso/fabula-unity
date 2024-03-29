using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace fabula.modules.csv.impl.services.csvreader
{
    public class CsvReader
    {
        public static Dictionary<string, List<string>> GetKeyValues(string _csvFilePath)
        {
            var dataFromFile = File.ReadAllLines(_csvFilePath, System.Text.Encoding.UTF8);
            Dictionary<string, List<string>> keyValues = new();

            foreach(string dataInLine in dataFromFile)
            {
                string[] splittedLine = dataInLine.Split(",");

                List<string> textCollection = new List<string>();
 
                for (int _indText = 1; _indText < splittedLine.Length; _indText++)
                {
                    textCollection.Add(splittedLine[_indText]);
                }

                keyValues.Add(splittedLine[0], textCollection);
            }

            return keyValues;
        }

        public static Dictionary<string, List<string>> GetKeyValues(string _csvFilePath, string _removeKey)
        {
            var cleanedCollection = CsvReader.GetKeyValues(_csvFilePath);
            cleanedCollection.Remove(_removeKey);

            return cleanedCollection;
        }

        public static List<string> GetHeaderInfo (string _csvFilePath)
        {
            var dataFromFile = File.ReadAllLines(_csvFilePath);
            string[] splittedLine = dataFromFile[0].Split(",");

            List<string> headerTitles = new List<string>(splittedLine.Length);
            headerTitles.AddRange(splittedLine);

            return headerTitles;
        }
    }
}