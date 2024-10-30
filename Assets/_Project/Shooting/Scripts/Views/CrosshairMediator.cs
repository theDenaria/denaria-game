using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Views
{
    public class CrosshairMediator : Mediator
    {
        [Inject] public CrosshairView View { get; set; }
        private GunScriptableObject gunScriptableObject;
        
        public override void OnRegister()
        {
            base.OnRegister();
            View.Init();

        }

        [ListensTo(typeof(OnTargetHitSignal))]
        public void PaintAndToggleImages()
        {
            UnityEngine.Debug.Log("OnTargetHitSignal");
            View.PaintAndToggleImages();
        }

        private void RespondToEvent()
        {
            View.PaintAndToggleImages();
        }
        
    }
}