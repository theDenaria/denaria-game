using _Project.Shooting.Scripts.Views;
using UnityEngine;

namespace _Project.Shooting.Scripts.Enemy
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemyPlayerSensor : MonoBehaviour
    {
        public SphereCollider Collider;
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        private void Awake()
        {
            Collider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerAction player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerAction player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
            }
        }
    }
}