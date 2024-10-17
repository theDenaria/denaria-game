using _Project.Analytics.Core.Scripts.Commands;
using _Project.Analytics.Core.Scripts.Services;
using _Project.Analytics.Core.Scripts.Signals;
using _Project.Analytics.Core.Scripts.Views;
using _Project.Analytics.CustomEvents.Scripts.Commands;
using _Project.Analytics.UnityAnalytics.Scripts.Services;
using _Project.LoadingScreen.Scripts.Signals;
using _Project.LoggingAndDebugging;
using _Project.NetworkManagement.DenariaServer.Scripts.Commands;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;
using _Project.NetworkManagement.DenariaServer.Scripts.Signals;

using _Project.ShowLoading.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using _Project.WaitingCanvas.Scripts.Signals;
using _Project.WaitingCanvas.Scripts.Views;
using _Project.PlayerSessionInfo.Scripts.Models;
using _Project.InputManager.Scripts.Views;
using _Project.InputManager.Scripts.Signals;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using _Project.NetworkManagement.TPSServer.Scripts.Services;
using _Project.NetworkManagement.TPSServer.Scripts.Commands;
using _Project.Matchmaking.Scripts.Commands;

using UnityEngine;
using _Project.WaitingCanvas.Scripts.Commands;
using _Project.ShowLoading.Scripts.Views;

