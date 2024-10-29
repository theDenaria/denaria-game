using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller
{
    public class PlayerPositionUpdateCommandData
    {
        public string PlayerId { get; set; }
        public ushort Tick { get; set; }
        public bool IsTeleport { get; set; }
        public Vector3 Position { get; set; }

        public PlayerPositionUpdateCommandData(string playerId, ushort tick, bool isTeleport, Vector3 position)
        {
            PlayerId = playerId;
            Tick = tick;
            IsTeleport = isTeleport;
            Position = position;
        }
    }
}