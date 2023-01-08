using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class RailRidingState : MonoBehaviour
    {
        public SplineContainer spline;
        public Vector3 AdditionalJumpDirection;
        public AudioSource JumpAudio;

        Vector3 GravityDirection = Vector3.down;

        public float gravityForceAcceleration = 0.5f;
        public float staticPlayerSpeed = 0.5f;
        public float playerOnSplineDistance;
        public float currentPlayerSpeed;

        private PlayerStateMachine machine;

        public void SetStateMachine(PlayerStateMachine machine)
        {
            this.machine = machine;
        }

        public void EnableState(SplineContainer spline, float normalizedDistanceOnSpline)
        {
            this.spline = spline;
            playerOnSplineDistance = NormalizedToDistance(normalizedDistanceOnSpline);
            currentPlayerSpeed = 0;
        }

        private void Update()
        {
            float playerOnSplinePositionNormalized = DistanceToNormalized(playerOnSplineDistance);
            Vector3 pointOnSpline = spline.EvaluatePosition(playerOnSplinePositionNormalized);
            Vector3 splineTangent = spline.EvaluateTangent(playerOnSplinePositionNormalized);

            transform.right = splineTangent.normalized;
            float dotProduct = math.dot(GravityDirection, splineTangent.normalized);

            currentPlayerSpeed = (staticPlayerSpeed + (gravityForceAcceleration * dotProduct)) * Time.deltaTime;

            playerOnSplineDistance += currentPlayerSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpAudio.Stop();
                JumpAudio.Play();
                GotoFlyingState(AdditionalJumpDirection);
            }
            else if (playerOnSplinePositionNormalized is > 1.0f or < 0.0f)
                GotoFlyingState(Vector3.zero);

            transform.position = pointOnSpline;

            void GotoFlyingState(Vector3 additionalJumpingDirection)
            {
                var playerDirection = (staticPlayerSpeed + (gravityForceAcceleration * dotProduct)) *
                                      splineTangent.normalized;
                playerDirection += additionalJumpingDirection;

                machine.ActivateFlyingState(pointOnSpline, playerDirection);
            }
        }

        private float NormalizedToDistance(float normalizedValue)
            => spline.Spline.ConvertIndexUnit(normalizedValue, PathIndexUnit.Distance);

        private float DistanceToNormalized(float distance)
            => distance / spline.Spline.GetLength();
    }
}