/*using _Project.ABTesting.Scripts.Commands;
using _Project.ABTesting.Scripts.Models;
using _Project.ABTesting.Scripts.Signals;
using _Project.AdultContent.Scripts.Commands;
using _Project.AdultContent.Scripts.Services;
using _Project.AdultContent.Scripts.Signals;
using _Project.AdultContent.Scripts.Views;
using _Project.Analytics.Commands;
using _Project.Analytics.Models;
using _Project.Analytics.Services;
using _Project.Analytics.Signals;
using _Project.ApplicationMemoryTracker.Controllers;
using _Project.Applovin.Scripts.Controllers;
using _Project.Applovin.Scripts.Models;
using _Project.Applovin.Scripts.Services;
using _Project.Applovin.Scripts.Views;
using _Project.CameraManagement;
using _Project.CBSUtility.Controllers;
using _Project.CBSUtility.Models;
using _Project.CBSUtility.Services;
using _Project.CBSUtility.Signals;
using _Project.CBSUtility.Views;
using _Project.GameLifecycle.Scripts.Commands;
using _Project.GameLifecycle.Scripts.Services;
using _Project.GameLifecycle.Scripts.Signals;
using _Project.GameLifecycle.Scripts.Views;
using _Project.GameProgression.Scripts.Controllers;
using _Project.GameProgression.Scripts.Models;
using _Project.GameProgression.Scripts.Services;
using _Project.GameSettings.Scripts.Controllers;
using _Project.GameSettings.Scripts.Models;
using _Project.GameSettings.Scripts.Services;
using _Project.GenrePickScreen.Scripts.Models;
using _Project.HostWelcomeScreen.Scripts.model;
using _Project.InGameTransaction.Scripts.Controllers;
using _Project.InGameTransaction.Scripts.Signals;
using _Project.ItsAMatch.Controllers;
using _Project.ItsAMatch.Signals;
using _Project.LoadingScreen.Scripts;
using _Project.NavigationBar.Scripts.Commands;
using _Project.NavigationBar.Scripts.Signals;
using _Project.NotEnoughCoinsCanvas.Scripts.Models;
using _Project.PlayerProfile.Scripts.Controllers;
using _Project.PlayerProfile.Scripts.Services;
using _Project.PushNotifications.Scripts.Controllers;
using _Project.PushNotifications.Scripts.Services;
using _Project.RetryConnection.Scripts.Controllers;
using _Project.RetryConnection.Scripts.Models;
using _Project.RetryConnection.Scripts.Signals;
using _Project.RetryConnection.Scripts.Views;
using _Project.ServerTimeStamp.Controllers;
using _Project.ServerTimeStamp.Models;
using _Project.ServerTimeStamp.Services;
using _Project.ServerTimeStamp.Signals;
using _Project.TimeGate.Models;
using _Project.ApplicationMemoryTracker.Scripts.Models;
using _Project.ApplicationMemoryTracker.Scripts.Services;
using _Project.ApplicationMemoryTracker.Scripts.Signals;
using _Project.ApplicationMemoryTracker.Scripts.Views;
using _Project.NotEnoughCoinsCanvas.Scripts.Signals;
using _Project.TopBar.Scripts.Views;
using _Project.CloudContentDelivery.Scripts.Models;
using _Project.CloudContentDelivery.Scripts.Services;
using _Project.CloudContentDelivery.Scripts.Signals;
using _Project.CloudContentDelivery.Scripts.Views;
using _Project.ContentManagementSystem.Scripts.Services;
using _Project.ContentManagementSystem.Scripts.Signals;
using _Project.DeviceStorageTracker.Scripts.Controllers;
using _Project.DeviceStorageTracker.Scripts.Models;
using _Project.DeviceStorageTracker.Scripts.Signals;
using _Project.DeviceStorageTracker.Scripts.Views;
using _Project.DeviceUtility.Scripts.Controllers;
using _Project.DeviceUtility.Scripts.Services;
using _Project.DeviceUtility.Scripts.Signals;
using _Project.InGameTransaction.Scripts.Models;
using _Project.InGameTransaction.Scripts.Models.Interfaces;
using _Project.InGameTransaction.Scripts.Services;
using _Project.InGameTransaction.Scripts.Views;
using _Project.LoadingScreen.Models;
using _Project.NotEnoughCoinsCanvas.Scripts.Commands;
using _Project.NotEnoughCoinsCanvas.Scripts.Views;
using _Project.FacebookSDKIntegration.Services;
using _Project.ForceUpdate.Scripts.Controllers;
using _Project.ForceUpdate.Scripts.Models;
using _Project.ForceUpdate.Scripts.Services;
using _Project.ForceUpdate.Scripts.Signals;
using _Project.ForceUpdate.Scripts.Views;
using _Project.GemsSpawner.Scripts.Views;
using _Project.WaitingCanvas.Scripts.Signals;
using _Project.WaitingCanvas.Scripts.Controllers;
using _Project.WaitingCanvas.Scripts.Views;
using _Project.TopBar.Scripts.Controllers;
using _Project.MainButtonsNotificationSystem.Scripts.Models;
using _Project.MainButtonsNotificationSystem.Scripts.Services;
using _Project.MainButtonsNotificationSystem.Scripts.Controllers;
using _Project.InAppReview.Scripts.Controllers;
using _Project.InAppReview.Views;
using _Project.PushNotifications.Scripts.Models;
using _Project.PushNotifications.Scripts.Views;
using _Project.LiveOps.Common.Scripts.Models.Interfaces;
using _Project.LiveOps.Common.Scripts.Models;
using _Project.LiveOps.Common.Scripts.Services;
using _Project.LiveOps.Common.Scripts.Signals;
using _Project.LiveOps.Common.Scripts.Controllers;
using _Project.Login.Models;
using _Project.Login.Services;
using _Project.Login.Signals;
using _Project.Utilities;
using _Project.InGameTransaction.Scripts.Models.ActiveTransactionsModels;
using _Project.PaidAnswer.Scripts.Models;
using _Project.ShowLoading.Mediators;
using _Project.ShowLoading.Signals;*/

namespace _Project.StrangeIOCUtility.Scripts.Context
{
	public class MainContext : SignalContext
	{
		public MainContext(MonoBehaviour view) : base(view)
		{

		}

		protected override void mapBindings()
		{
			if (strange.extensions.context.impl.Context.firstContext == this)
			{
				/*
				BindForceUpdatePopup();
				BindFacebookSDKInjections();
				BindSceneManagementInjections();
				 
				BindCBSGeneralElements();
				BindABTestingInjections();
				BindCurrencyInjections();
				
				BindRetryConnectionInjections();
				BindDeviceUtilityInjections();
				BindDeviceStorageTrackerInjections();
				BindApplicationMemoryTrackingInjections();
				
				BindApplicationLifecycleInjections();*/
				BindLogReportInjections();
				BindLoadingInjections();
				ShowLoadingBindings();
				BindAnalyticsInjections();
				BindRoutineRunnerInjections();
				BindWaitingCanvasInjections();
				//BindLoginInjections(); //TODO: Uncomment 21 August
				TPSServerBindings();
				BindPlayerSessionInfoInjections();
				InputManagerBindings();
				DenariaServerBindings();
			}
		}

