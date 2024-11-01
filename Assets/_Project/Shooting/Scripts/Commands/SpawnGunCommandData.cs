using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class SpawnGunCommandData
    {
        public Transform GunParent { get; set; }
        public MonoBehaviour ActiveMonoBehaviour { get; set; }

        public SpawnGunCommandData(Transform gunParent, MonoBehaviour activeMonoBehaviour)
        {
            GunParent = gunParent;
            ActiveMonoBehaviour = activeMonoBehaviour;
        }
    }
}