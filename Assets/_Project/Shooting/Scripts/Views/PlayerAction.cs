using System;
using System.Linq;
using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Shooting.Scripts.Views
{
    [DisallowMultipleComponent]
    public class PlayerAction : ViewZeitnot
    {
        [SerializeField] private PlayerGunSelectorView GunSelector;

        [SerializeField] private InputActionAsset playerControls;
        [SerializeField] private string playerActionMapName = "Player";
        [SerializeField] private string fire = "Fire";
        private InputAction fireAction;

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
            if (fireAction.WasPressedThisFrame() && GunSelector.ActiveGun!= null)
            {
                GunSelector.ActiveGun.Shoot();
            }
        }
    }
}