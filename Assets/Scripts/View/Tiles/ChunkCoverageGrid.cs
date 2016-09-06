using Assets.Scripts.Model.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Tiles
{
    class ChunkCoverageGrid
    {
        private int chunkWidth;
        private int chunkHeight;

        private bool[,] coverageGrid;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ChunkCoverageGrid(int chunkWidth, int chunkHeight)
        {
            this.chunkWidth = chunkWidth;
            this.chunkHeight = chunkHeight;
            Width = 0;
            Height = 0;
        }

        public void ClearGrid()
        {
            for (var x = 0; x < Width; ++x)
            {
                for (var y = 0; y < Height; ++y)
                {
                    coverageGrid[x, y] = false;
                }
            }
        }

        public void SetDimensions(float width, float height)
        {
            var numWidth = Mathf.CeilToInt(width / chunkWidth) + 1;
            var numHeight = Mathf.CeilToInt(height / chunkHeight) + 1;

            if (coverageGrid == null || height != numHeight || width != numWidth)
            {
                coverageGrid = new bool[numWidth, numHeight];
                Width = numWidth;
                Height = numHeight;
            }
        }

        public bool SetOccupied(int globalX, int globalY)
        {
            var x = globalX / chunkWidth;
            var y = globalY / chunkHeight;
            if (x >= Width || y >= Height || x < 0 || y < 0)
            {
                return false;
            }
            if (coverageGrid[x, y])
            {
                return false;
            }
            coverageGrid[x, y] = true;
            return true;
        }

        public IEnumerable<Coordinate> GetUnoccupied()
        {
            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Width; ++x)
                {
                    if (!coverageGrid[x, y])
                    {
                        yield return new Coordinate(x, y);
                    }
                }
            }
        }
    }
}
