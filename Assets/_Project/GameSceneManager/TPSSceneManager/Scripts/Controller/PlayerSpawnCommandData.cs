using _Project.GameSceneManager.TPSSceneManager.Scripts.Models;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Views;
using _Project.NetworkManagement.TPSServer.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Controller
{
    public class PlayerSpawnCommandData
    {
        public string PlayerId { get; set; }
        public Vector3 Position { get; set; }
        public Vector4 Rotation { get; set; }

        public PlayerSpawnCommandData(string playerId, Vector3 position, Vector4 rotation)
        {
            PlayerId = playerId;
            Position = position;
            Rotation = rotation;
        }
    }
}