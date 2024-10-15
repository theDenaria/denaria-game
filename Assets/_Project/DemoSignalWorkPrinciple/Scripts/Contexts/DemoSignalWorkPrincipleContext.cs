using _Project.DemoSignalWorkPrinciple.Scripts.Commands;
using _Project.DemoSignalWorkPrinciple.Scripts.Signals;
using _Project.DemoSignalWorkPrinciple.Scripts.Views;
using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;

namespace _Project.DemoSignalWorkPrinciple.Scripts.Contexts
{
    public class DemoSignalWorkPrincipleContext : SignalContext
    {
        public DemoSignalWorkPrincipleContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            mediationBinder.Bind<ButtonView>().To<ButtonMediator>();

            /*
            commandBinder.Bind<ASignal>().To<ACommand>();
            commandBinder.Bind<BSignal>().To<BCommand>();
            commandBinder.Bind<CSignal>().To<CCommand>();
            */
            commandBinder.Bind<ASignal>().To<ACommand>().To<BCommand>().To<CCommand>();
        }
    }
}