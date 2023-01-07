using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInitializerComponent : MonoBehaviour
    {
        public RailTilesBuilder TilesBuilder;

        private void Start()
        {
            GetComponent<FlyingState>().Rails = TilesBuilder.GetRailRoadSplinesFromInstances().ToList();
        }
    }
}