using _Project.GameSceneManager.Scripts.Commands;
using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Context;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.GameSceneManager.Scripts.Context
{
    public class GameSceneManagerContext : SignalContext
    {
        public GameSceneManagerContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            BindGameSceneManager();
        }


        private void BindGameSceneManager()
        {
            injectionBinder.Bind<IPlayerIdMapModel>().To<PlayerIdMapModel>().ToSingleton();

            injectionBinder.Bind<PlayerMoveInputSignal>().ToSingleton();
            injectionBinder.Bind<PlayerLookInputSignal>().ToSingleton();
            injectionBinder.Bind<PlayerJumpInputSignal>().ToSingleton();
            injectionBinder.Bind<PlayerFireInputSignal>().ToSingleton();
            injectionBinder.Bind<PlayerSprintInputSignal>().ToSingleton();
            injectionBinder.Bind<PlayerEscMenuInputSignal>().ToSingleton();

            mediationBinder.Bind<GameSceneView>().To<GameSceneMediator>();
            mediationBinder.Bind<OwnPlayerView>().To<OwnPlayerMediator>();
            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
            mediationBinder.Bind<OwnPlayerInputHandlerView>().To<OwnPlayerInputHandlerMediator>();

            commandBinder.Bind<ReceiveSpawnSignal>().To<PlayerSpawnCommand>();
            commandBinder.Bind<ReceivePositionUpdateSignal>().To<PlayerPositionUpdateCommand>();
            commandBinder.Bind<ReceiveRotationUpdateSignal>().To<PlayerRotationUpdateCommand>();
            commandBinder.Bind<ReceiveHealthUpdateSignal>().To<PlayerHealthUpdateCommand>();
        }
    }
}