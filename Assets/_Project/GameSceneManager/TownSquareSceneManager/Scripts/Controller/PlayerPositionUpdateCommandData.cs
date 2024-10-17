using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller
{
    public class PlayerPositionUpdateCommandData
    {
        public string PlayerId { get; set; }
        public Vector3 Position { get; set; }

        public PlayerPositionUpdateCommandData(string playerId, Vector3 position)
        {
            PlayerId = playerId;
            Position = position;
        }
    }
}