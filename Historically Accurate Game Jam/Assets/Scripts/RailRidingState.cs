using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class RailRidingState : MonoBehaviour
    {
        public SplineContainer spline;
        public FlyingState FlyingState;
        public Vector3 AdditionalJumpDirection;

        Vector3 GravityDirection = Vector3.down;

        public float gravityForceAcceleration = 0.5f;
        public float playerOnSplineDistance;
        public float currentPlayerSpeed;


        public void Initialize(SplineContainer spline, float normalizedDistanceOnSpline)
        {
            this.spline = spline;
            playerOnSplineDistance = NormalizedToDistance(normalizedDistanceOnSpline);
            enabled = true;
        }

        private void Update()
        {
            float playerOnSplinePositionNormalized = DistanceToNormalized(playerOnSplineDistance);
            Vector3 pointOnSpline = spline.EvaluatePosition(playerOnSplinePositionNormalized);
            Vector3 splineTangent = spline.EvaluateTangent(playerOnSplinePositionNormalized);

            transform.right = splineTangent.normalized;
            float dotProduct = math.dot(GravityDirection, splineTangent.normalized);

            currentPlayerSpeed += (Time.deltaTime * gravityForceAcceleration) * dotProduct;

            playerOnSplineDistance += currentPlayerSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
                GotoFlyingState(AdditionalJumpDirection * Time.deltaTime);
            else if ((playerOnSplinePositionNormalized is > 1.0f or < 0.0f))
                GotoFlyingState(Vector3.zero);

            transform.position = pointOnSpline;

            void GotoFlyingState(Vector3 additionalJumpingDirection)
            {
                var playerDirection = currentPlayerSpeed * splineTangent.normalized;
                playerDirection += additionalJumpingDirection;

                FlyingState.Initialize(pointOnSpline, playerDirection);
                enabled = false;
            }
        }

        private float NormalizedToDistance(float normalizedValue)
            => spline.Spline.ConvertIndexUnit(normalizedValue, PathIndexUnit.Distance);

        private float DistanceToNormalized(float distance)
            => distance / spline.Spline.GetLength();
    }
}