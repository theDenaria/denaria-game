using strange.extensions.mediation.impl;
using UnityEngine;
using _Project.NetworkManagement.DenariaServer.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using System.Collections;
using _Project.NetworkManagement.DenariaServer.Scripts.Commands;
using _Project.InputManager.Scripts.Signals;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views
{
    public class OwnPlayerMediator : Mediator
    {
        [Inject] public OwnPlayerView View { get; set; }

        [Inject] public DenariaServerOwnPlayerSpawnedSignal DenariaServerOwnPlayerSpawnedSignal { get; set; }

        [Inject] public DenariaServerSendMoveSignal DenariaServerSendMoveSignal { get; set; }
        [Inject] public DenariaServerSendLookSignal DenariaServerSendLookSignal { get; set; }
        [Inject] public DenariaServerSendJumpSignal DenariaServerSendJumpSignal { get; set; }
        [Inject] public IRoutineRunner RoutineRunner { get; set; }

        private float interval = 1.0f / 30.0f; // 30 times per second
        private float nextExecutionTime;

        private Coroutine _sendInputsToServerCoroutine;

        public override void OnRegister()
        {
            View.onMoveInputToSend.AddListener(SendMoveToServer);
            View.onLookToSend.AddListener(SendLookToServer);
            DenariaServerOwnPlayerSpawnedSignal.Dispatch();
            _sendInputsToServerCoroutine = RoutineRunner.StartCoroutine(SendInputsToServer());
        }

        public override void OnRemove()
        {
            View.onMoveInputToSend.RemoveListener(SendMoveToServer);
            View.onLookToSend.RemoveListener(SendLookToServer);
            if (_sendInputsToServerCoroutine != null)
            {
                RoutineRunner.StopCoroutine(_sendInputsToServerCoroutine);
            }
        }

        private IEnumerator SendInputsToServer()
        {

            while (true)
            {
                nextExecutionTime = Time.realtimeSinceStartup + interval;
                View.SendMoveInputToServer();
                View.SendRotationToServer();
                float waitTime = nextExecutionTime - Time.realtimeSinceStartup;
                if (waitTime > 0)
                {
                    yield return new WaitForSecondsRealtime(waitTime);
                }
            }
        }

        [ListensTo(typeof(PlayerMoveInputSignal))]
        public void HandleMove(Vector2 moveInput)
        {
            View.SetRotatedMoveInput(moveInput);
        }


        [ListensTo(typeof(PlayerLookInputSignal))]
        public void HandleLook(Vector2 rotation)
        {
            View.SetPlayerLook(rotation);
        }


        [ListensTo(typeof(PlayerJumpInputSignal))]
        public void HandleJumpInput()
        {
            DenariaServerSendJumpSignal.Dispatch();
        }


        private void SendMoveToServer(Vector2 moveInput)
        {
            DenariaServerSendMoveSignal.Dispatch(moveInput);
        }

        private void SendLookToServer(Vector4 lookInput)
        {
            DenariaServerSendLookSignal.Dispatch(lookInput);
        }

    }
}