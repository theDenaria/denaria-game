using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Shooting.Scripts.Views
{
    public class WeaponMediator : Mediator
    {
        [Inject] public WeaponView View { get; set; }
        [Inject] public PlayShootingParticleSystemSignal PlayShootingParticleSystemSignal { get; set; }
        [Inject] public StopPlayingShootingParticleSystemSignal StopPlayingShootingParticleSystemSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.Init();
        }
        
        [ListensTo(typeof(SetUpGunViewSignal))]
        public void SetUpGunView(SetUpWeaponViewCommandData setUpWeaponViewCommandData)
        {
            View.TrailConfiguration = setUpWeaponViewCommandData.GunScriptableObject.TrailConfiguration;
            View.DamageConfiguration = setUpWeaponViewCommandData.GunScriptableObject.DamageConfiguration;
        }

        [ListensTo(typeof(PlayShootingParticleSystemSignal))]
        public void PlayShootingParticleSystem()
        {
            View.PlayParticleSystem();
        }
        
        [ListensTo(typeof(StopPlayingShootingParticleSystemSignal))]
        public void StopPlayingShootingParticleSystem()
        {
            View.StopParticleSystem();
        }        
        
        [ListensTo(typeof(PlayTrailEffectSignal))]
        public void PlayTrailEffect(PlayTrailEffectCommandData playTrailEffectCommandData)
        {
            StartCoroutine(
                View.PlayTrail(
                    playTrailEffectCommandData.TrailOrigin,
                    playTrailEffectCommandData.Hit.point,
                    playTrailEffectCommandData.Hit
                    //,
                    //Iteration
                )
            );
        }
    }
}