		private void BindRoutineRunnerInjections()
		{
			injectionBinder.Bind<IRoutineRunner>().To<RoutineRunner>().CrossContext().ToSingleton();
		}

		/*
		private void BindForceUpdatePopup()
		{
			injectionBinder.Bind<OpenForceUpdatePopupSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<IMinimumClientVersionNeededModel>().To<MinimumClientVersionNeededModel>().ToSingleton().CrossContext();
			injectionBinder.Bind<IApplicationVersionModel>().To<ApplicationVersionModel>().ToSingleton().CrossContext();
			injectionBinder.Bind<IMinimumClientVersionNeededService>().To<MinimumClientVersionNeededService>()
				.ToSingleton().CrossContext();

			mediationBinder.Bind<ForceUpdatePopupView>().To<ForceUpdatePopupMediator>();
			
			commandBinder.Bind<CheckForceUpdateNeededSignal>().To<CheckForceUpdateNeededCommand>();

#if UNITY_ANDROID
			commandBinder.Bind<OpenStorePageButtonClickSignal>().To<OpenPlayStorePageCommand>().To<SendForceUpdatePopupEventCommand>();
#elif UNITY_IOS
			commandBinder.Bind<OpenStorePageButtonClickSignal>().To<OpenAppleStorePageCommand>().To<SendForceUpdatePopupEventCommand>();
#else 
			commandBinder.Bind<OpenStorePageButtonClickSignal>().To<OpenPlayStorePageCommand>().To<SendForceUpdatePopupEventCommand>();
#endif
		}

      
        
		private void BindLoginInjections()
		{
			injectionBinder.Bind<ILoginService>().To<LoginService>().ToSingleton().CrossContext();
			injectionBinder.Bind<ILoginSucceedDataProgress>().To<LoginSucceedDataProgress>().ToSingleton().CrossContext();
		}
        
		private void BindGeneralMediators()
		{
			mediationBinder.Bind<CameraManagerView>().To<CameraManagerMediator>();
		}
		*/
		private void BindAnalyticsInjections()
		{
			injectionBinder.Bind<IAnalyticsService>().To<UnityAnalyticsService>().ToSingleton().CrossContext();
			injectionBinder.Bind<SendAnalyticsEventSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<SendAnalyticsEventSignal>().To<SendAnalyticsEventCommand>();

			injectionBinder.Bind<SendCachedAnalyticEventsSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<SendCachedAnalyticEventsSignal>()
				.To<SendCachedScreenTrackingEventCommand>()
				.To<SendCachedNotificationPopupQuitEventCommand>()
				.To<SendCachedNotEnoughCoinsEventCommand>()
				.To<SendCachedAnalyticEventsCompletedCommand>()
				.To<SendCachedForceUpdatePopupEventCommand>()
				.InSequence();

			mediationBinder.Bind<AnalyticsTesterView>().To<AnalyticsTesterMediator>();
		}

