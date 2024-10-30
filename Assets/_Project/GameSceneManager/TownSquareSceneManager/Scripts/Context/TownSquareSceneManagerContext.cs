using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Controller;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Models;
using _Project.InputManager.Scripts.Signals;
using _Project.GameSceneManager.TownSquareSceneManager.Scripts.Views;
using _Project.MainMenu.Scripts.Signals;
using _Project.MainMenu.Scripts.Views;
using _Project.Matchmaking.Scripts.Commands;
using _Project.Matchmaking.Scripts.Services;
using _Project.Matchmaking.Scripts.Signals;
using _Project.Matchmaking.Scripts.Views;
using _Project.NetworkManagement.DenariaServer.Scripts.Signals;
using _Project.SettingsManager.Scripts.Controllers;
using _Project.SettingsManager.Scripts.Models;
using _Project.SettingsManager.Scripts.Signals;
using _Project.SettingsManager.Scripts.Views;
using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;
using _Project.InputManager.Scripts.Views;
using _Project.Animation.Scripts.Views;
using _Project.Login.Scripts.Signals;
using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.Signals;
using _Project.Shooting.Scripts.Views;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Context
{
    public class TownSquareSceneManagerContext : SignalContext
    {
        public TownSquareSceneManagerContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            BindGameSceneManager();
            BindMainMenu();
            SettingsManagerBindings();
            BindMatchmaking();
            BindAnimation();
            
            mediationBinder.Bind<CrosshairView>().To<CrosshairMediator>(); //TODO: Move them to battle scene
        }


        private void BindGameSceneManager()
        {
            injectionBinder.Bind<IPlayerIdMapModel>().To<PlayerIdMapModel>().ToSingleton();

            mediationBinder.Bind<GameSceneView>().To<GameSceneMediator>();
            mediationBinder.Bind<OwnPlayerView>().To<OwnPlayerMediator>();
            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();

            commandBinder.Bind<DenariaServerReceiveSpawnSignal>().To<PlayerSpawnCommand>();
            commandBinder.Bind<DenariaServerReceivePositionUpdateSignal>().To<PlayerPositionUpdateCommand>();
            commandBinder.Bind<DenariaServerReceiveRotationUpdateSignal>().To<PlayerRotationUpdateCommand>();
            
            mediationBinder.Bind<PlayerGunSelectorView>().To<PlayerGunSelectorMediator>();//TODO: Move into TPS Context
            mediationBinder.Bind<PlayerAction>().To<PlayerActionMediator>();//TODO: Move into TPS Context

            commandBinder.Bind<ShootSignal>().To<FireWithRaycastCommand>();//TODO: Move into TPS Context
            commandBinder.Bind<StopShootingSignal>().To<StopShootingCommand>();//TODO: Move into TPS Context
            commandBinder.Bind<SpawnGunSignal>().To<SpawnGunCommand>();//TODO: Move into TPS Context
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

        private void BindMatchmaking()
        {

            injectionBinder.Bind<PlatformTriggerEnterSignal>().ToSingleton();
            injectionBinder.Bind<PlatformTriggerExitSignal>().ToSingleton();
            // injectionBinder.Bind<StartMatchmakingSignal>().ToSingleton().CrossContext();
            // injectionBinder.Bind<CancelMatchmakingSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SearchButtonClickedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<CancelButtonClickedSignal>().ToSingleton().CrossContext();

            injectionBinder.Bind<QueueStartedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<MatchFoundSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<QueueFinishedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<ReadyButtonClickedSignal>().ToSingleton();
            injectionBinder.Bind<PlayerReadyMessageReceivedSignal>().ToSingleton().CrossContext();

            mediationBinder.Bind<MatchmakingMenuView>().To<MatchmakingMenuMediator>();
            mediationBinder.Bind<PlatformTriggerView>().To<PlatformTriggerMediator>();
            mediationBinder.Bind<ReadyCheckView>().To<ReadyCheckMediator>();

            injectionBinder.Bind<IMatchmakingService>().To<MatchmakingService>().ToSingleton().CrossContext();

            commandBinder.Bind<MatchFoundSignal>().To<MatchFoundCommand>();
            commandBinder.Bind<SearchButtonClickedSignal>().To<StartMatchmakingCommand>();
            commandBinder.Bind<CancelButtonClickedSignal>().To<CancelMatchmakingCommand>();
            commandBinder.Bind<ReadyButtonClickedSignal>().To<SendReadyCommand>();

            commandBinder.Bind<MatchStartReceivedSignal>().To<ConnectMatchCommand>();
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