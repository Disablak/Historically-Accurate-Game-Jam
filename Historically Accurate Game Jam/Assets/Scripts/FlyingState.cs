using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using SplineUtility = UnityEngine.Splines.SplineUtility;

namespace DefaultNamespace
{
    public class FlyingState : MonoBehaviour
    {
        public RailRidingState RailRidingState;
        public Vector3 MovementDirection;
        public Vector3 GravityDirection;
        public Vector3 StartedFlyingFromPoint;

        public List<SplineContainer> Rails;
        public float DistanceToRailToBeAttachedTo = 0.1f;
        public float DistanceFromStartToBeAllowedToAttach = 0.1f;


        public void Initialize(Vector3 PlayerPosition, Vector3 PlayerDirection)
        {
            MovementDirection = PlayerDirection;
            StartedFlyingFromPoint = PlayerPosition;

            transform.position = PlayerPosition;
            enabled = true;
        }

        private void Update()
        {
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

                // Debug.DrawLine(comparisonLocalPoint, resultLocalPoint);

                Vector3 resultWorldPoint = splineContainer.transform.TransformPoint(resultLocalPoint);
                float distanceFromStartToEnd = (StartedFlyingFromPoint - resultWorldPoint).magnitude;

                // Debug.Log("distanceFromStartToEnd: " + distanceFromStartToEnd + " distance:" + distanceToSplinePoint +
                //           " normalizedDistance: " + normalizedDistance);

                if (distanceToSplinePoint <= DistanceToRailToBeAttachedTo
                    && distanceFromStartToEnd >= DistanceFromStartToBeAllowedToAttach)
                {
                    RailRidingState.Initialize(splineContainer, normalizedDistance);
                    enabled = false;
                    return;
                }
            }
        }
    }
}