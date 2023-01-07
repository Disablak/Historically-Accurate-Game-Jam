using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class RailModeTile : MonoBehaviour
    {
        public float TileWidth;
        public RailModeTile[] NonCombinableTiles;


        public bool CanCombineWithTile(RailModeTile tile)
        {
            return !NonCombinableTiles.Contains(tile);
        }
    }
}