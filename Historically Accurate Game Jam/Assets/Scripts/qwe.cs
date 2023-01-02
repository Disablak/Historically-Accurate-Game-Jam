using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class qwe : MonoBehaviour
    {
        public SplineContainer spline;

        Vector3 playerWorldPosition;
        Vector3 GravityDirection = Vector3.down;

        public float gravityForceAcceleration = 0.5f;

        public float playerOnSplineDistance;

        public float inputAccelerationSpeed = 0.1f;
        public float manualMovementSpeed = 0.5f;
        public float inputAccelerationFactor = 0.0f;

        public float currentPlayerSpeed;
        public float maxPlayerSpeed;


        private void Start()
        {
            playerOnSplineDistance = NormalizedToDistance(0.5f);
        }

        private void Update()
        {
            var playerOnSplinePositionNormalized = DistanceToNormalized(playerOnSplineDistance);
            var pointOnSpline = spline.EvaluatePosition(playerOnSplinePositionNormalized);
            Vector3 splineTangent = spline.EvaluateTangent(playerOnSplinePositionNormalized);

            float dotProduct = math.dot(GravityDirection, splineTangent.normalized);

            playerOnSplineDistance += (Time.deltaTime * gravityForceAcceleration) * dotProduct;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
                inputAccelerationFactor += inputAccelerationSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
                inputAccelerationFactor -= inputAccelerationSpeed * Time.deltaTime;
            else
            {
                var deceleration = math.abs(inputAccelerationFactor) - inputAccelerationSpeed * Time.deltaTime;
                deceleration = math.clamp(deceleration, 0.0f, 1.0f);

                inputAccelerationFactor = deceleration * math.sign(inputAccelerationFactor);
            }

            inputAccelerationFactor = math.clamp(inputAccelerationFactor, -1.0f, 1.0f);
            playerOnSplineDistance += (manualMovementSpeed * Time.deltaTime) * inputAccelerationFactor;

            Debug.DrawLine(Vector3.zero, pointOnSpline);
            Debug.DrawRay(pointOnSpline, splineTangent, Color.green);
        }

        private float NormalizedToDistance(float normalizedValue)
            => spline.Spline.ConvertIndexUnit(normalizedValue, PathIndexUnit.Distance);

        private float DistanceToNormalized(float distance)
            => distance / spline.Spline.GetLength();
    }
}