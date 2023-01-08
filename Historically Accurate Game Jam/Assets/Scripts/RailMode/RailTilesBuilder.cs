using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class RailTilesBuilder : MonoBehaviour
    {
        public float DesiredLevelWidth;
        public Vector3 SpawnStartPosition;

        public RailModeTile FinishTile;
        public RailModeTile[] TilesPrefabs;
        public List<RailModeTile> TilesInstances;


        public void Awake()
        {
            float totalLevelWidth = 0;
            Vector3 spawnPosition = SpawnStartPosition;

            while (totalLevelWidth < DesiredLevelWidth)
            {
                RailModeTile previousTile = TilesInstances.LastOrDefault();
                IEnumerable<RailModeTile> tilesFilter;

                if (previousTile != null)
                    tilesFilter = TilesPrefabs.Where(tile => previousTile.CanCombineWithTile(tile));
                else
                    tilesFilter = TilesPrefabs;

                List<RailModeTile> tilesSelection = tilesFilter.ToList();

                if (tilesSelection.Count == 0)
                {
                    Debug.LogWarning($"Tile {previousTile} filtered out all possible tiles prefabs. Exiting Early");
                    break;
                }

                int randomIndex = Random.Range(0, tilesSelection.Count);
                RailModeTile tile = TilesPrefabs[randomIndex];
                RailModeTile instance = Instantiate(tile, spawnPosition, tile.transform.rotation, transform);
                TilesInstances.Add(instance);

                spawnPosition.x += tile.TileWidth;
                totalLevelWidth += tile.TileWidth;
            }

            if (FinishTile != null)
            {
                RailModeTile instance = Instantiate(FinishTile, spawnPosition, FinishTile.transform.rotation, transform);
                TilesInstances.Add(instance);
            }
        }

        public void DestroyAllInstances()
        {
            foreach (RailModeTile tile in TilesInstances)
            {
                Destroy(tile);
            }
        }

        public IEnumerable<SplineContainer> GetRailRoadSplinesFromInstances()
            => TilesInstances.SelectMany(tile => tile.GetAllRoadSplines());

        public IEnumerable<SplineContainer> GetSplinesFromLastTile()
            => TilesInstances.Last().RailRoads;
    }
}