using _Project.DemoSignalWorkPrinciple.Controllers;
using _Project.DemoSignalWorkPrinciple.Signals;
using _Project.DemoSignalWorkPrinciple.Views;
using _Project.StrangeIOCUtility.CrossContext;
using UnityEngine;

namespace _Project.DemoSignalWorkPrinciple.Contexts
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