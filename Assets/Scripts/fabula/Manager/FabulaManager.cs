#region LICENSE
/// <summary>
/// Author: Vinícius Bruno da Costa
//Copyright(c) 2022, Hiperverso

//All rights reserved.

// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

//    *Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//      the documentation and/or other materials provided with the distribution.
//    * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
/// </summary>
/// 
#endregion
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using fabula.manager.singleton;
using fabula.model;
using fabula.controller;

public class FabulaManager : Singleton<MonoBehaviour>
{
    [SerializeField]
    List<FabulaModel> listOfModel;
    private List<FabulaModel> currentModels;
    private int indexOnList = 0;
    
    public void Start()
    {
        Initialize();
        UpdateMessage();
    }
    
    public void Initialize()
    {
        indexOnList = 0;
        currentModels = new List<FabulaModel>();
        foreach(FabulaModel _model in listOfModel)
        {
            _model.Start();
        }
        
        currentModels = listOfModel.ToList();
    }

    private void SortByID(bool _canOrganize = true)
    {
        if (listOfModel != null)
        {
            if (_canOrganize)
                currentModels.OrderBy(c => c.GetID);
        }
    }

    public void UpdateMessage()
    {
        var message = CurrentMessage(indexOnList);
        Debug.Log(message.MessageText);
        //NextIndex();
    }

    public void NextIndex()
    {
        int previousValidIndex = indexOnList;
        indexOnList++;
        if (indexOnList >= currentModels.Count)
        {
            indexOnList = previousValidIndex;
        }
    }

    private Message CurrentMessage(int _index = 0)
    {
        if (currentModels == null) return null;
        Debug.Log("Ind. " + currentModels.Count);
        if (_index < currentModels.Count)
        {
            return currentModels[_index].GetFabulaController.GetMessage();
        }
        else
        {
            return new Message() { ID = 0, MessageText = "NO VALUE" };
        }
    }

    private void ClearValues()
    {
        
    }
}
