using UnityEngine;
using Internal.Baseact.api;

namespace Internal.Baseact.implementation
{
    public abstract class BaseAct : MonoBehaviour, IAct
    {
        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void NextAct()
        {
            throw new System.NotImplementedException();
        }

        public void NextMessage()
        {
            throw new System.NotImplementedException();
        }

        public void PreviousMessage()
        {
            throw new System.NotImplementedException();
        }

        public void Unload()
        {
            throw new System.NotImplementedException();
        }
    }
}
