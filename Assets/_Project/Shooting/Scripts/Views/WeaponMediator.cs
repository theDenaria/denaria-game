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

        [ListensTo(typeof(PlayShootingParticleSystemSignal))]
        public void PlayShootingParticleSystem()
        {
            View.PlayParticleSystem();
        }
        
        [ListensTo(typeof(StopPlayingShootingParticleSystemSignal))]
        public void OpenForceUpdatePopUp()
        {
            View.StopParticleSystem();
        }
    }
}