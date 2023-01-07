using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class RailTilesBuilder : MonoBehaviour
    {
        public float DesiredLevelWidth;
        public Vector3 SpawnStartPosition;

        public RailModeTile[] TilesPrefabs;
        public List<RailModeTile> TilesInstances;


        public void Start()
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
                RailModeTile instance = Instantiate(tile, spawnPosition, tile.transform.rotation);
                TilesInstances.Add(instance);

                spawnPosition.x += tile.TileWidth;
                totalLevelWidth += tile.TileWidth;
            }
        }
    }
}