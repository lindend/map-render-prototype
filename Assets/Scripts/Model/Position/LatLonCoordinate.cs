using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Position
{
    public struct LatLonCoordinate
    {
        public double Lat { get; private set; }
        public double Long { get; private set; }

        public LatLonCoordinate(double lat, double lon)
        {
            Lat = lat;
            Long = lon;
        }
    }
}
