using Assets.Scripts.Model.Position;
using Assets.Scripts.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Tiles
{
    class VirtualTileChunk : MonoBehaviour
    {
        private int width;
        private int height;
        private GameObject[] tiles;

        public Coordinate Position { get; private set; }

        public VirtualTileChunk()
        {
        }

        public void SetData(GameObject tilePrefab, Coordinate coordinates, Vector3 position, int width, int height, float resolution, Tile[] tileData)
        {
            Position = coordinates;

            if (this.width != width || this.height != height)
            {
                DestroyTiles();
            }

            this.width = width;
            this.height = height;
            transform.localPosition = position;

            tiles = tiles ?? new GameObject[width * height];

            SetupTiles(tilePrefab, width, height, resolution, tileData);
        }

        private void SetupTiles(GameObject tilePrefab, int width, int height, float resolution, Tile[] tileData)
        {
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var tileIndex = x + y * width;

                    var tileObject = GetOrCreateTile(tileIndex, tilePrefab);
                    tileObject.transform.localRotation = Quaternion.identity;
                    tileObject.transform.localPosition = new Vector3(x * resolution, 0f, y * resolution);

                    var virtualTile = tileObject.GetComponent<VirtualTile>();
                    virtualTile.SetData(tileData[tileIndex]);
                }
            }
        }

        private GameObject GetOrCreateTile(int tileIndex, GameObject tilePrefab)
        {
            var tile = tiles[tileIndex];
            if (tile == null)
            {
                tile = Instantiate(tilePrefab);
                tile.transform.SetParent(transform, false);
                tiles[tileIndex] = tile;
            }
            return tile;
        }

        private void DestroyTiles()
        {
            if (tiles != null)
            {
                foreach (var tile in tiles)
                {
                    Destroy(tile);
                }
                tiles = null;
            }
        }
    }
}
