using _Project.GameSceneManager.Scripts.Models;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Context
{
    public class GameSceneManagerBootstrap : ContextView
    {
        [SerializeField] private GameObject ownPlayerPrefab;
        [SerializeField] private GameObject enemyPlayerPrefab;
        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new GameSceneManagerContext(this);

            PlayerIdMapModel playerIdMapModel = (context as GameSceneManagerContext).injectionBinder.GetInstance<PlayerIdMapModel>();
            playerIdMapModel.SetOwnPlayerPrefab(ownPlayerPrefab);
            playerIdMapModel.SetEnemyPlayerPrefab(enemyPlayerPrefab);
        }
    }
}