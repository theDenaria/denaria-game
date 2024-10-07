using strange.extensions.mediation.impl;
using _Project.GameSceneManager.Scripts.Signals;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class OwnPlayerInputHandlerMediator : Mediator
    {
        [Inject] public OwnPlayerInputHandlerView View { get; set; }
        [Inject] public PlayerMoveInputSignal PlayerMoveSignal { get; set; }
        [Inject] public PlayerLookInputSignal PlayerLookSignal { get; set; }
        [Inject] public PlayerFireInputSignal PlayerFireSignal { get; set; }
        [Inject] public PlayerJumpInputSignal PlayerJumpSignal { get; set; }
        [Inject] public PlayerSprintInputSignal PlayerSprintSignal { get; set; }
        [Inject] public PlayerEscMenuInputSignal PlayerEscMenuSignal { get; set; }

        public override void OnRegister()
        {
            View.onMoveInput.AddListener(OnMove);
            View.onLookInput.AddListener(OnLook);
            View.onFireInput.AddListener(OnFire);
            View.onJumpInput.AddListener(OnJump);
            View.onSprintInput.AddListener(OnSprint);
            View.onEscMenuInput.AddListener(OnEscMenu);
        }

        public override void OnRemove()
        {
            View.onMoveInput.RemoveListener(OnMove);
            View.onLookInput.RemoveListener(OnLook);
            View.onFireInput.RemoveListener(OnFire);
            View.onJumpInput.RemoveListener(OnJump);
            View.onSprintInput.RemoveListener(OnSprint);
            View.onEscMenuInput.RemoveListener(OnEscMenu);
        }

        private void OnMove(Vector2 input)
        {
            Debug.Log("UUUY OnMove " + input);
            PlayerMoveSignal.Dispatch(input);
        }

        private void OnLook(Vector2 input)
        {
            PlayerLookSignal.Dispatch(input);
        }

        private void OnFire()
        {
            PlayerFireSignal.Dispatch();
        }

        private void OnJump()
        {
            PlayerJumpSignal.Dispatch();
        }

        private void OnSprint(float sprintValue)
        {
            PlayerSprintSignal.Dispatch(sprintValue);
        }

        private void OnEscMenu()
        {
            PlayerEscMenuSignal.Dispatch();
        }
    }
}