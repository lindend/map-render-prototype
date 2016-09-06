using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Tiles
{
    static class TileVisualCache
    {
        private static TileVisual[] tileVisualsCacheVersion;
        private static UnityEngine.Object defaultVisualCache;
        private static Dictionary<int, UnityEngine.Object> tileVisualsCache;

        private static Dictionary<int, Stack<GameObject>> visualInstanceCache = new Dictionary<int, Stack<GameObject>>();
        private static HashSet<int> missingIndexes = new HashSet<int>();

        public static void Update(TileVisual[] tileVisuals)
        {
            if (tileVisuals != tileVisualsCacheVersion)
            {
                tileVisualsCache = tileVisuals.ToDictionary(tv => tv.Index, tv => tv.Visual);
                tileVisualsCacheVersion = tileVisuals;
                if (!tileVisualsCache.TryGetValue(0, out defaultVisualCache))
                {
                    defaultVisualCache = null;
                }
            }
        }

        public static UnityEngine.Object GetTileVisual(int index)
        {
            UnityEngine.Object result = null;
            if (!tileVisualsCache.TryGetValue(index, out result))
            {
                result = defaultVisualCache;
            }

            return result;
        }

        public static GameObject GetTileVisualInstance(GameObject tile, int index)
        {
            var visual = GetTileVisual(index);
            if (visual == null)
            {
                if (missingIndexes.Add(index))
                {
                    Debug.Log("Unable to find visual for tile index: " + index.ToString());
                }
                return null;
            }

            var layerVisual = GetVisualObject(tile, index, visual);
            layerVisual.transform.localPosition = Vector3.zero;
            return layerVisual;
        }

        private static GameObject GetVisualObject(GameObject tile, int index, UnityEngine.Object visual)
        {
            Stack<GameObject> tiles;
            if (visualInstanceCache.TryGetValue(index, out tiles) && tiles.Any())
            {
                var visualObject = tiles.Pop();
                visualObject.transform.SetParent(tile.transform);
                visualObject.SetActive(true);
                return visualObject;
            }
            else
            {
                var newObject = (GameObject)GameObject.Instantiate(visual, tile.transform);
                newObject.AddComponent<TileData>().VisualIndex = index;
                return newObject;
            }
        }

        public static void FreeVisualInstance(GameObject tile)
        {
            tile.SetActive(false);

            var visualIndex = tile.GetComponent<TileData>().VisualIndex;
            Stack<GameObject> tiles;
            if (!visualInstanceCache.TryGetValue(visualIndex, out tiles))
            {
                tiles = new Stack<GameObject>();
                visualInstanceCache.Add(visualIndex, tiles);
            }

            tiles.Push(tile);
        }
    }
}
