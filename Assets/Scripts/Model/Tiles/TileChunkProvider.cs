using Assets.Scripts.Model.Tiles.FileFormat;
using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Model.Tiles
{
    class TileChunkProvider
    {
        public TileChunk LoadChunk(long chunkId)
        {
            var fileName = Path.Combine(Path.Combine("Assets", "Data"), "tile_0.bin");
            using (var stream = File.OpenRead(fileName))
            {
                var chunkFile = ChunkFileFormat.Deserialize(stream);
                var tiles = ReadTiles(chunkFile);
                return new TileChunk(new Position.Coordinate(0, 0), chunkFile.Width, chunkFile.Height, tiles);
            }
        }

        private Tile[] ReadTiles(ChunkFileFormat chunkFile)
        {
            var tiles = new Tile[chunkFile.Width * chunkFile.Height];

            for (var i = 0; i < tiles.Length; ++i)
            {
                tiles[i] = new Tile(chunkFile.Layers.Length);
            }

            for (var layer = 0; layer < chunkFile.Layers.Length; ++layer)
            {
                ReadTileLayer(tiles, layer, DecodeLayer(chunkFile, layer));
            }

            return tiles;
        }

        private int[] DecodeLayer(ChunkFileFormat chunkFile, int layerIndex)
        {
            var layerTiles = new int[chunkFile.Width * chunkFile.Height];
            var currentPosition = 0;

            var layer = chunkFile.Layers[layerIndex].Tiles;

            for (var i = 0; i < layer.Length; i += 2)
            {
                var numTiles = layer[i];
                var tile = layer[i + 1];

                for (var j = 0; j < numTiles; ++j)
                {
                    layerTiles[currentPosition++] = tile;
                }
            }

            return layerTiles;
        }

        private void ReadTileLayer(Tile[] tiles, int layer, int[] layerTiles)
        {
            for (var i = 0; i < layerTiles.Length; ++i)
            {
                tiles[i].Layers[layer] = layerTiles[i];
            }
        }
    }
}
