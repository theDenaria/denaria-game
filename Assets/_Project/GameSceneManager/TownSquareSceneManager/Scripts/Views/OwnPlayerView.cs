using System.Collections.Generic;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;
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

        [SerializeField] private float timeElapsed = 0f;
        [SerializeField] private float timeToReachTarget = 0.05f;
        [SerializeField] private float movementThreshold = 0.1f;

        private readonly List<TransformUpdate> futureTransformUpdates = new List<TransformUpdate>();

        private float squareMovementThreshold;
        private TransformUpdate to;
        private TransformUpdate from;
        private TransformUpdate previous;

        // internal float tickRate = 0.03f;

        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        private void OnEnable()
        {
            mainCamera = Camera.main;
            lastSentRotation = transform.rotation;

            var cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            // Set the Cinemachine camera's follow and look at targets

            //Transform childTransform = transform.Find("PlayerLookAt");
            cinemachineVirtualCamera.Follow = camFollowPos;

            // Optionally, enable the Cinemachine Brain if it was disabled
            Camera.main.GetComponent<CinemachineBrain>().enabled = true;

            squareMovementThreshold = movementThreshold * movementThreshold;
            to = new TransformUpdate(DenariaServerService.ServerTick, false, transform.position);
            from = new TransformUpdate(DenariaServerService.InterpolationTick, false, transform.position);
            previous = new TransformUpdate(DenariaServerService.InterpolationTick, false, transform.position);
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


        void Update()
        {
            for (int i = 0; i < futureTransformUpdates.Count; i++)
            {
                if (DenariaServerService.ServerTick >= futureTransformUpdates[i].Tick)
                {
                    if (futureTransformUpdates[i].IsTeleport)
                    {
                        to = futureTransformUpdates[i];
                        from = to;
                        previous = to;
                        transform.position = to.Position;
                    }
                    else
                    {
                        previous = to;
                        to = futureTransformUpdates[i];
                        from = new TransformUpdate(DenariaServerService.InterpolationTick, false, transform.position);
                    }

                    futureTransformUpdates.RemoveAt(i);
                    i--;
                    timeElapsed = 0f;
                    timeToReachTarget = (to.Tick - from.Tick) * DenariaServerService.TickRate;

                    Debug.Log($"TIME TO REACH TARGET: {timeToReachTarget}");
                    Debug.Log($"TARGET DISTANCE: {(to.Position - from.Position).magnitude}");
                    Debug.Log($"DISTANCE PER TICK: {(to.Position - from.Position).magnitude / (to.Tick - from.Tick)}");
                    Debug.Log($"EFFECTIVE SPEED: {(to.Position - from.Position).magnitude / timeToReachTarget}");
                }
            }

            timeElapsed += Time.deltaTime;
            InterpolatePosition(timeElapsed / timeToReachTarget);
        }

        private void InterpolatePosition(float lerpAmount)
        {
            if ((to.Position - previous.Position).sqrMagnitude < squareMovementThreshold)
            {
                if (to.Position != from.Position)
                    transform.position = Vector3.Lerp(from.Position, to.Position, lerpAmount);

                return;
            }
            if (lerpAmount <= 1f)
            {
                transform.position = Vector3.LerpUnclamped(from.Position, to.Position, lerpAmount);
            }
            else
            {
                transform.position = Vector3.Lerp(from.Position, to.Position, lerpAmount);
                Debug.Log("LERP AMOUNT: " + lerpAmount);
            }
        }

        public void NewUpdate(ushort tick, bool isTeleport, Vector3 position)
        {
            if (tick <= DenariaServerService.InterpolationTick && !isTeleport)
            {
                Debug.Log("TICK IS LESS THAN INTERPOLATION TICK");
                return;
            }
            for (int i = 0; i < futureTransformUpdates.Count; i++)
            {
                if (tick < futureTransformUpdates[i].Tick)
                {
                    futureTransformUpdates.Insert(i, new TransformUpdate(tick, isTeleport, position));
                    return;
                }
            }
            futureTransformUpdates.Add(new TransformUpdate(tick, isTeleport, position));
        }

        // public void OnServerStateUpdate(Vector3 position)
        // {
        //     // bTime = aTime;
        //     // aTime = Time.time;
        //     if (t < 1f)
        //     {
        //         Debug.Log("t entered " + t);
        //         State latestState = stateBuffer[stateBuffer.Count - 1];
        //         State previousState = stateBuffer[stateBuffer.Count - 2];

        //         float newPreviousTime = previousState.timestamp + ((latestState.timestamp - previousState.timestamp) * t);
        //         State newPreviousState = new() { position = transform.position, timestamp = newPreviousTime };
        //         stateBuffer.Add(newPreviousState);
        //     }

        //     State newState = new() { position = position, timestamp = Time.time };
        //     stateBuffer.Add(newState);
        // }
    }

    public class TransformUpdate
    {
        public ushort Tick { get; private set; }
        public bool IsTeleport { get; private set; }
        public Vector3 Position { get; private set; }

        public TransformUpdate(ushort tick, bool isTeleport, Vector3 position)
        {
            Tick = tick;
            IsTeleport = isTeleport;
            Position = position;
        }
    }
}