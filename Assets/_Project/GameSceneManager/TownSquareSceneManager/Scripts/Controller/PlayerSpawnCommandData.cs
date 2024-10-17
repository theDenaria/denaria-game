using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Models;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views;
using _Project.NetworkManagement.DenariaServer.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller
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