using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.GameSceneManager.Scripts.Commands
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

                    ownPlayerView.OnServerStateUpdate(PlayerPositionUpdateCommandData.Position);
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