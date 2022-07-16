using System;
using fabula.model;

namespace fabula.api
{
    public interface IActionable
    {
        void Initialize();

        void UpdateMessage();

        void Ended();
    }
}

