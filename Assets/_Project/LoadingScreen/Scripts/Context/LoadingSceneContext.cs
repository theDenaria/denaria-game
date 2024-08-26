/*using _Project.ABTesting.Scripts.Commands;
using _Project.Applovin.Scripts.Controllers;
using _Project.CBSUtility.Controllers;
using _Project.CloudContentDelivery.Scripts.Controllers;
using _Project.ContentManagementSystem.Scripts.Controllers;
using _Project.ContentManagementSystem.Scripts.Signals;
using _Project.ContentManagementSystem.Scripts.Views;
using _Project.GameProgression.Scripts.Controllers;
using _Project.GameSettings.Scripts.Controllers;
using _Project.InGameTransaction.Scripts.Controllers;
using _Project.LiveOps.Common.Scripts.Controllers;
using _Project.LoadingScreen.Scripts.Controllers;
using _Project.Login.Controllers;
using _Project.Login.Services;
using _Project.Login.Signals;
using _Project.PlayerProfile.Scripts.Controllers;
using _Project.PlayerProfile.Scripts.Views;*/

using _Project.LoadingScreen.Scripts.Controllers;
using _Project.StrangeIOCUtility.CrossContext;
using strange.extensions.context.api;
using UnityEngine;

namespace _Project.LoadingScreen.Scripts.Context
{
    public class LoadingSceneContext : SignalContext
	{
		public LoadingSceneContext(MonoBehaviour view) : base(view)
		{

		}

		override public IContext Start()
		{
			base.Start();
			
			LoadingScreenShownSignal loadingScreenShownSignal = injectionBinder.GetInstance<LoadingScreenShownSignal>();
			loadingScreenShownSignal.Dispatch();

			return this;
		}

		
		protected override void mapBindings()
		{
			base.mapBindings();
			
			BindLoadingScene();
			//BindTermsOfServiceWindowPopUpElements();//TODO: Uncomment 21 August
			//BindLoginInjections();
            //BindContentPreparationInjections();
        }
		

		private void BindLoadingScene()
		{
			mediationBinder.Bind<LoadingBarView>().To<LoadingBarMediator>();
			mediationBinder.Bind<DynamicIntroductionTextView>().To<DynamicIntroductionTextMediator>();
			//mediationBinder.Bind<UserIdCopyToClipboardButtonView>().To<UserIdCopyToClipboardButtonMediator>();

			commandBinder.Bind<LoadingBarCompletedSignal>().To<LoadingBarCompletedCommand>();
			commandBinder.Bind<StartLoadingSignal>().To<StartLoadingCommand>().Once();
			commandBinder.Bind<SwitchSceneAccordingToProgressSignal>().To<SwitchSceneAccordingToProgressCommand>();
			
			
			//To<LoadingBarIsReadyToCompleteCommand>(); tetikle
		}
		
//TODO: Uncomment 21 August
		/*
		private void BindTermsOfServiceWindowPopUpElements()
		{
			mediationBinder.Bind<TermsOfServiceButtonView>().To<TermsOfServiceButtonMediator>()
				.To<TermsOfServiceMediator>()
				.To<PrivacyPolicyMediator>();

			commandBinder.Bind<AcceptTermsOfServiceSignal>().To<AcceptTermsOfServiceCommand>();
			commandBinder.Bind<OpenTermsOfServiceScreenSignal>().To<OpenTermsOfServiceScreenCommand>();
			commandBinder.Bind<OpenPrivacyPolicyScreenSignal>().To<OpenPrivacyPolicyScreenCommand>();
		}
		
		private void BindLoginInjections()
		{
			commandBinder.Bind<StartConfigDataCommandChainSignal>()
				.To<SetRewardedAdsConfigDataCommand>()
				.To<SetMiscellaneousConfigTitleDataCommand>()
				.To<SetOutOfProfileConfigCommand>()
				.To<InitializeStoreModelCollectionModelCommand>()
				.To<ConfigDataFetchCompletedCommand>() // This must to be last command of this chain
				.InParallel();
			
			commandBinder.Bind<StartAddressableFetchCommandChainSignal>()
				.To<InitializeCloudContentDeliveryCommand>()
				.To<InitializeAddressablesContentManagementServiceCommand>()
				.To<InitializeCharacterMetaServiceCommand>()
                .To<InitializeInitialCharacterOrderArrayCollectionModelCommand>()
                .To<InitializePremiumOptionsConfigModelCommand>()	//TODO: This should be moved to another command chain later as long as its job is done before SetFreeOptionsPremiumCommand starts.
                .To<AddressableFetchChainCompletedCommand>() // This must to be last command of this chain
				.InSequence();
			
			commandBinder.Bind<StartSetPlayerDataCommandChainSignal>() // Model setting related Sync operations are here
				.To<InitializeStoreItemModelCollectionModelCommand>()
				.To<InitializeTimeLimitedStoreSpecialOffersTrackingCommand>()
				.To<InitializeLiveOpsExpirationTrackingCommand>()
				.To<SetFreeGemsRewardedAdProgressFromPrefsCommand>()
				.To<SetTestGroupIdAtStartCommand>()
				.To<SetGameProgressAtStartCommand>()
				.To<SetGameSettingsAtLoadCommand>()
				.To<SetPlayerProfileSavesCommand>()
				.To<EpisodeVersionFetcherCommand>()
				.To<SetTraceOutOfProfileCommand>()
				.To<LoadingBarIsReadyToCompleteCommand>()
				.InSequence();
		}

		private void BindContentPreparationInjections()
		{
			mediationBinder.Bind<DownloadContentDependenciesProgressBarView>().To<DownloadContentDependenciesProgressBarMediator>();
            commandBinder.Bind<ContentManagementServiceInitializedSignal>()
                .To<LoadPlaceholderCollectableDateCapsuleBackgroundSpriteCommand>()
                .InParallel();
        }*/

	}
}