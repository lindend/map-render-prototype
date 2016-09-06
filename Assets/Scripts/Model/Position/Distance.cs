using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model.Position
{
    public struct Distance
    {
        private const double EarthRadius = 6371;

        private double meters;

        public double InMeters { get { return meters; } }
        public double InKm { get { return meters / 1000.0; } }

        private Distance(double meters)
        {
            this.meters = meters;
        }

        public static Distance FromMeters(double meters)
        {
            return new Distance(meters);
        }

        public static Distance FromKm(double km)
        {
            return new Distance(km * 1000);
        }

        public static Distance operator +(Distance d0, Distance d1)
        {
            return new Distance(d0.meters + d1.meters);
        }

        public static Distance operator -(Distance d0, Distance d1)
        {
            return new Distance(d0.meters - d1.meters);
        }

        public static Distance Measure(LatLonCoordinate p0, LatLonCoordinate p1)
        {
            var deltaLat = ToRad(p1.Lat - p0.Lat);
            var deltaLong = ToRad(p1.Long - p0.Long);

            var a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                    Math.Cos(ToRad(p0.Lat)) * Math.Cos(ToRad(p1.Lat)) *
                    Math.Pow(Math.Sin(deltaLong / 2), 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Distance.FromKm(EarthRadius * c);
        }

        private static double ToRad(double deg)
        {
            return deg * Mathf.Deg2Rad;
        }
    }
}
