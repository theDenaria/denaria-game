using System.Collections.Generic;
using _Project.StrangeIOCUtility.Scripts.Views;
using Cinemachine;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views
{
    public class OwnPlayerView : ViewZeitnot
    {
        public string PlayerId { get; private set; }

        public float Health { get; set; }

        [SerializeField] protected Transform shoulderTransform;

        [SerializeField] protected Transform barrelPosition;

        private Camera mainCamera;
        private Quaternion lastSentRotation;
        public float xAxis, yAxis = 0f;
        [SerializeField] Transform camFollowPos;
        [SerializeField] float mouseSensitivity = 0.2f;

        internal Signal<Vector2> onMoveInputToSend = new Signal<Vector2>();
        internal Signal<Vector4> onLookToSend = new Signal<Vector4>();
        internal Signal<Vector3, Vector3, Vector3> onFire = new Signal<Vector3, Vector3, Vector3>();

        private Vector2 _moveInput;

        private void OnEnable()
        {
            mainCamera = Camera.main;
            lastSentRotation = transform.rotation;

            var cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            // Set the Cinemachine camera's follow and look at targets

            // Transform childTransform = transform.Find("PlayerLookAt");
            cinemachineVirtualCamera.Follow = camFollowPos;

            // Optionally, enable the Cinemachine Brain if it was disabled
            Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        }

        public void SetPlayerId(string playerId)
        {
            PlayerId = playerId;
        }
        public void SetPlayerLook(Vector2 lookInput)
        {
            xAxis += lookInput.x * mouseSensitivity;
            yAxis -= lookInput.y * mouseSensitivity;
            yAxis = Mathf.Clamp(yAxis, -80, 80);

            float yRotation = mainCamera.transform.eulerAngles.y;
            float xRotation = mainCamera.transform.eulerAngles.x;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            shoulderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);

        }

        public void SendRotationToServer()
        {
            Quaternion currentRotation = transform.rotation;
            float angleDifference = Quaternion.Angle(lastSentRotation, currentRotation);
            if (angleDifference > 5.0)
            {
                // Combine into a single Vector4
                Vector4 quaternionData = new(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);
                onLookToSend.Dispatch(quaternionData);
                lastSentRotation = currentRotation;
            }
        }

        public void SetRotatedMoveInput(Vector2 normalizedInput)
        {
            _moveInput = normalizedInput;

        }

        public void SendMoveInputToServer()
        {
            if (_moveInput == Vector2.zero)
            {
                return;
            }
            float angleDegrees = transform.eulerAngles.y;
            float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians
            float cosAngle = Mathf.Cos(angleRadians);
            float sinAngle = Mathf.Sin(angleRadians);

            float rotatedX = _moveInput.x * cosAngle + _moveInput.y * sinAngle;
            float rotatedY = -_moveInput.x * sinAngle + _moveInput.y * cosAngle;

            var rotatedMoveInput = new Vector2(rotatedX, rotatedY);
            onMoveInputToSend.Dispatch(rotatedMoveInput);
        }

        public (Vector3, Vector3, Vector3) GetFireInput()
        {
            Vector3 screenPoint = new(Screen.width / 2, Screen.height / 2, 0);

            // Create a ray from the camera to the screen point
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            return (ray.origin, ray.direction, barrelPosition.position);
        }


        // Position update interpolation


        private struct State
        {
            public Vector3 position;
            public float timestamp;
        }

        private List<State> stateBuffer = new List<State>();
        public float interpolationBackTime = 0.5f; // Time to interpolate back in seconds
        public float smoothingFactor = 0.5f; // Smoothing factor for position updates

        void Update()
        {
            float interpolationTime = Time.time - interpolationBackTime;
            stateBuffer.RemoveAll(state => state.timestamp < interpolationTime);

            if (stateBuffer.Count >= 2)
            {
                State latestState = stateBuffer[stateBuffer.Count - 1];
                State previousState = stateBuffer[stateBuffer.Count - 2];
                float t = (interpolationTime - previousState.timestamp) / (latestState.timestamp - previousState.timestamp);
                t = Mathf.Clamp(t, 0f, 1f);
                Vector3 interpolatedPosition = Vector3.Lerp(previousState.position, latestState.position, t);
                transform.position = Vector3.Lerp(transform.position, interpolatedPosition, smoothingFactor);
            }
            else if (stateBuffer.Count == 1)
            {
                transform.position = Vector3.Lerp(transform.position, stateBuffer[0].position, smoothingFactor);
            }
        }

        public void OnServerStateUpdate(Vector3 position)
        {
            // State newState = new() { position = position, timestamp = Time.time };
            //stateBuffer.Add(newState);
            transform.position = position;
        }


    }
}