using Assets.Scripts.Model.Player;
using Assets.Scripts.Model.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controller.Location
{
    class GpsLocationController : ILocationController
    {
        private Player player;

        public GpsLocationController(Player player)
        {
            this.player = player;
        }

        public void CheckGpsEnabled()
        {
            if (!Input.location.isEnabledByUser)
            {
                throw new Exception("TODO: Make this not exception");
            }
        }

        public virtual void Start()
        {
            CheckGpsEnabled();
            Input.location.Start();
        }

        public virtual void Stop()
        {
            Input.location.Stop();
        }

        public void Update(float delta)
        {
            if (Input.location.status == LocationServiceStatus.Initializing)
            {
                return;
            }

            var locationData = Input.location.lastData;
            var position = new LatLonCoordinate(locationData.longitude, locationData.latitude);

            if (player != null)
            {
                player.Position = position;
            }
        }
    }
}
