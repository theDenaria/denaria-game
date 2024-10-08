using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Controller
{
    public class PlayerRotationUpdateCommandData
    {
        public string PlayerId { get; set; }
        public Vector4 Rotation { get; set; }

        public PlayerRotationUpdateCommandData(string playerId, Vector4 rotation)
        {
            PlayerId = playerId;
            Rotation = rotation;
        }
    }
}