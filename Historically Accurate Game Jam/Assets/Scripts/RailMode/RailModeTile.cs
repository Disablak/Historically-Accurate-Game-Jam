using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class RailModeTile : MonoBehaviour
    {
        public float TileWidth;
        public SplineContainer[] RailRoads;
        public RailModeTile[] NonCombinableTiles;


        public bool CanCombineWithTile(RailModeTile tile)
        {
            return !NonCombinableTiles.Contains(tile);
        }

        public IEnumerable<SplineContainer> GetAllRoadSplines() => RailRoads;
    }
}