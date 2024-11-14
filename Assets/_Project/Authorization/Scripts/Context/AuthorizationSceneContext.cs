using _Project.Authorization.Scripts.Commands;
using _Project.Authorization.Scripts.Services;
using _Project.Authorization.Scripts.Signals;
using _Project.Authorization.Scripts.Views;
using _Project.Language.Scripts.Commands;
using _Project.Language.Scripts.Signals;
using _Project.Language.Scripts.Views;
using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;

namespace _Project.Authorization.Scripts.Context
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
            BindLanguageTranslationInjections();
        }


        private void BindAuthorizationScene()
        {
            injectionBinder.Bind<ILoginService>().To<LoginService>().ToSingleton();
            injectionBinder.Bind<UserLoginCompletedSignal>().ToSingleton();
            mediationBinder.Bind<LoginFormView>().To<LoginFormMediator>();
            commandBinder.Bind<LoginWithMailAndPasswordSignal>().To<LoginWithMailAndPasswordCommand>();
            commandBinder.Bind<LoginWithDeviceIdSignal>().To<LoginWithDeviceIdCommand>();
            commandBinder.Bind<LoginWithCustomIdSignal>().To<LoginWithCustomIdCommand>();

            commandBinder.Bind<UserLoginCompletedSignal>().To<UserLoginCompletedCommand>();
            injectionBinder.Bind<IPasswordRecoveryService>().To<PasswordRecoveryService>().ToSingleton();
            mediationBinder.Bind<RecoveryFormView>().To<RecoveryFormMediator>();
            commandBinder.Bind<RequestPasswordRecoverySignal>().To<RequestPasswordRecoveryCommand>();

            injectionBinder.Bind<IRegisterService>().To<RegisterService>().ToSingleton();
            injectionBinder.Bind<RegistrationCompletedSignal>().ToSingleton();
            mediationBinder.Bind<RegistrationFormView>().To<RegistrationFormMediator>();
            commandBinder.Bind<RegisterWithMailAndPasswordSignal>().To<RegisterWithMailAndPasswordCommand>();
        }
        
        private void BindLanguageTranslationInjections()
        {
            mediationBinder.Bind<LanguageView>().To<LanguageMediator>();
        }
    }
}