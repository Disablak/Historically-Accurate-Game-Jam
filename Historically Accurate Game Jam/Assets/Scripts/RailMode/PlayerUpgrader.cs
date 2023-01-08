using Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerUpgrader : MonoBehaviour
    {
        public PlayerStateMachine StateMachine;
        public FlyingState FlyingState;
        public RailRidingState RailRidingState;
        public GameObject SecondPilot;


        private void Start()
        {
            SecondPilot.SetActive(ModulesCommon.ModulePlayer.player.playerUpgrades.hasHelper);
        }
    }
}