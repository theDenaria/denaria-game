using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Enums;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Controller
{
    public class PlayerSpawnCommand : Command
    {
        [Inject] public PlayerSpawnCommandData PlayerSpawnCommandData { get; set; }
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            GameObject contextObject = GameObject.Find("GameSceneManagerContext");
            Debug.Log("UUU RECEIVED SPAWN SIGNAL");
            if (PlayerIdMapModel.IsOwnPlayer(PlayerSpawnCommandData.PlayerId))
            {
                if (PlayerIdMapModel.IsOwnPlayerInitialized())
                {
                    return;
                }
                Debug.Log("UUU SPAWNING OWN PLAYER");
                GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.OwnPlayerPrefab, PlayerSpawnCommandData.Position,
                    new Quaternion(PlayerSpawnCommandData.Rotation.x, PlayerSpawnCommandData.Rotation.y, PlayerSpawnCommandData.Rotation.z, PlayerSpawnCommandData.Rotation.w),
                    contextObject.transform);

                Debug.Log("UUU AFTER SPAWNING");

                OwnPlayerView newPlayer = newPlayerObj.GetComponent<OwnPlayerView>();

                Debug.Log("UUU SETTING PLAYER ID");

                newPlayer.SetPlayerId(PlayerSpawnCommandData.PlayerId);

                Debug.Log("UUU SETTING OWN PLAYER VIEW");

                PlayerIdMapModel.SetOwnPlayerView(newPlayer);
            }
            else
            {
                if (PlayerIdMapModel.IsPlayerInitialized(PlayerSpawnCommandData.PlayerId))
                {
                    return;
                }

                GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.EnemyPlayerPrefab, PlayerSpawnCommandData.Position,
                    new Quaternion(PlayerSpawnCommandData.Rotation.x, PlayerSpawnCommandData.Rotation.y, PlayerSpawnCommandData.Rotation.z, PlayerSpawnCommandData.Rotation.w),
                    contextObject.transform);

                PlayerView newPlayer = newPlayerObj.GetComponent<PlayerView>();
                newPlayer.SetPlayerId(PlayerSpawnCommandData.PlayerId);

                PlayerIdMapModel.AddPlayerView(PlayerSpawnCommandData.PlayerId, newPlayer);
            }
        }
    }
}