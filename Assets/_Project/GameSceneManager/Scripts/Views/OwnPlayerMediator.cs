using strange.extensions.mediation.impl;
using UnityEngine;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.NetworkManagement.Scripts.Signals;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class OwnPlayerMediator : Mediator
    {
        [Inject] public OwnPlayerView View { get; set; }

        [Inject] public SendMoveSignal SendMoveSignal { get; set; }
        [Inject] public SendLookSignal SendLookSignal { get; set; }
        [Inject] public SendFireSignal SendFireSignal { get; set; }

        public override void OnRegister()
        {
            View.onMoveInputToSend.AddListener(HandleMoveInputToSend);
            View.onLookToSend.AddListener(HandleLookToSend);
        }

        public override void OnRemove()
        {
            View.onMoveInputToSend.RemoveListener(HandleMoveInputToSend);
            View.onLookToSend.RemoveListener(HandleLookToSend);
        }


        public void HandleLook(Vector2 rotation)
        {
            View.SetPlayerLook(rotation);
        }

        public void HandleMove(Vector2 moveInput)
        {
            View.SendRotatedMoveInput(moveInput);
        }

        [ListensTo(typeof(PlayerMoveInputSignal))]
        private void HandleMoveInputToSend(Vector2 moveInput)
        {
            SendMoveSignal.Dispatch(moveInput);
        }

        [ListensTo(typeof(PlayerLookInputSignal))]
        private void HandleLookToSend(Vector4 lookInput)
        {
            SendLookSignal.Dispatch(lookInput);
        }

        [ListensTo(typeof(PlayerFireInputSignal))]
        public void HandleFireInput()
        {
            (Vector3 origin, Vector3 direction, Vector3 barrelPosition) = View.GetFireInput();
            SendFireSignal.Dispatch(origin, direction, barrelPosition);
        }

    }
}