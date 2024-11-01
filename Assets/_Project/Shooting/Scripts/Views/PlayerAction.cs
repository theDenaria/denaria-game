using System;
using System.Linq;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Shooting.Scripts.Views
{
    [DisallowMultipleComponent]
    public class PlayerAction : ViewZeitnot
    {
        [SerializeField] private PlayerGunSelectorView GunSelector { get; set; }

        [SerializeField] private InputActionAsset playerControls;
        [SerializeField] private string playerActionMapName = "Player";
        [SerializeField] private string fire = "Fire";
        private InputAction fireAction;
        
        internal Signal onFireButtonIsPressedSignal = new Signal();
        internal Signal onFireButtonReleasedSignal = new Signal();
        
        internal void Init()
        {
        }

        private void OnEnable()
        {
            fireAction.Enable();
        }        
        
        private void OnDisable()
        {
            fireAction.Disable();
        }
        
        protected override void Awake()
        {
            base.Awake();
            InitializeInputActions();
        }

        private void InitializeInputActions()
        {
            fireAction = playerControls.FindActionMap(playerActionMapName).FindAction(fire);
        }
        
        private void Update()
        {
            //if (Mouse.current.leftButton.isPressed && GunSelector.ActiveGun!= null)
            if (fireAction.IsPressed())// && GunSelector.ActiveGun!= null // INCLUDE
            {
                UnityEngine.Debug.Log("xxx fireAction.WasPressedThisFrame()");
                onFireButtonIsPressedSignal.Dispatch();
                //GunSelector.ActiveGun.Shoot();
            }
       
            if (fireAction.WasReleasedThisFrame())// && GunSelector.ActiveGun!= null // INCLUDE
            {
                UnityEngine.Debug.Log("xxx fireAction.WasReleasedThisFrame()");
                onFireButtonReleasedSignal.Dispatch();
                //GunSelector.ActiveGun.StopShooting();
            }
        }
    }
}