using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Controller
{
    public class PlayerPositionUpdateCommand : Command
    {
        [Inject] public PlayerPositionUpdateCommandData PlayerPositionUpdateCommandData { get; set; }
        [Inject] public PlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerPositionUpdateCommandData.PlayerId))
            {
                OwnPlayerView ownPlayer = PlayerIdMapModel.GetOwnPlayer();
                if (ownPlayer != null)
                {
                    ownPlayer.transform.position = PlayerPositionUpdateCommandData.Position;
                }
                else
                {
                    GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.OwnPlayerPrefab, PlayerPositionUpdateCommandData.Position, Quaternion.Euler(0f, 0f, 0f));

                    OwnPlayerView newPlayer = newPlayerObj.GetComponent<OwnPlayerView>();
                    newPlayer.SetPlayerId(PlayerPositionUpdateCommandData.PlayerId);

                    PlayerIdMapModel.SetOwnPlayer(newPlayer);
                }
            }
            else
            {
                PlayerView player = PlayerIdMapModel.GetPlayer(PlayerPositionUpdateCommandData.PlayerId);
                if (player != null)
                {
                    player.transform.position = PlayerPositionUpdateCommandData.Position;
                }
                else
                {
                    GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.EnemyPlayerPrefab, PlayerPositionUpdateCommandData.Position, Quaternion.Euler(0f, 0f, 0f));

                    PlayerView newPlayer = newPlayerObj.GetComponent<PlayerView>();
                    newPlayer.SetPlayerId(PlayerPositionUpdateCommandData.PlayerId);

                    PlayerIdMapModel.AddPlayer(PlayerPositionUpdateCommandData.PlayerId, newPlayer);

                }
            }
        }
    }
}