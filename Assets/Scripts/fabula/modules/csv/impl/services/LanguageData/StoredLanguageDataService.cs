using System.Collections.Generic;
using UnityEngine;

using fabula.modules.csv.impl.services.csvreader;

namespace fabula.modules.csv.impl.services.languagedata
{
    public class StoredLanguageDataService : MonoBehaviour
    {
        private readonly string k_generalPath = Application.dataPath + "/Resources/FabulaKeyValues.csv";

        private Dictionary<string, List<string>> m_Keys;

        [SerializeField] private List<LanguageKeyData> m_LanguagesKeyList = new List<LanguageKeyData>();

        private void Start()
        {
            if(m_Keys == null)
                m_Keys = CsvReader.GetKeyValues(k_generalPath, "keys");



            foreach (string _key in m_Keys.Keys)
            {
                Debug.Log("Data from CSV: " + _key.ToString());
            }
        }

        private void StoragedValues(string[] values)
        {
            if (m_Keys == null)
                m_Keys = new Dictionary<string, List<string>>();

            string _key = values[0];
            List<string> valueFromKey;

            if (!m_Keys.TryGetValue(_key, out valueFromKey))
            {
                if (valueFromKey != null)
                {
                    for (int _indexInValues = 1; _indexInValues < values.Length; _indexInValues++)
                    {
                        string returnedText = values[_indexInValues];
                        valueFromKey.Add(returnedText);
                    }

                    m_Keys.Add(_key, valueFromKey);
                }
            }
        }
    }
}