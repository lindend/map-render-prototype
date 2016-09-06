using Assets.Scripts.Controller.Location;
using Assets.Scripts.View.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ControllerHost
{
    enum LocationControllerType
    {
        GPS,
        Debug,
    }

    class LocationControllerHost : MonoBehaviour
    {
        private ILocationController locationController;

        public PlayerComponent PlayerComponent = null;
        public LocationControllerType ControllerType = LocationControllerType.Debug;

        public LocationControllerHost()
        {
        }

        public void OnValidate()
        {
            switch (ControllerType)
            {
                case LocationControllerType.Debug:
                    locationController = new DebugLocationController();
                    break;
                case LocationControllerType.GPS:
                    if (PlayerComponent != null)
                    {
                        locationController = new GpsLocationController(PlayerComponent.Player);
                    }
                    break;
            }
        }

        public void Update()
        {
            if (locationController != null)
            {
                locationController.Update(Time.deltaTime);
            }
        }
    }
}
