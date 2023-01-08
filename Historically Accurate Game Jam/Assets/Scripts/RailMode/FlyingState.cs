using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using SplineUtility = UnityEngine.Splines.SplineUtility;

namespace DefaultNamespace
{
    public class FlyingState : MonoBehaviour
    {
        public Vector3 MovementDirection;
        public Vector3 GravityDirection;
        public Vector3 StartedFlyingFromPoint;

        public List<SplineContainer> Rails;
        public float DistanceToRailToBeAttachedTo = 0.1f;
        public float DistanceFromStartToBeAllowedToAttach = 0.1f;
        public float PlayerAltitudeToBeConsideredCrushed = -1;


        public int testValue;
        public float distanceToSplinePointThisFrame;
        public Vector3 directionToSplinePointThisFrame;

        private PlayerStateMachine machine;


        public void SetStateMachine(PlayerStateMachine machine)
        {
            this.machine = machine;
        }

        public void EnableState(Vector3 PlayerPosition, Vector3 PlayerDirection)
        {
            MovementDirection = PlayerDirection;
            StartedFlyingFromPoint = PlayerPosition;

            transform.position = PlayerPosition;
            enabled = true;
        }

        private void Update()
        {
            Application.targetFrameRate = testValue;

            MovementDirection += GravityDirection * Time.deltaTime;
            transform.position += MovementDirection;

            foreach (SplineContainer splineContainer in Rails)
            {
                Vector3 comparisonLocalPoint = splineContainer.transform.InverseTransformPoint(transform.position);

                float distanceToSplinePoint = SplineUtility.GetNearestPoint(
                    splineContainer.Spline,
                    comparisonLocalPoint,
                    out float3 resultLocalPoint,
                    out float normalizedDistance
                    , SplineUtility.PickResolutionMax
                    , 10);

                Vector3 resultWorldPoint = splineContainer.transform.TransformPoint(resultLocalPoint);
                float distanceFromStartToEnd = (StartedFlyingFromPoint - resultWorldPoint).magnitude;

                directionToSplinePointThisFrame = transform.position - resultWorldPoint;
                distanceToSplinePointThisFrame = distanceToSplinePoint;

                if (distanceToSplinePoint <= DistanceToRailToBeAttachedTo
                    && distanceFromStartToEnd >= DistanceFromStartToBeAllowedToAttach)
                {
                    machine.ActivateRailingState(splineContainer, normalizedDistance);
                    return;
                }
            }

            if (transform.position.y >= PlayerAltitudeToBeConsideredCrushed)
                return;

            machine.ActivateCrushedState();
        }
    }
}