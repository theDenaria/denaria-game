using System;
using System.Collections.Generic;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views;
using UnityEngine;
namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Models
{
    public class PlayerIdMapModel : IPlayerIdMapModel
    {
        public Dictionary<string, PlayerView> PlayerIdMap { get; set; }

        public string OwnPlayerId { get; set; }
        public OwnPlayerView OwnPlayerView { get; set; }

        public GameObject OwnPlayerPrefab { get; set; }
        public GameObject EnemyPlayerPrefab { get; set; }

        public void Init(string ownPlayerId, GameObject ownPlayerPrefab, GameObject enemyPlayerPrefab)
        {
            OwnPlayerId = ownPlayerId;
            PlayerIdMap = new Dictionary<string, PlayerView>();
            OwnPlayerView = null;
            OwnPlayerPrefab = ownPlayerPrefab;
            EnemyPlayerPrefab = enemyPlayerPrefab;
        }

        public void SetOwnPlayerView(OwnPlayerView ownPlayerView)
        {
            OwnPlayerView = ownPlayerView;
        }

        public OwnPlayerView GetOwnPlayerView()
        {
            if (OwnPlayerView == null)
            {
                return null;
            }
            return OwnPlayerView;
        }

        public bool IsOwnPlayerInitialized()
        {
            return OwnPlayerView != null;
        }

        public bool IsOwnPlayer(string playerId)
        {
            return OwnPlayerId == playerId;
        }

        public void AddPlayerView(string playerId, PlayerView playerView)
        {
            PlayerIdMap.Add(playerId, playerView);
        }

        public PlayerView GetPlayerView(string playerId)
        {
            if (PlayerIdMap.TryGetValue(playerId, out PlayerView playerView))
            {
                return playerView;
            }
            return null;
        }

        public bool IsPlayerInitialized(string playerId)
        {
            return PlayerIdMap.ContainsKey(playerId);
        }
    }
}