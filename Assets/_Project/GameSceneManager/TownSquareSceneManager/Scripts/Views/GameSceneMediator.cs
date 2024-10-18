using strange.extensions.mediation.impl;
using _Project.NetworkManagement.DenariaServer.Scripts.Signals;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Models;
using _Project.PlayerSessionInfo.Scripts.Models;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views
{
    public class GameSceneMediator : Mediator
    {
        [Inject] public GameSceneView View { get; set; }


        [Inject] public TownSquareLoadedSignal TownSquareLoadedSignal { get; set; }

        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        [Inject] public IPlayerSessionInfoModel PlayerSessionInfoModel { get; set; }

        public override void OnRegister()
        {
            View.onSceneLoaded.AddListener(HandleSceneLoaded);
        }

        public override void OnRemove()
        {
            View.onSceneLoaded.RemoveListener(HandleSceneLoaded);
        }

        public void HandleSceneLoaded()
        {
            if (View.SceneName == "TownSquare")
            {
                PlayerIdMapModel.Init(PlayerSessionInfoModel.PlayerId, View.OwnPlayerPrefab, View.EnemyPlayerPrefab);
                TownSquareLoadedSignal.Dispatch();
            }
        }

    }
}