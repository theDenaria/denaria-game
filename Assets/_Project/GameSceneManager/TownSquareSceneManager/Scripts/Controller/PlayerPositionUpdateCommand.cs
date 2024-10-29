using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Models;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views;
using _Project.NetworkManagement.DenariaServer.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller
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

                    ownPlayerView.NewUpdate(PlayerPositionUpdateCommandData.Tick, PlayerPositionUpdateCommandData.IsTeleport, PlayerPositionUpdateCommandData.Position);
                }
            }
            else
            {
                PlayerView playerView = PlayerIdMapModel.GetPlayerView(PlayerPositionUpdateCommandData.PlayerId);
                if (playerView != null)
                {
                    playerView.OnServerStateUpdate(PlayerPositionUpdateCommandData.Position);
                }
            }
        }
    }
}