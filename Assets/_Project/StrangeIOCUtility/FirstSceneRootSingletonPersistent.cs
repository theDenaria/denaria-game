using _Project.Utilities;
using strange.extensions.context.impl;

namespace _Project.StrangeIOCUtility
{
    public class FirstSceneRootSingletonPersistent : MonoBehaviourSingletonPersistent<FirstSceneRootSingletonPersistent>
    {
        private static SignalMVCSContext context;

        public SignalMVCSContext GetContext(ContextView contextView)
        {
            if (context != null)
            {
                return context;
            }
            context = new SignalMVCSContext(contextView);
            return context;
        }
        
        //public SignalMVCSContext context = new SignalMVCSContext();//TODO: Maybe reference it inside DontdestroyOnLoad

        
    }
}