		/*
		private void BindFacebookSDKInjections()
		{
			injectionBinder.Bind<IFacebookSDKService>().To<FacebookSDKService>().ToSingleton().CrossContext();
		}

		private void BindABTestingInjections()
		{
			injectionBinder.Bind<ITestGroupIdModel>().To<TestGroupIdModel>().ToSingleton().CrossContext();
			injectionBinder.Bind<IPlayfabIdModel>().To<PlayfabIdModel>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayfabIdReceivedSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TestGroupIdSetSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<SetPlayfabIdTextSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<CopyUserIdToClipboardSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<SetTestGroupIdAtStartSignal>().To<SetTestGroupIdAtStartCommand>();
			commandBinder.Bind<TestGroupIdSetSignal>().To<SetTestGroupIdToUserPropertiesCommand>();
			commandBinder.Bind<PlayfabIdReceivedSignal>().To<PlayfabIdReceivedCommand>();
			commandBinder.Bind<SetPlayfabIdTextSignal>().To<SetPlayfabIdTextCommand>();
			commandBinder.Bind<CopyUserIdToClipboardSignal>().To<CopyUserIdToClipboardCommand>();
		}
		
		private void BindCBSGeneralElements()
		{
			injectionBinder.Bind<CBSUtilityMediator>().ToSingleton().CrossContext();
			injectionBinder.Bind<IMiscellaneousConfigService>().To<MiscellaneousConfigService>().ToSingleton().CrossContext();
			
			injectionBinder.Bind<UpdateProfileDataSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<UpdateProfileDataSignal>().To<UpdateProfileDataCommand>();
			
			injectionBinder.Bind<GetProfileDataSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<GetProfileDataSignal>().To<GetProfileDataCommand>();
			
			injectionBinder.Bind<WaitGetProfileDataSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<WaitGetProfileDataSignal>().To<WaitGetProfileDataCommand>();
			
			injectionBinder.Bind<WaitUpdateProfileDataSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<WaitUpdateProfileDataSignal>().To<WaitUpdateProfileDataCommand>();

			injectionBinder.Bind<IProfileService>().To<ProfileService>().ToSingleton().CrossContext();
			injectionBinder.Bind<ICustomProfileData>().To<CustomProfileData>().ToSingleton().CrossContext();

			injectionBinder.Bind<GetProfileDataThenUpdateProfileDataSignal>().ToSingleton().CrossContext();
			//commandBinder.Bind<GetProfileDataThenUpdateProfileDataSignal>().To<WaitGetProfileDataCommand>().To<UpdateProfileDataCommand>().InSequence();
			commandBinder.Bind<GetProfileDataThenUpdateProfileDataSignal>().To<GetProfileDataCommand>().To<UpdateProfileDataCommand>();

			injectionBinder.Bind<StartConfigDataCommandChainSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<StartSetPlayerDataCommandChainSignal>().ToSingleton().CrossContext();
		}

		private void BindCurrencyInjections()
		{
			injectionBinder.Bind<CurrencyUpdatePreviewSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<CurrencyUpdatedSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<SetCurrencyAmountToIndicatorSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<BuyWithCurrencySignal>().ToSingleton().CrossContext();

			injectionBinder.Bind<ICurrencyModel>().To<CurrencyModel>().ToSingleton().CrossContext();
			injectionBinder.Bind<ICurrencyService>().To<CurrencyService>().ToSingleton().CrossContext();

			mediationBinder.Bind<CurrencyIndicatorView>().To<CurrencyIndicatorMediator>();

			commandBinder.Bind<SetCurrencyAmountToIndicatorSignal>().To<SetCurrencyIconDataIndicatorCommand>();
			commandBinder.Bind<BuyWithCurrencySignal>().To<BuyWithCurrencyCommand>();

			injectionBinder.Bind<GemButtonClickedSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<GemButtonClickedSignal>().To<GemButtonClickedCommand>();
		}

		private void BindRetryConnectionInjections()
		{
			mediationBinder.Bind<RetryConnectionButtonView>().To<RetryConnectionButtonMediator>();
			mediationBinder.Bind<RetryConnectionCanvasView>().To<RetryConnectionCanvasMediator>();
			mediationBinder.Bind<RetryConnectionButtonNetworkLossView>().To<RetryConnectionButtonNetworkLossMediator>();
			
			injectionBinder.Bind<IRetryConnectionChannels>().To<RetryConnectionChannels>().ToSingleton().CrossContext();
			injectionBinder.Bind<ToggleRetryConnectionCanvasSignal>().ToSingleton().CrossContext();
			
			injectionBinder.Bind<RetryConnectionSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<RetryConnectionSignal>().To<RetryConnectionCommand>();
		}

		private void BindDeviceUtilityInjections()
		{
            injectionBinder.Bind<IDeviceUtilityService>().To<DeviceUtilityService>().ToSingleton().CrossContext();

            commandBinder.Bind<ForceQuitApplicationSignal>().To<ForceQuitApplicationCommand>();
        }

		private void BindDeviceStorageTrackerInjections()
		{
			injectionBinder.Bind<IDeviceStorageStatusModel>().To<DeviceStorageStatusModel>().ToSingleton().CrossContext();

			injectionBinder.Bind<DeviceStorageSpaceStatusChangedSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<CheckDeviceStorageStatusSignal>().To<CheckDeviceStorageSpaceCommand>();
			commandBinder.Bind<CleanStorageUpSignal>().To<CleanStorageUpCommand>();

			mediationBinder.Bind<DeviceStorageTrackerView>().To<DeviceStorageTrackerMediator>();
			mediationBinder.Bind<InsufficientStorageSpaceCanvasView>().To<InsufficientStorageSpaceCanvasMediator>();
		}

		private void BindApplicationMemoryTrackingInjections()
		{
			injectionBinder.Bind<IMemoryProfilerRecorders>().To<MemoryProfilerRecorders>().ToSingleton().CrossContext();

			injectionBinder.Bind<IApplicationMemoryService>().To<ApplicationMemoryService>().ToSingleton().CrossContext();

			injectionBinder.Bind<ApplicationMemoryCleanedSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<ApplicationMemoryCriticalSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<RequestApplicationMemoryCheckSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<RequestApplicationMemoryCheckSignal>().To<RequestApplicationMemoryCheckCommand>();

			mediationBinder.Bind<ApplicationMemoryTrackerView>().To<ApplicationMemoryTrackerMediator>();
			mediationBinder.Bind<ApplicationMemoryCriticalCanvasView>().To<ApplicationMemoryCriticalCanvasMediator>();
		}

        private void BindApplicationLifecycleInjections()
		{
			injectionBinder.Bind<ApplicationStartSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<ApplicationFocusChangedSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<ApplicationPauseSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<ApplicationQuitSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<NetworkConnectionLostSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<NetworkConnectionSuccessSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<RetryNetworkConnectionSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<StartCheckingNetworkConnectionSignal>().ToSingleton().CrossContext();

			injectionBinder.Bind<IApplicationLifecycleService>().To<ApplicationLifecycleService>();

			mediationBinder.Bind<LifecycleManagerView>().To<LifecycleManagerMediator>();
			mediationBinder.Bind<NetworkConnectionCheckerView>().To<NetworkConnectionCheckerMediator>();

			commandBinder.Bind<ApplicationStartSignal>().To<ApplicationStartCommand>()
				.To<ArrangePushNotificationsOnStartCommand>();
			
			commandBinder.Bind<ApplicationFocusChangedSignal>().To<ApplicationFocusChangedCommand>()
				.To<ArrangePushNotificationsOnFocusCommand>()
				.To<TimeStampControlOnFocusCommand>()
				.To<CurrentEpisodeControlOnFocusCommand>()
				.To<CurrentSceneControlOnFocusCommand>();
			
			commandBinder.Bind<ApplicationPauseSignal>().To<ApplicationPauseCommand>()
				.To<ArrangePushNotificationsOnPauseCommand>()
				.To<ArrangeCacheEventsOnPauseCommand>()
				.To<CacheEpisodeLeftEventCommand>()
				.To<CacheNotificationPopupQuitEventCommand>()
				.To<CacheOutOfProfileTrackingEventCommand>()
				.To<CacheScreenTrackingEventCommand>()
				.To<CacheNotEnoughCoinsEventCommand>()
				.To<CacheForceUpdatePopupCommand>()
				.InSequence();
			
			commandBinder.Bind<ApplicationQuitSignal>().To<ApplicationQuitCommand>()
				.To<ArrangePushNotificationsOnQuitCommand>()
				.To<ArrangeCacheEventsOnQuitCommand>()
				.To<CacheEpisodeLeftEventCommand>()
				.To<CacheNotificationPopupQuitEventCommand>()
				.To<CacheOutOfProfileTrackingEventCommand>() 
				.To<CacheScreenTrackingEventCommand>()
				.To<CacheNotEnoughCoinsEventCommand>()
				.To<CacheForceUpdatePopupCommand>()
				.InSequence();
			
			commandBinder.Bind<RetryNetworkConnectionSignal>().To<RetryNetworkConnectionCommand>();
		}*/
		private void BindWaitingCanvasInjections()
		{
			injectionBinder.Bind<AddWaitHandlerSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<ShowWaitingCanvasSignal>().To<ShowWaitingCanvasCommand>();

			mediationBinder.Bind<WaitingCanvasView>().To<WaitingCanvasMediator>();
		}

