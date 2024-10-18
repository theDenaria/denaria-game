using strange.extensions.mediation.impl;
using UnityEngine;
using System.Collections;
using _Project.Shooting.Scripts.Signals;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using _Project.NetworkManagement.TPSServer.Scripts.Commands;
using _Project.InputManager.Scripts.Signals;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Views
{
    public class OwnPlayerMediator : Mediator
    {
        [Inject] public OwnPlayerView View { get; set; }

        [Inject] public TPSServerOwnPlayerSpawnedSignal TPSServerOwnPlayerSpawnedSignal { get; set; }

        [Inject] public TPSServerSendMoveSignal TPSServerSendMoveSignal { get; set; }
        [Inject] public TPSServerSendLookSignal TPSServerSendLookSignal { get; set; }
        [Inject] public TPSServerSendFireSignal TPSServerSendFireSignal { get; set; }
        [Inject] public TPSServerSendJumpSignal TPSServerSendJumpSignal { get; set; }
        [Inject] public IRoutineRunner RoutineRunner { get; set; }

        private float interval = 1.0f / 30.0f; // 30 times per second
        private float nextExecutionTime;

        private Coroutine _sendInputsToServerCoroutine;

        public override void OnRegister()
        {
            View.onMoveInputToSend.AddListener(SendMoveToServer);
            View.onLookToSend.AddListener(SendLookToServer);
            TPSServerOwnPlayerSpawnedSignal.Dispatch();
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

        [ListensTo(typeof(PlayerFireInputSignal))]
        public void HandleFireInput()
        {
            (Vector3 origin, Vector3 direction, Vector3 barrelPosition) = View.GetFireInput();
            TPSServerSendFireSignal.Dispatch(new TPSServerSendFireCommandData(origin, direction, barrelPosition));
        }

        [ListensTo(typeof(PlayerJumpInputSignal))]
        public void HandleJumpInput()
        {
            TPSServerSendJumpSignal.Dispatch();
        }


        private void SendMoveToServer(Vector2 moveInput)
        {
            TPSServerSendMoveSignal.Dispatch(moveInput);
        }

        private void SendLookToServer(Vector4 lookInput)
        {
            TPSServerSendLookSignal.Dispatch(lookInput);
        }

    }
}