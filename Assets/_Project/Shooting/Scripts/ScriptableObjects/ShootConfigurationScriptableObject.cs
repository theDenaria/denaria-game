using UnityEngine;

namespace _Project.Shooting.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShootConfiguration", menuName = "Denaria/Guns/Shoot Configuration", order = 2)]
    public class ShootConfigurationScriptableObject : ScriptableObject
    {
        public LayerMask HitMask;
        public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
        public float FireRate = 0.25f;
    }
}