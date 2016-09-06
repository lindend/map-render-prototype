using Assets.Scripts.Model.Position;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests.Position
{
    class CoordinateTest
    {
        private const long QuarterEarthDistance = 10007543;

        [Test]
        public void GivesCorrectCoordinatesForPoint0()
        {
            var c = new Coordinate(new LatLonCoordinate(0, 0));
            Assert.AreEqual(0, c.X);
            Assert.AreEqual(QuarterEarthDistance, c.Y);
        }

        [Test]
        public void GivesCorrectCoordinatesForQuarterAroundEarth()
        {
            var c = new Coordinate(new LatLonCoordinate(0, 90));
            Assert.AreEqual(QuarterEarthDistance, c.X);
            Assert.AreEqual(QuarterEarthDistance, c.Y);
        }

        [Test]
        public void GivesCorrectCoordinatesForThreeQuartersAroundEarth()
        {
            var c = new Coordinate(new LatLonCoordinate(0, 270));
            Assert.AreEqual(QuarterEarthDistance * 3, c.X);
            Assert.AreEqual(QuarterEarthDistance, c.Y);
        }

        [Test]
        public void GivesCorrectCoordinatesForNorthPole()
        {
            var c = new Coordinate(new LatLonCoordinate(90, 0));
            Assert.AreEqual(0, c.X);
            Assert.AreEqual(QuarterEarthDistance * 2, c.Y);
        }

        [Test]
        public void GivesCorrectCoordinatesForSouthPole()
        {
            var c = new Coordinate(new LatLonCoordinate(-90, 0));
            Assert.AreEqual(0, c.X);
            Assert.AreEqual(0, c.Y);
        }
    }
}
