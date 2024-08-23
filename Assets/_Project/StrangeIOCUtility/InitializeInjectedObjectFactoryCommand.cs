using strange.extensions.command.impl;
using strange.extensions.injector.api;

namespace _Project.StrangeIOCUtility
{
    public class InitializeInjectedObjectFactoryCommand : Command
    {
        [Inject] public IInjectionBinder InjectionBinder { get; set; }
        public override void Execute()
        {
            InjectedObjectFactory.InjectionBinder = InjectionBinder;
        }
    }
}