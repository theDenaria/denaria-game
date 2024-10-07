using strange.extensions.mediation.impl;
using UnityEngine;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.NetworkManagement.Scripts.Signals;
using strange.extensions.signal.impl;
using _Project.GameSceneManager.Scripts.Models;
using _Project.NetworkManagement.Scripts.Services;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class GameSceneMediator : Mediator
    {
        [Inject] public GameSceneView View { get; set; }
        [Inject] public TownSquareLoadedSignal TownSquareLoadedSignal { get; set; }

        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        [Inject] public IDenariaServerService DenariaServerService { get; set; }

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
            Debug.Log("SceneChangedMediator: HandleSceneLoaded");
            if (View.SceneName == "TownSquare")
            {
                Debug.Log("SceneChangedMediator: TownSquareLoadedSignal.Dispatch");
                PlayerIdMapModel.Init(DenariaServerService.PlayerId, View.OwnPlayerPrefab, View.EnemyPlayerPrefab);
                TownSquareLoadedSignal.Dispatch();
            }
        }

        public void HandleSpawnOwnPlayer()
        {

        }
    }
}