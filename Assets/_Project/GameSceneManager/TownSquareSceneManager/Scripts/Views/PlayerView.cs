using System.Collections.Generic;
using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views
{
    public class PlayerView : ViewZeitnot
    {
        public string PlayerId { get; private set; }

        public float Health { get; set; }

        [SerializeField] protected Transform shoulderTransform;

        [SerializeField] protected Transform barrelPosition;

        public void SetPlayerId(string playerId)
        {
            PlayerId = playerId;
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
            State newState = new() { position = position, timestamp = Time.time };
            stateBuffer.Add(newState);
        }

    }
}