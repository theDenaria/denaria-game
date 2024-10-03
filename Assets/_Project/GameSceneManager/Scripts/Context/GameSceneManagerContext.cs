using _Project.GameSceneManager.Scripts.Controller;
using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Signals;
using _Project.StrangeIOCUtility.CrossContext;
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

            injectionBinder.Bind<PlayerMoveInputSignal>().CrossContext();
            injectionBinder.Bind<PlayerLookInputSignal>().CrossContext();
            injectionBinder.Bind<PlayerJumpInputSignal>().CrossContext();
            injectionBinder.Bind<PlayerSprintInputSignal>().CrossContext();
            injectionBinder.Bind<PlayerEscMenuInputSignal>().CrossContext();

            injectionBinder.Bind<PlayerIdMapModel>().ToSingleton().CrossContext();

            mediationBinder.Bind<OwnPlayerView>().To<OwnPlayerMediator>();
            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
            mediationBinder.Bind<OwnPlayerInputHandlerView>().To<OwnPlayerInputHandlerMediator>();

            commandBinder.Bind<ReceivePositionUpdateSignal>().To<PlayerPositionUpdateCommand>();
            commandBinder.Bind<ReceiveRotationUpdateSignal>().To<PlayerRotationUpdateCommand>();
            commandBinder.Bind<ReceiveHealthUpdateSignal>().To<PlayerHealthUpdateCommand>();
        }
    }
}