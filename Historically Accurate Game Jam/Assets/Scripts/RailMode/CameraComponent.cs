using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraComponent : MonoBehaviour
    {
        public float interpolationSpeed = 0.1f;
        public GameObject target;


        private void Update()
        {
            if (target == null)
                return;

            var newPosition = transform.position;
            newPosition.x = math.lerp(transform.position.x, target.transform.position.x, interpolationSpeed);

            transform.position = newPosition;
        }
    }
}