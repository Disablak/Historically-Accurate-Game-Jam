using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("directionToSplinePointThisFrame")]
        public Vector3 directionToSplinePoint;

        private PlayerStateMachine machine;
        private Vector3 playerPositionLastFrame;


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
            transform.position += MovementDirection * Time.deltaTime;


            var nearestSplineContainer = Rails.OrderBy(splineContainer =>
            {
                Vector3 comparisonLocalPoint = splineContainer.transform.InverseTransformPoint(transform.position);
                float distanceToSplinePoint = SplineUtility.GetNearestPoint(
                    splineContainer.Spline,
                    comparisonLocalPoint,
                    out float3 _,
                    out float _
                    , SplineUtility.PickResolutionMax
                    , 10);

                return distanceToSplinePoint;
            }).First();

            Vector3 comparisonLocalPoint = nearestSplineContainer.transform.InverseTransformPoint(transform.position);

            float distanceToSplinePoint = SplineUtility.GetNearestPoint(
                nearestSplineContainer.Spline,
                comparisonLocalPoint,
                out float3 resultLocalPoint,
                out float normalizedDistance
                , SplineUtility.PickResolutionMax
                , 10);

            Vector3 resultWorldPoint = nearestSplineContainer.transform.TransformPoint(resultLocalPoint);
            float distanceFromStartToEnd = (StartedFlyingFromPoint - resultWorldPoint).magnitude;

            directionToSplinePoint = transform.position - resultWorldPoint;
            distanceToSplinePointThisFrame = distanceToSplinePoint;

            bool fliedThroughSpline =
                (resultWorldPoint - playerPositionLastFrame).y < 0 && (resultWorldPoint - transform.position).y > 0;

            playerPositionLastFrame = transform.position;

            if ((distanceToSplinePoint <= DistanceToRailToBeAttachedTo
                 && distanceFromStartToEnd >= DistanceFromStartToBeAllowedToAttach)
                || fliedThroughSpline)
            {
                machine.ActivateRailingState(nearestSplineContainer, normalizedDistance);
                return;
            }

            if (transform.position.y >= PlayerAltitudeToBeConsideredCrushed)
                return;

            machine.ActivateCrushedState();
        }
    }
}