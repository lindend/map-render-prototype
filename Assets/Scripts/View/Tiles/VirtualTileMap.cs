using Assets.Scripts.Model.Position;
using Assets.Scripts.Model.Tiles;
using Assets.Scripts.View.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Tiles
{
    class VirtualTileMap : MonoBehaviour
    {
        private List<VirtualTileChunk> chunks = new List<VirtualTileChunk>();

        private const int chunkWidth = 10;
        private const int chunkHeight = 10;

        private Vector3 chunkSize;

        private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        public Camera Camera = null;
        public GameObject TilePrefab = null;

        public TileVisual[] TileVisuals = null;

        private TileMap tileMap = new TileMap();
        private Tile[] loadTilesBuffer = new Tile[chunkWidth * chunkHeight];
        private Queue<VirtualTileChunk> unusedChunks = new Queue<VirtualTileChunk>();
        private ChunkCoverageGrid chunkCoverage = new ChunkCoverageGrid(chunkWidth, chunkHeight);

        public VirtualTileMap()
        {
        }

        void Start()
        {
            chunkSize = new Vector3(chunkWidth * tileMap.Resolution, 0f, chunkHeight * tileMap.Resolution);
            TileVisualCache.Update(TileVisuals);
        }

        private Vector3 CoordinatesToPosition(Coordinate coordinates)
        {
            return new Vector3(coordinates.X, 0f, coordinates.Y);
        }

        void Update()
        {
            var viewBounds = GetGroundViewBounds();

            var minPosX = Mathf.FloorToInt(viewBounds.min.x / 10) * 10;
            var minPosY = Mathf.FloorToInt(viewBounds.min.z / 10) * 10;

            var width = viewBounds.extents.x * 2f;
            var height = viewBounds.extents.z * 2f;

            FindUnusedChunks(minPosX, minPosY, width, height);
            ArrangeMissingChunks(minPosX, minPosY);
        }

        private void FindUnusedChunks(int minPosX, int minPosY, float width, float height)
        {
            unusedChunks.Clear();
            chunkCoverage.SetDimensions(width, height);
            chunkCoverage.ClearGrid();
            foreach (var c in chunks)
            {
                if (!chunkCoverage.SetOccupied((int)(c.Position.X - minPosX), (int)(c.Position.Y - minPosY)))
                {
                    unusedChunks.Enqueue(c);
                }
            }
        }

        private void ArrangeMissingChunks(int minPosX, int minPosY)
        {
            foreach (var missingChunk in chunkCoverage.GetUnoccupied())
            {
                var chunk = GetUnusedChunk();

                var coordinates = new Coordinate(minPosX + missingChunk.X * chunkWidth, minPosY + missingChunk.Y * chunkHeight);
                tileMap.LoadChunk(coordinates, chunkWidth, chunkHeight, loadTilesBuffer);
                var chunkPos = CoordinatesToPosition(coordinates);
                chunk.SetData(TilePrefab, coordinates, chunkPos, chunkWidth, chunkHeight, tileMap.Resolution, loadTilesBuffer);
            }
        }

        private VirtualTileChunk GetUnusedChunk()
        {
            VirtualTileChunk chunk;
            if (unusedChunks.Any())
            {
                chunk = unusedChunks.Last();
            }
            else
            {
                chunk = CreateChunk();
            }

            return chunk;
        }

        private Bounds GetGroundViewBounds()
        {
            var botLeftRay = GetGroundPlanePos(new Vector3(0f, 0f, 0f));
            var topLeftRay = GetGroundPlanePos(new Vector3(0f, 1f, 0f));
            var topRightRay = GetGroundPlanePos(new Vector3(1f, 1f, 0f));
            var botRightRay = GetGroundPlanePos(new Vector3(1f, 0f, 0f));
            var bounds = new Bounds();
            bounds.SetMinMax(Vector3.Max(Vector3.zero, Vector3.Min(Vector3.Min(botLeftRay, topLeftRay), Vector3.Min(botRightRay, topRightRay))),
                             Vector3.Max(Vector3.Max(botLeftRay, topLeftRay), Vector3.Max(botRightRay, topRightRay)));
            return bounds;
        }

        private VirtualTileChunk CreateChunk()
        {
            var chunkObject = new GameObject("Chunk", typeof(VirtualTileChunk));
            var chunkComponent = chunkObject.GetComponent<VirtualTileChunk>();
            chunkObject.transform.SetParent(transform, false);
            chunks.Add(chunkComponent);
            return chunkComponent;
        }

        private Bounds GetChunkBounds(VirtualTileChunk chunk)
        {
            return new Bounds(chunk.transform.localPosition + chunkSize * 0.5f, chunkSize);
        }

        Vector3 GetGroundPlanePos(Vector3 viewportPos)
        {
            var ray = Camera.ViewportPointToRay(viewportPos);

            var groundDist = 0f;
            groundPlane.Raycast(ray, out groundDist);

            return ray.origin + ray.direction * groundDist;
        }
    }
}
