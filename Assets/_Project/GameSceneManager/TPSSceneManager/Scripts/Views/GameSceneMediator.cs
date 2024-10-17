using strange.extensions.mediation.impl;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Models;
using _Project.PlayerSessionInfo.Scripts.Models;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Views
{
    public class GameSceneMediator : Mediator
    {
        [Inject] public GameSceneView View { get; set; }
        [Inject] public TPSLoadedSignal TPSLoadedSignal { get; set; }

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
            if (View.SceneName == "ThirdPersonShooter")
            {
                PlayerIdMapModel.Init(PlayerSessionInfoModel.PlayerId, View.OwnPlayerPrefab, View.EnemyPlayerPrefab);
                TPSLoadedSignal.Dispatch();
            }
        }

        public void HandleSpawnOwnPlayer()
        {

        }
    }
}