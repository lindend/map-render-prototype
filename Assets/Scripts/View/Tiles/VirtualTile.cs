using Assets.Scripts.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Tiles
{
    class VirtualTile : MonoBehaviour
    {
        private Tile tile = null;
        private GameObject[] layerObjects = null;

        public int[] LayerData;

        public void SetData(Tile tile)
        {
            layerObjects = layerObjects ?? new GameObject[tile.Layers.Length];

            UpdateTileLayers(tile.Layers);
            this.tile = tile;
        }
        
        private void UpdateTileLayers(int[] layers)
        {
            LayerData = layers;

            for (var i = 0; i < layers.Length; ++i)
            {
                var layerData = layers[i];
                if (IsOldVisual(i, layerData))
                {
                    if (layerObjects[i] != null)
                    {
                        layerObjects[i].transform.localPosition = transform.position;
                    }
                    continue;
                }

                if (this.layerObjects[i] != null)
                {
                    TileVisualCache.FreeVisualInstance(this.layerObjects[i]);
                    this.layerObjects[i] = null;
                }

                var visual = TileVisualCache.GetTileVisualInstance(layerData);
                if (visual != null)
                {
                    visual.transform.localPosition = transform.position;
                }

                this.layerObjects[i] = visual;
            }
        }

        private bool IsOldVisual(int layerIdx, int layer)
        {
            return tile != null && tile.Layers.Length > layer && tile.Layers[layerIdx] == layer;
        }
    }
}
