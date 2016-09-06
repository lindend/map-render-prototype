using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Controller.Location
{
    public interface ILocationController
    {
        void Start();
        void Stop();
        void Update(float delta);
    }
}
