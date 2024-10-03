using System;
using System.Collections.Generic;
using _Project.GameSceneManager.Scripts.Controller;
using _Project.GameSceneManager.Scripts.Views;
using UnityEngine;
namespace _Project.GameSceneManager.Scripts.Models
{
    public class PlayerIdMapModel
    {
        public Dictionary<string, PlayerView> PlayerIdMap { get; set; }

        public OwnPlayerView OwnPlayer { get; set; }

        public GameObject OwnPlayerPrefab { get; set; }
        public GameObject EnemyPlayerPrefab { get; set; }

        public PlayerIdMapModel()
        {
            PlayerIdMap = new Dictionary<string, PlayerView>();
            OwnPlayer = null;
            OwnPlayerPrefab = null;
            EnemyPlayerPrefab = null;
        }

        public void SetOwnPlayerPrefab(GameObject ownPlayerPrefab)
        {
            OwnPlayerPrefab = ownPlayerPrefab;
        }

        public void SetEnemyPlayerPrefab(GameObject enemyPlayerPrefab)
        {
            EnemyPlayerPrefab = enemyPlayerPrefab;
        }

        public void SetOwnPlayer(OwnPlayerView ownPlayer)
        {
            OwnPlayer = ownPlayer;
        }

        public OwnPlayerView GetOwnPlayer()
        {
            return OwnPlayer;
        }

        public bool IsOwnPlayer(string playerId)
        {
            return OwnPlayer.PlayerId == playerId;
        }

        public void AddPlayer(string playerId, PlayerView player)
        {
            PlayerIdMap.Add(playerId, player);
        }

        public PlayerView GetPlayer(string playerId)
        {
            if (PlayerIdMap.TryGetValue(playerId, out PlayerView player))
            {
                return player;
            }
            return null;
        }
    }
}