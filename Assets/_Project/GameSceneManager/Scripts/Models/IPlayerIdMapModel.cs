using System;
using System.Collections.Generic;
using _Project.GameSceneManager.Scripts.Controller;
using _Project.GameSceneManager.Scripts.Views;
using UnityEngine;
namespace _Project.GameSceneManager.Scripts.Models
{
    public interface IPlayerIdMapModel
    {
        public Dictionary<string, PlayerView> PlayerIdMap { get; set; }

        public string OwnPlayerId { get; set; }
        public OwnPlayerView OwnPlayerView { get; set; }

        public GameObject OwnPlayerPrefab { get; set; }
        public GameObject EnemyPlayerPrefab { get; set; }

        public void Init(string ownPlayerId, GameObject ownPlayerPrefab, GameObject enemyPlayerPrefab);

        public void SetOwnPlayerView(OwnPlayerView ownPlayerView);

        public OwnPlayerView GetOwnPlayerView();

        public bool IsOwnPlayerInitialized();

        public bool IsOwnPlayer(string playerId);

        public void AddPlayerView(string playerId, PlayerView playerView);
        public PlayerView GetPlayerView(string playerId);

        public bool IsPlayerInitialized(string playerId);
    }
}