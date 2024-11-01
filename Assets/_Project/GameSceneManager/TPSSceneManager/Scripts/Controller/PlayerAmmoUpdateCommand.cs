using _Project.GameSceneManager.TPSSceneManager.Scripts.Models;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Views;
using strange.extensions.command.impl;


namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Controller
{
    public class PlayerAmmoUpdateCommand : Command
    {
        [Inject] public PlayerAmmoUpdateCommandData PlayerAmmoUpdateCommandData { get; set; }
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerAmmoUpdateCommandData.PlayerId) && PlayerIdMapModel.IsOwnPlayerInitialized())
            {

                PlayerIdMapModel.GetOwnPlayerView().CurrentAmmo = PlayerAmmoUpdateCommandData.CurrentAmmo;
                PlayerIdMapModel.GetOwnPlayerView().TotalAmmo = PlayerAmmoUpdateCommandData.TotalAmmo;

            }
        }
    }
}