		private void ShowLoadingBindings()
		{
			mediationBinder.Bind<LoadingView>().To<LoadingMediator>();
			injectionBinder.Bind<ShowLoadingAnimationSignal>().ToSingleton().CrossContext(); ;
			injectionBinder.Bind<HideLoadingAnimationSignal>().ToSingleton().CrossContext(); ;
		}

		private void BindLoadingInjections()
		{
			injectionBinder.Bind<CompleteLoadingSignal>().ToSingleton().CrossContext();
		}

		private void BindLogReportInjections()
		{
			injectionBinder.Bind<IFileOperationService>().To<FileOperationService>().ToSingleton().CrossContext();
			injectionBinder.Bind<INativeShareService>().To<NativeShareService>().ToSingleton().CrossContext();
			injectionBinder.Bind<ILogRecordService>().To<LogRecordService>().ToSingleton().CrossContext();
		}

		private void BindPlayerSessionInfoInjections()
		{
			injectionBinder.Bind<IPlayerSessionInfoModel>().To<PlayerSessionInfoModel>().ToSingleton().CrossContext();
		}

		private void InputManagerBindings()
		{

			injectionBinder.Bind<PlayerMoveInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayerLookInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayerJumpInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayerFireInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayerSprintInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<PlayerEscMenuInputSignal>().ToSingleton().CrossContext();

			injectionBinder.Bind<EnablePlayerInputSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DisablePlayerInputSignal>().ToSingleton().CrossContext();


			mediationBinder.Bind<InputHandlerView>().To<InputHandlerMediator>();
		}

