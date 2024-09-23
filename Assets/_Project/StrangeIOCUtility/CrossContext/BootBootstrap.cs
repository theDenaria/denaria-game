using strange.extensions.context.impl;

namespace _Project.StrangeIOCUtility.CrossContext
{
    public class BootBootstrap : ContextView
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            context = new BootContext(this);
        }
    }
}