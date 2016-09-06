using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Position
{
    public struct Coordinate
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        public float FractionX { get; private set; }
        public float FractionY { get; private set; }

        public Coordinate(LatLonCoordinate coords)
        {
            var xDist = GetLonDistance(coords);
            var yDist = Distance.Measure(coords, new LatLonCoordinate(-90f, coords.Long));

            X = (long)xDist.InMeters;
            Y = (long)yDist.InMeters;
            FractionX = (float)(xDist.InMeters % 1);
            FractionY = (float)(yDist.InMeters % 1);
        }

        public Coordinate(long x, long y)
        {
            X = x;
            Y = y;
            FractionX = 0f;
            FractionY = 0f;
        }

        private static Distance GetLonDistance(LatLonCoordinate coords)
        {
            if (coords.Long < 180f)
            {
                return Distance.Measure(coords, new LatLonCoordinate(coords.Lat, 0.0));
            }
            else
            {
                return Distance.Measure(coords, new LatLonCoordinate(coords.Lat, 180)) + 
                       Distance.Measure(new LatLonCoordinate(coords.Lat, 180), new LatLonCoordinate(coords.Lat, 0));
            }
        }
    }
}
