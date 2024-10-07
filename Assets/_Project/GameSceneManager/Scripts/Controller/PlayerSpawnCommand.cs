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
            if (PlayerIdMapModel.IsOwnPlayer(PlayerSpawnCommandData.PlayerId))
            {
                if (PlayerIdMapModel.IsOwnPlayerInitialized())
                {
                    return;
                }
                GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.OwnPlayerPrefab, PlayerSpawnCommandData.Position,
                    new Quaternion(PlayerSpawnCommandData.Rotation.x, PlayerSpawnCommandData.Rotation.y, PlayerSpawnCommandData.Rotation.z, PlayerSpawnCommandData.Rotation.w),
                    contextObject.transform);

                OwnPlayerView newPlayer = newPlayerObj.GetComponent<OwnPlayerView>();

                newPlayer.SetPlayerId(PlayerSpawnCommandData.PlayerId);

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