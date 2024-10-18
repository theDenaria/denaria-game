using _Project.GameSceneManager.TPSSceneManager.Scripts.Controller;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Models;
using _Project.GameSceneManager.TPSSceneManager.Scripts.Views;
using _Project.MainMenu.Scripts.Signals;
using _Project.MainMenu.Scripts.Views;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using _Project.SettingsManager.Scripts.Controllers;
using _Project.SettingsManager.Scripts.Models;
using _Project.SettingsManager.Scripts.Signals;
using _Project.SettingsManager.Scripts.Views;
using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;
using _Project.Animation.Scripts.Views;
using _Project.Login.Scripts.Signals;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Context
{
    public class TPSSceneManagerContext : SignalContext
    {
        public TPSSceneManagerContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            BindGameSceneManager();
            BindMainMenu();
            SettingsManagerBindings();
            BindAnimation();
        }


        private void BindGameSceneManager()
        {
            injectionBinder.Bind<IPlayerIdMapModel>().To<PlayerIdMapModel>().ToSingleton();

            mediationBinder.Bind<GameSceneView>().To<GameSceneMediator>();
            mediationBinder.Bind<OwnPlayerView>().To<OwnPlayerMediator>();
            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();

            commandBinder.Bind<TPSServerReceiveSpawnSignal>().To<PlayerSpawnCommand>();
            commandBinder.Bind<TPSServerReceivePositionUpdateSignal>().To<PlayerPositionUpdateCommand>();
            commandBinder.Bind<TPSServerReceiveRotationUpdateSignal>().To<PlayerRotationUpdateCommand>();
            commandBinder.Bind<TPSServerReceiveHealthUpdateSignal>().To<PlayerHealthUpdateCommand>();
        }

        private void BindMainMenu()
        {
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();

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

            injectionBinder.Bind<ISettingsModel>().To<SettingsModel>().ToSingleton();

            mediationBinder.Bind<VideoSettingsView>().To<VideoSettingsMediator>();
            mediationBinder.Bind<AudioSettingsView>().To<AudioSettingsMediator>();
            mediationBinder.Bind<HotkeySettingsView>().To<HotkeySettingsMediator>();
            mediationBinder.Bind<FooterView>().To<FooterMediator>();

            commandBinder.Bind<ApplySettingsSignal>().To<ApplySettingsCommand>();
            commandBinder.Bind<RestoreDefaultSettingsSignal>().To<RestoreDefaultSettingsCommand>();
            commandBinder.Bind<ChangeSettingsSignal>().To<ChangeSettingsCommand>();
            //commandBinder.Bind<SettingsMenuClosedSignal>().To<SettingsMenuClosedCommand>();
        }

        private void BindAnimation()
        {
            mediationBinder.Bind<PlayerAnimationView>().To<PlayerAnimationMediator>();
            mediationBinder.Bind<GroundColliderView>().To<GroundColliderMediator>();

            injectionBinder.Bind<OnLandColliderEnterSignal>().ToSingleton();
            injectionBinder.Bind<OnLandColliderExitSignal>().ToSingleton();
        }

    }
}