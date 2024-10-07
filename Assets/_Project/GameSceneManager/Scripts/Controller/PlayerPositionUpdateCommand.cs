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
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerPositionUpdateCommandData.PlayerId))
            {
                OwnPlayerView ownPlayerView = PlayerIdMapModel.GetOwnPlayerView();
                if (ownPlayerView != null)
                {
                    ownPlayerView.transform.position = PlayerPositionUpdateCommandData.Position;
                }
            }
            else
            {
                PlayerView playerView = PlayerIdMapModel.GetPlayerView(PlayerPositionUpdateCommandData.PlayerId);
                if (playerView != null)
                {
                    playerView.transform.position = PlayerPositionUpdateCommandData.Position;
                }
            }
        }
    }
}