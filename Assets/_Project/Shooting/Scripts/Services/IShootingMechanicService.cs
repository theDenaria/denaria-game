using LlamAcademy.Guns;
using SRDebugger.Internal;
using UnityEngine;

namespace _Project.Shooting.Scripts.Services
{
    public interface IShootingMechanicService
    {
        public void Shoot();
        public void SetUpShootingMechanicService();
        void SetGunModel(GameObject spawn);
        public DamageConfigurationScriptableObject GetDamageConfiguration();
    }
}