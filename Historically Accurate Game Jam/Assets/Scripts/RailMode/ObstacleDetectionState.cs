using UnityEngine;

namespace DefaultNamespace
{
    public class ObstacleDetectionState : MonoBehaviour
    {
        private PlayerStateMachine machine;


        public void SetStateMachine(PlayerStateMachine machine)
        {
            this.machine = machine;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!enabled)
                return;

            if (machine != null)
                machine.ActivateCrushedState();
        }
    }
}