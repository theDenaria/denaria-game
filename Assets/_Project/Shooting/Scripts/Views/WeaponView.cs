using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;

namespace _Project.Shooting.Scripts.Views
{
    public class WeaponView : ViewZeitnot
    {
        [field: SerializeField] private ParticleSystem ParticleSystem { get; set; }
        
        internal void Init()
        {
        }

        private void OnDisable()
        {
        }

        internal void PlayParticleSystem()
        {
            ParticleSystem.Play();
        }        
        
        internal void StopParticleSystem()
        {
            ParticleSystem.Stop();
        }
    }
}