using Assets.Scripts.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controller.Location
{
    class IdleGpsLocationController : GpsLocationController
    {
        public IdleGpsLocationController(Player player)
            : base(player)
        {
        }

        public override void Start()
        {
            CheckGpsEnabled();
            Input.location.Start(500, 500);
        }
    }
}
