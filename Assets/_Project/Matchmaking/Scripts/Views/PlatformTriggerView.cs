
using _Project.Matchmaking.Scripts.Enums;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.Matchmaking.Scripts.Views
{
    public class PlatformTriggerView : ViewZeitnot
    {
        [SerializeField] private MatchmakingPlatformEnum platform;
        internal Signal<MatchmakingPlatformEnum> onPlatformTriggerEnter = new Signal<MatchmakingPlatformEnum>();
        internal Signal onPlatformTriggerExit = new Signal();

        // Called when another object enters the trigger collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlatformTriggerEnter.Dispatch(platform);
            }
        }

        // Called when another object exits the trigger collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlatformTriggerExit.Dispatch();
            }
        }
    }
}