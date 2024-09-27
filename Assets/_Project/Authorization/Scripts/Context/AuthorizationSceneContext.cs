using _Project.LoadingScreen.Scripts;
using _Project.Login.Controllers;
using _Project.Login.Scripts.Signals;
using _Project.Login.Scripts.Views;
using _Project.Login.Services;
using _Project.StrangeIOCUtility.CrossContext;
using strange.extensions.context.api;
using UnityEngine;

namespace _Project.Login.Scripts.Context
{
    public class AuthorizationSceneContext : SignalContext
    {
        public AuthorizationSceneContext(MonoBehaviour view) : base(view)
        {
        }
        
        protected override void mapBindings()
        {
            base.mapBindings();
			
            BindAuthorizationScene();
        }
		

        private void BindAuthorizationScene()
        {
            injectionBinder.Bind<ILoginService>().To<LoginService>().ToSingleton();
            injectionBinder.Bind<UserLoginCompletedSignal>().ToSingleton();
            
            mediationBinder.Bind<LoginFormView>().To<LoginFormMediator>();
            
            
            commandBinder.Bind<LoginWithMailAndPasswordSignal>().To<LoginWithMailAndPasswordCommand>();
            commandBinder.Bind<LoginWithDeviceIdSignal>().To<LoginWithDeviceIdCommand>();
            commandBinder.Bind<LoginWithCustomIdSignal>().To<LoginWithCustomIdCommand>();
            
            
            
            injectionBinder.Bind<IPasswordRecoveryService>().To<PasswordRecoveryService>().ToSingleton();
            
            mediationBinder.Bind<RecoveryFormView>().To<RecoveryFormMediator>();

            commandBinder.Bind<RequestPasswordRecoverySignal>().To<RequestPasswordRecoveryCommand>();
            
            
            
            injectionBinder.Bind<IRegisterService>().To<RegisterService>().ToSingleton();

        }
    }
}