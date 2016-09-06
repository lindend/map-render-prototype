using Assets.Scripts.Model.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model.Tiles
{
    public class TileChunk
    {
        public Coordinate Position { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Tile[] Tiles { get; private set; }

        public TileChunk(Coordinate position, int width, int height, Tile[] tiles)
        {
            Tiles = tiles;
            Width = width;
            Height = height;
            Position = position;
        }

        public Tile GetTile(long x, long y)
        {
            return Tiles[(x - Position.X) + (y - Position.Y) * Width];
        }
    }
}
