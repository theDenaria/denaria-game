using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Shooting.Scripts.Views
{
    public class CrosshairMediator : Mediator
    {
        [Inject] public CrosshairView View { get; set; }
        [Inject] public OnTargetHitSignal OnTargetHitSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.Init();
        }

        /*[ListensTo(typeof(OnTargetHitSignal))]
        public void OpenForceUpdatePopUp()
        {
            UnityEngine.Debug.Log("OnTargetHitSignal");
            View.PaintAndToggleImages();
        }*/
        
        
        public GunScriptableObject myEventSO;

        private void OnEnable()
        {
            myEventSO.OnTargetHitOccurred += RespondToEvent;
        }

        private void OnDisable()
        {
            myEventSO.OnTargetHitOccurred -= RespondToEvent;
        }

        private void RespondToEvent()
        {
            View.PaintAndToggleImages();
        }
        
    }
}