using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.InputSystem;
using _Project.StrangeIOCUtility;
using strange.extensions.signal.impl;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class OwnPlayerInputHandlerView : ViewZeitnot
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset playerControls;

        [Header("Action Map References")]
        [SerializeField] private string playerActionMapName = "Player";
        [SerializeField] private string uiActionMapName = "UI";

        [Header("Action Name References")]
        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string fire = "Fire";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string escMenu = "EscMenu";

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction fireAction;
        private InputAction sprintAction;
        private InputAction escMenuAction;

        internal Signal<Vector2> onMoveInput = new Signal<Vector2>();
        internal Signal<Vector2> onLookInput = new Signal<Vector2>();
        internal Signal onJumpInput = new Signal();
        internal Signal onFireInput = new Signal();
        internal Signal<float> onSprintInput = new Signal<float>();
        internal Signal onEscMenuInput = new Signal();

        protected override void Awake()
        {
            base.Awake();
            InitializeInputActions();
        }

        private void InitializeInputActions()
        {
            moveAction = playerControls.FindActionMap(playerActionMapName).FindAction(move);
            lookAction = playerControls.FindActionMap(playerActionMapName).FindAction(look);
            jumpAction = playerControls.FindActionMap(playerActionMapName).FindAction(jump);
            fireAction = playerControls.FindActionMap(playerActionMapName).FindAction(fire);
            sprintAction = playerControls.FindActionMap(playerActionMapName).FindAction(sprint);
            escMenuAction = playerControls.FindActionMap(uiActionMapName).FindAction(escMenu);

            RegisterInputActions();
        }

        private void RegisterInputActions()
        {
            moveAction.performed += context => onMoveInput.Dispatch(context.ReadValue<Vector2>());
            moveAction.canceled += context => onMoveInput.Dispatch(Vector2.zero);

            lookAction.performed += context => onLookInput.Dispatch(context.ReadValue<Vector2>());
            lookAction.canceled += context => onLookInput.Dispatch(Vector2.zero);

            jumpAction.performed += context => onJumpInput.Dispatch();

            fireAction.performed += context => onFireInput.Dispatch();

            sprintAction.performed += context => onSprintInput.Dispatch(context.ReadValue<float>());
            sprintAction.canceled += context => onSprintInput.Dispatch(0f);

            escMenuAction.performed += context => onEscMenuInput.Dispatch();
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            fireAction.Enable();
            sprintAction.Enable();
            escMenuAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            fireAction.Disable();
            sprintAction.Disable();
            escMenuAction.Disable();
        }
    }
}