		private void DenariaServerBindings()
		{
			injectionBinder.Bind<IDenariaServerService>().To<DenariaServerService>().ToSingleton().CrossContext();

			injectionBinder.Bind<DenariaServerConnectSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<DenariaServerConnectSignal>().To<DenariaServerConnectCommand>();

			injectionBinder.Bind<TownSquareLoadedSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<TownSquareLoadedSignal>().To<DenariaServerSendConnectCommand>();

			injectionBinder.Bind<DenariaServerOwnPlayerSpawnedSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<DenariaServerOwnPlayerSpawnedSignal>().To<DenariaServerOwnPlayerSpawnedCommand>();

			injectionBinder.Bind<DenariaServerReceiveSpawnSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DenariaServerReceivePositionUpdateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DenariaServerReceiveRotationUpdateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DenariaServerReceiveDisconnectSignal>().ToSingleton().CrossContext();

			injectionBinder.Bind<DenariaServerSendMoveSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DenariaServerSendLookSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<DenariaServerSendJumpSignal>().ToSingleton().CrossContext();

			// Will be dispatched from GameSceneManager
			commandBinder.Bind<DenariaServerSendMoveSignal>().To<DenariaServerSendMoveCommand>();
			commandBinder.Bind<DenariaServerSendLookSignal>().To<DenariaServerSendLookCommand>();
			commandBinder.Bind<DenariaServerSendJumpSignal>().To<DenariaServerSendJumpCommand>();

		}

		private void TPSServerBindings()
		{
			injectionBinder.Bind<ITPSServerService>().To<TPSServerService>().ToSingleton().CrossContext();

			injectionBinder.Bind<TPSServerConnectSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<TPSServerConnectSignal>().To<TPSServerConnectCommand>();

			injectionBinder.Bind<TPSServerConnectSuccessSignal>().ToSingleton();

			commandBinder.Bind<TPSServerConnectSuccessSignal>().To<MatchmakingDisconnectCommand>().To<DenariaServerDisonnectCommand>().To<LoadTPSGameSceneCommand>().InSequence();

			injectionBinder.Bind<TPSLoadedSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<TPSLoadedSignal>().To<TPSServerSendConnectCommand>();

			injectionBinder.Bind<TPSServerOwnPlayerSpawnedSignal>().ToSingleton().CrossContext();
			commandBinder.Bind<TPSServerOwnPlayerSpawnedSignal>().To<TPSServerOwnPlayerSpawnedCommand>();


			injectionBinder.Bind<TPSServerReceiveSpawnSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceivePositionUpdateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceiveRotationUpdateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceiveFireSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceiveHitSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceiveHealthUpdateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerReceiveDisconnectSignal>().ToSingleton().CrossContext();

			injectionBinder.Bind<TPSServerSendMoveSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerSendLookSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerSendFireSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<TPSServerSendJumpSignal>().ToSingleton().CrossContext();

			commandBinder.Bind<TPSServerSendMoveSignal>().To<TPSServerSendMoveCommand>();
			commandBinder.Bind<TPSServerSendLookSignal>().To<TPSServerSendLookCommand>();
			commandBinder.Bind<TPSServerSendFireSignal>().To<TPSServerSendFireCommand>();
			commandBinder.Bind<TPSServerSendJumpSignal>().To<TPSServerSendJumpCommand>();
		}


	}
}
