using strange.extensions.mediation.impl;
using UnityEngine;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.NetworkManagement.Scripts.Signals;
using Unity.VisualScripting;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class OwnPlayerMediator : Mediator
    {
        [Inject] public OwnPlayerView View { get; set; }

        [Inject] public OwnPlayerSpawnedSignal OwnPlayerSpawnedSignal { get; set; }

        [Inject] public PlayerMoveInputSignal PlayerMoveSignal { get; set; }
        [Inject] public PlayerLookInputSignal PlayerLookSignal { get; set; }
        [Inject] public PlayerFireInputSignal PlayerFireSignal { get; set; }

        [Inject] public SendMoveSignal SendMoveSignal { get; set; }
        [Inject] public SendLookSignal SendLookSignal { get; set; }
        [Inject] public SendFireSignal SendFireSignal { get; set; }

        public override void OnRegister()
        {
            Debug.Log("UUU OwnPlayerMediator OnRegister");
            View.onMoveInputToSend.AddListener(HandleMoveInputToSend);
            View.onLookToSend.AddListener(HandleLookToSend);
            OwnPlayerSpawnedSignal.Dispatch();
        }

        public override void OnRemove()
        {
            Debug.Log("UUU OwnPlayerMediator OnRemove");
            View.onMoveInputToSend.RemoveListener(HandleMoveInputToSend);
            View.onLookToSend.RemoveListener(HandleLookToSend);
        }

        [ListensTo(typeof(PlayerMoveInputSignal))]
        public void HandleMove(Vector2 moveInput)
        {
            Debug.Log("UUUY HandleMove in own player mediator " + moveInput);
            View.SendRotatedMoveInput(moveInput);
        }


        [ListensTo(typeof(PlayerLookInputSignal))]
        public void HandleLook(Vector2 rotation)
        {
            View.SetPlayerLook(rotation);
        }

        [ListensTo(typeof(PlayerFireInputSignal))]
        public void HandleFireInput()
        {
            (Vector3 origin, Vector3 direction, Vector3 barrelPosition) = View.GetFireInput();
            SendFireSignal.Dispatch(origin, direction, barrelPosition);
        }


        private void HandleMoveInputToSend(Vector2 moveInput)
        {
            SendMoveSignal.Dispatch(moveInput);
        }

        private void HandleLookToSend(Vector4 lookInput)
        {
            SendLookSignal.Dispatch(lookInput);
        }

    }
}