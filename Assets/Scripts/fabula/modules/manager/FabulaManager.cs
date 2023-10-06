using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fabula.modules.csv.impl.services.csvreader;

namespace fabula.modules.manager
{
    public class FabulaManager : MonoBehaviour
    {
        private Dictionary<string, List<string>> currentLoadedKeyValues;

        private void OnEnable()
        {
            if (currentLoadedKeyValues == null)
                return;
        }

        public void LoadMessages(string _filePath)
        {
            if(_filePath != null)
            { 
                currentLoadedKeyValues = CsvReader.GetKeyValues(_filePath);
                Debug.Log("<color=green>Successfully loaded data from " + _filePath + "</color>");
            }
            else
            {
                Debug.Log("<color=orange>Impossible to load data from " + _filePath + "</color>");
            }
        }
    }
}