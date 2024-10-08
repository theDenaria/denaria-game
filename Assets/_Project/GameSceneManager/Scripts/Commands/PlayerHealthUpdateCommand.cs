using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.GameSceneManager.Scripts.Commands
{
    public class PlayerHealthUpdateCommand : Command
    {
        [Inject] public PlayerHealthUpdateCommandData PlayerHealthUpdateCommandData { get; set; }
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerHealthUpdateCommandData.PlayerId))
            {
                PlayerIdMapModel.GetOwnPlayerView().Health = PlayerHealthUpdateCommandData.Health;
            }
            else
            {
                PlayerView playerView = PlayerIdMapModel.GetPlayerView(PlayerHealthUpdateCommandData.PlayerId);
                playerView.Health = PlayerHealthUpdateCommandData.Health;
            }
        }
    }
}