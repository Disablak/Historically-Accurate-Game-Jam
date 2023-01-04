using UnityEngine;

namespace DefaultNamespace
{
    public class RailCrushState : MonoBehaviour
    {
        public RailRidingState RailRidingState;


        public void Initialize()
        {
            enabled = false;

            RailRidingState.Initialize(RailRidingState.spline, 0.0f);
        }
    }
}