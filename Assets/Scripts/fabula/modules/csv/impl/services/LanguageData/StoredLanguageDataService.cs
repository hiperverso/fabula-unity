using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using fabula.modules.csv.impl.services.csvreader;

namespace fabula.modules.csv.impl.services.languagedata
{
    public class StoredLanguageDataService : MonoBehaviour
    {
        private readonly string k_generalPath = Application.dataPath + "/Resources/FabulaKeyValues.csv";

        private Dictionary<string, List<string>> m_Keys;
        private List<string> m_HeaderTitles;

        private string currentLanguage = "";
        private int currentLanguageIndex = 0;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (m_Keys == null)
                m_Keys = CsvReader.GetKeyValues(k_generalPath, "keys");

            if (m_HeaderTitles == null)
            {
                m_HeaderTitles = new List<string>();
                foreach (string title in CsvReader.GetHeaderInfo(k_generalPath))
                {
                    m_HeaderTitles.Add(title);
                }
            }

            SetLanguage("pt-br");

        }

        public void SetLanguage(string _language)
        {
            if(m_HeaderTitles != null)
            {
                int languageIndex = -1;
                foreach (string language in m_HeaderTitles)
                {
                    if (_language == language)
                    {
                        currentLanguage = _language;
                        currentLanguageIndex = languageIndex;
                    }

                    languageIndex++;
                }
            }

            if (currentLanguageIndex == -1)
                Debug.Log("Languade ID "  + _language + " not found.");
        }

        public string GetSentenceByKey(string _key, int _keyIndex)
        {
            return m_Keys.ContainsKey(_key) ? m_Keys[_key][_keyIndex] : null;
        }

        public string GetCurrentLanguage() => currentLanguage;
    }
}