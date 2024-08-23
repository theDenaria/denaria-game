using _Project.LoadingScreen.Scripts;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Controllers;
using UnityEngine;
/*using _Project.Analytics.Commands;
using _Project.Analytics.Signals;
using _Project.CBSUtility.Controllers;
using _Project.CBSUtility.Views;
using _Project.CloudContentDelivery.Scripts.Controllers;
using _Project.ContentManagementSystem.Scripts.Controllers;
using _Project.ContentManagementSystem.Scripts.Signals;
using _Project.DeviceFrameRateLimiter.Controllers;
using _Project.ForceUpdate.Scripts.Controllers;
using _Project.InGameTransaction.Scripts.Controllers;
using _Project.LiveOps.Common.Scripts.Controllers;
using _Project.LoadingScreen.Scripts;
using _Project.Login.Controllers;
using _Project.Login.Signals;
using _Project.ServerTimeStamp.Controllers;
using _Project.UnityGamingServices.Scripts.Controllers;
*/

namespace _Project.StrangeIOCUtility.CrossContext
{
    public class BootContext : SignalContext
    {
        public BootContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void postBindings()
        {
            base.postBindings();
            
            StartGameSignal startGameSignal = (StartGameSignal)injectionBinder.GetInstance<StartGameSignal>();
            startGameSignal.Dispatch();
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            
            commandBinder.Bind<StartGameSignal>()
                .To<SetSceneGroupsCommand>()
                .To<StartGameCommand>() // Starts loading screen
                .Once()
                .InSequence();

            injectionBinder.Bind<LoadingScreenStartSignal>().ToSingleton().CrossContext();
            commandBinder.Bind<LoadingScreenStartSignal>() // Required to proceed after loading screen starts
                //.To<LimitDeviceFrameRateCommand>()//TODO: Uncomment after adding classes - 14 August 2024
                .To<InitializeSRDebuggerCommand>()
                .To<InitializeInjectedObjectFactoryCommand>()
                .InSequence().Once();
            
            //TODO: Uncomment after adding Classes. -14 August 2024
            /*
            injectionBinder.Bind<PreLoginInitializerSignal>().ToSingleton().CrossContext();
            commandBinder.Bind<PreLoginInitializerSignal>()
                //.To<AddressableBootCommandChainCommand>() // Fires AddressableBootCommandChainSignal if successful//TODO: Uncomment after adding classes - 14 August 2024
                //.To<LoginCommand>() // Last purpose of this command chain is to call login.
                .InParallel().Once();
            
            injectionBinder.Bind<LoginSucceedSignal>().ToSingleton().CrossContext();
            commandBinder.Bind<LoginSucceedSignal>() 
                .To<CheckForceUpdateNeededCommand>()
                .To<LoginSucceedParallelCommand>()
                .InSequence();
            
            commandBinder.Bind<LoginSucceedParallelSignal>()
                .To<SetPlayerDataToModelsCommand>() // This directs to loading scene and its completion
                .InParallel();*/
        }
    }
}