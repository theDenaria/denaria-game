using strange.extensions.context.impl;

namespace _Project.DemoSignalWorkPrinciple.Contexts
{
    public class DemoSignalWorkPrincipleBootstrap : ContextView
    {
        private void Awake()
        {
            context = new DemoSignalWorkPrincipleContext(this);
        }
    }
}