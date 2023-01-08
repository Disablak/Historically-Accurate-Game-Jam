using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Splines;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public RailTilesBuilder tilesBuilder;
        public AudioSource CrushAudio;

        private RailRidingState _ridingState;
        private FlyingState _flyingState;
        private ObstacleDetectionState _obstacleDetectionState;
        private List<SplineContainer> finishSplines;
        private bool crushedThisFrame = false;


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
            finishSplines = tilesBuilder.GetSplinesFromLastTile().ToList();
        }

        public void ActivateFlyingState(
            float currentSplineNormalizedPosition,
            Vector3 PlayerPosition,
            Vector3 PlayerDirection)
        {
            if (finishSplines.Contains(_ridingState.spline) && currentSplineNormalizedPosition >= 0.95f)
            {
                ActivateFinishState();
                return;
            }

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
            if (crushedThisFrame)
                return;

            crushedThisFrame = true;

            ModulesCommon.ModuleCart.tryLoseResources(out _);
            CrushAudio.Stop();
            CrushAudio.Play();
            ActivateRailingState(_ridingState.spline, 0.0f);
        }

        public void ActivateFinishState()
        {
            _flyingState.enabled = false;
            _ridingState.enabled = false;
            _obstacleDetectionState.enabled = false;

            ModulesCommon.ModuleCart.endCarting();
            ModulesCommon.loadNextScene();

            Debug.Log("Level completed");
        }

        private void Update()
        {
            crushedThisFrame = false;
        }
    }
}