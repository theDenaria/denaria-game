using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Enums;
using strange.extensions.command.impl;


namespace _Project.GameSceneManager.Scripts.Controller
{
    public class PlayerHealthUpdateCommand : Command
    {
        [Inject] public PlayerHealthUpdateCommandData PlayerHealthUpdateCommandData { get; set; }
        [Inject] public PlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerHealthUpdateCommandData.PlayerId))
            {
                PlayerIdMapModel.GetOwnPlayer().Health = PlayerHealthUpdateCommandData.Health;
            }
            else
            {
                PlayerView player = PlayerIdMapModel.GetPlayer(PlayerHealthUpdateCommandData.PlayerId);
                player.Health = PlayerHealthUpdateCommandData.Health;
            }
        }
    }
}