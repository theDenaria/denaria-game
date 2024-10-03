using _Project.StrangeIOCUtility;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Views
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
    }
}