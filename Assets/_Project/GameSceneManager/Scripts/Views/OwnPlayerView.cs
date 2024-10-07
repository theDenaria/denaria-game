using _Project.StrangeIOCUtility;
using Cinemachine;
using strange.extensions.signal.impl;
using UnityEngine;
namespace _Project.GameSceneManager.Scripts.Views
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

        private void OnEnable()
        {
            mainCamera = Camera.main;
            lastSentRotation = transform.rotation;

            var cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            // Set the Cinemachine camera's follow and look at targets

            Transform childTransform = transform.Find("PlayerLookAt");
            cinemachineVirtualCamera.Follow = childTransform;

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

            camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);

            float yRotation = mainCamera.transform.eulerAngles.y;
            float xRotation = mainCamera.transform.eulerAngles.x;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            shoulderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            SendRotationToServer();
        }

        void SendRotationToServer()
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

        public void SendRotatedMoveInput(Vector2 normalizedInput)
        {
            float angleDegrees = transform.eulerAngles.y;
            float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians
            float cosAngle = Mathf.Cos(angleRadians);
            float sinAngle = Mathf.Sin(angleRadians);

            float rotatedX = normalizedInput.x * cosAngle + normalizedInput.y * sinAngle;
            float rotatedY = -normalizedInput.x * sinAngle + normalizedInput.y * cosAngle;

            onMoveInputToSend.Dispatch(new Vector2(rotatedX, rotatedY));
        }

        public (Vector3, Vector3, Vector3) GetFireInput()
        {
            Vector3 screenPoint = new(Screen.width / 2, Screen.height / 2, 0);

            // Create a ray from the camera to the screen point
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            return (ray.origin, ray.direction, barrelPosition.position);
        }


    }
}