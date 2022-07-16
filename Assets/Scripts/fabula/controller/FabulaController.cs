
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

using fabula.model;
using fabula.api;
using System.Linq;
using System;
using System.Collections.Generic;

namespace fabula.controller
{
    public class FabulaController : IActionable
    {
        protected FabulaStateType StateType { get; private set; }

        private int fabulaModelID;

        private string fabulaModelName;

        private Message message;

        public Action OnFinalize;
        public Action OnInitialize;
        public Action OnUpdateMessage;
        public Action OnError;

        private List<Message> Messages;
        private int messagesIndex = 0;

        public FabulaController(FabulaModel _fabulaModel)
        {
            fabulaModelID = _fabulaModel.GetID;
            fabulaModelName = _fabulaModel.GetFabulaName;

            AddMessageFromModel(_fabulaModel.GetMessages);
            Initialize();
        }

        public void Initialize()
        {
            SwitchStateType(FabulaStateType.INIT);
            if (OnInitialize != null) OnInitialize();
            if (GetMessage() == null)
                return;
        }

        public Message GetMessage()
        {
            UpdateMessage();
            return (message != null) ? message : null;
        }

        private void AddMessageFromModel(List<Message> _messagesList)
        {
            if (_messagesList.Count > 0)
            {
                SwitchStateType(FabulaStateType.LOADING_MODEL);
                //Messages.AddRange(_messagesList);
                Messages = _messagesList.ToList();
                message = Messages[0];
            }
            else
            {
                message = null;
            }
        }

        public void UpdateMessage()
        {
            SwitchStateType(FabulaStateType.UPDATE);
            if (messagesIndex < Messages.Count)
                message = Messages[messagesIndex];
            else
                Ended();
                
            if (OnUpdateMessage != null) OnUpdateMessage();
            messagesIndex++;
        }

        public void Ended()
        {
            SwitchStateType(FabulaStateType.END);
            if (OnFinalize != null) OnFinalize();
        }

        private void SwitchStateType(FabulaStateType _stateType)
        {
            if (_stateType == StateType) return;
            
            StateType = _stateType;

            //Debug.Log("STATE:: " + _stateType.ToString());
        }

        ~FabulaController()
        {
            messagesIndex = 0;
            OnInitialize = null;
            OnUpdateMessage = null;
            OnError = null;
            OnFinalize = null;
        }
    }
}