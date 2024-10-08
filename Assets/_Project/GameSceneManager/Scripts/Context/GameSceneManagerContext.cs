using _Project.GameSceneManager.Scripts.Controller;
using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.GameSceneManager.Scripts.Views;
using _Project.MainMenu.Scripts.Signals;
using _Project.MainMenu.Scripts.Views;
using _Project.NetworkManagement.Scripts.Signals;
using _Project.SettingsManager.Scripts.Controllers;
using _Project.SettingsManager.Scripts.Models;
using _Project.SettingsManager.Scripts.Signals;
using _Project.SettingsManager.Scripts.Views;
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
            BindMainMenu();
            SettingsManagerBindings();
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

        private void BindMainMenu()
        {
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();

            injectionBinder.Bind<MainMenuOpenedSignal>().ToSingleton();
            injectionBinder.Bind<MainMenuClosedSignal>().ToSingleton();
            injectionBinder.Bind<LogoutButtonSignal>().ToSingleton();
            injectionBinder.Bind<ExitButtonSignal>().ToSingleton();

            //commandBinder.Bind<LogoutButtonSignal>().To<LogoutButtonCommand>();
            //commandBinder.Bind<ExitButtonSignal>().To<ExitButtonCommand>();
        }

        private void SettingsManagerBindings()
        {
            injectionBinder.Bind<ApplySettingsSignal>().ToSingleton();
            injectionBinder.Bind<RestoreDefaultSettingsSignal>().ToSingleton();
            injectionBinder.Bind<ChangeSettingsSignal>().ToSingleton();
            //injectionBinder.Bind<SettingsMenuClosedSignal>().ToSingleton();

            injectionBinder.Bind<ISettingsModel>().To<SettingsModel>().ToSingleton().CrossContext();

            mediationBinder.Bind<VideoSettingsView>().To<VideoSettingsMediator>();
            mediationBinder.Bind<AudioSettingsView>().To<AudioSettingsMediator>();
            mediationBinder.Bind<HotkeySettingsView>().To<HotkeySettingsMediator>();
            mediationBinder.Bind<FooterView>().To<FooterMediator>();

            commandBinder.Bind<ApplySettingsSignal>().To<ApplySettingsCommand>();
            commandBinder.Bind<RestoreDefaultSettingsSignal>().To<RestoreDefaultSettingsCommand>();
            commandBinder.Bind<ChangeSettingsSignal>().To<ChangeSettingsCommand>();
            //commandBinder.Bind<SettingsMenuClosedSignal>().To<SettingsMenuClosedCommand>();
        }
    }
}