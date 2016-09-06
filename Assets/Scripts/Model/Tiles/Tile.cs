using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model.Tiles
{
    public class Tile
    {
        public int[] Layers { get; private set; }

        public Tile(int numLayers)
        {
            Layers = new int[numLayers];
        }
    }
}
