using LlamAcademy.Guns.Demo.Enemy;
using UnityEngine;

namespace _Project.Shooting.Scripts.Enemy
{
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        public Models.EnemyHealth Health;
        //public EnemyMovement Movement;
        public EnemyPainResponse PainResponse;

        private void Start()
        {
            Health.OnTakeDamage += PainResponse.HandlePain;
            Health.OnDeath += Die;
        }

        private void Die(Vector3 Position)
        {
            //Movement.StopMoving();
            PainResponse.HandleDeath();
        }
    }
}