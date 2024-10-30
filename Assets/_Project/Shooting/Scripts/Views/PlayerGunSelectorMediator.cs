using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Shooting.Scripts.Views
{
    public class PlayerGunSelectorMediator : Mediator
    {
        [Inject] public PlayerGunSelectorView View { get; set; }
        [Inject] public SpawnGunSignal SpawnGunSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onPlayerGunSelectorReady.AddListener(HandleOnPlayerGunSelectorReady);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.onPlayerGunSelectorReady.RemoveListener(HandleOnPlayerGunSelectorReady);
        }

        private void HandleOnPlayerGunSelectorReady()
        {
            SpawnGunSignal.Dispatch(new SpawnGunCommandData(View.GunParent, View));
        }

    }
}