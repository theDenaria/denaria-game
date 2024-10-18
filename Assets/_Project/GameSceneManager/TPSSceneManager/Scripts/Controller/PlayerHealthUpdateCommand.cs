using _Project.GameSceneManager.TPSSceneManager.Scripts.Models;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Views;
using _Project.NetworkManagement.TPSServer.Scripts.Enums;
using strange.extensions.command.impl;


namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Controller
{
    public class PlayerHealthUpdateCommand : Command
    {
        [Inject] public PlayerHealthUpdateCommandData PlayerHealthUpdateCommandData { get; set; }
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerHealthUpdateCommandData.PlayerId))
            {
                if (PlayerIdMapModel.IsOwnPlayerInitialized())
                {
                    PlayerIdMapModel.GetOwnPlayerView().Health = PlayerHealthUpdateCommandData.Health;
                }
            }
            else if (PlayerIdMapModel.IsPlayerInitialized(PlayerHealthUpdateCommandData.PlayerId))
            {
                PlayerView playerView = PlayerIdMapModel.GetPlayerView(PlayerHealthUpdateCommandData.PlayerId);
                playerView.Health = PlayerHealthUpdateCommandData.Health;
            }
        }
    }
}