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
        private GameObject[] layers = null;

        public TileVisual[] TileVisuals = null;

        public void SetData(Tile tile)
        {
            TileVisualCache.Update(TileVisuals);
            //ClearTileLayers();


            layers = layers ?? new GameObject[tile.Layers.Length];

            UpdateTileLayers(tile.Layers);
            this.tile = tile;
        }

        private void ClearTileLayers()
        {
            foreach (var layer in layers ?? Enumerable.Empty<GameObject>())
            {
                if (layer == null)
                    continue;

                layer.GetComponent<Renderer>().enabled = false;
            }
        }

        private void UpdateTileLayers(IEnumerable<int> layers)
        {
            var layerIdx = 0;
            foreach (var layer in layers)
            {
                if (IsOldVisual(layerIdx, layer))
                {
                    if (this.layers[layerIdx] != null)
                    {
                        this.layers[layerIdx].transform.localPosition = Vector3.zero;
                    }
                    continue;
                }

                if (this.layers[layerIdx] != null)
                {
                    TileVisualCache.FreeVisualInstance(this.layers[layerIdx]);
                    this.layers[layerIdx] = null;
                }

                var visual = TileVisualCache.GetTileVisualInstance(gameObject, layer);

                this.layers[layerIdx] = visual;
                layerIdx += 1;
            }
        }

        private bool IsOldVisual(int layerIdx, int layer)
        {
            return tile != null && tile.Layers.Length > layer && tile.Layers[layerIdx] == layer;
        }
    }
}
