using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public RailTilesBuilder tilesBuilder;

        private RailRidingState _ridingState;
        private FlyingState _flyingState;
        private ObstacleDetectionState _obstacleDetectionState;


        private void Awake()
        {
            _obstacleDetectionState = GetComponent<ObstacleDetectionState>();
            _flyingState = GetComponent<FlyingState>();
            _ridingState = GetComponent<RailRidingState>();

            _ridingState.SetStateMachine(this);
            _flyingState.SetStateMachine(this);
            _obstacleDetectionState.SetStateMachine(this);
        }

        private void Start()
        {
            GetComponent<FlyingState>().Rails = tilesBuilder.GetRailRoadSplinesFromInstances().ToList();
        }

        public void ActivateFlyingState(Vector3 PlayerPosition, Vector3 PlayerDirection)
        {
            _flyingState.EnableState(PlayerPosition, PlayerDirection);

            _flyingState.enabled = true;
            _ridingState.enabled = false;
        }

        public void ActivateRailingState(SplineContainer spline, float normalizedDistanceOnSpline)
        {
            _ridingState.EnableState(spline, normalizedDistanceOnSpline);

            _flyingState.enabled = false;
            _ridingState.enabled = true;
        }

        public void ActivateCrushedState()
        {
            ActivateRailingState(_ridingState.spline, 0.0f);
        }
    }
}