using strange.extensions.context.impl;

namespace _Project.TPSGameLoadingScreen.Scripts.Context
{
    public class TPSGameLoadingSceneBootstrap : ContextView
    {
        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new TPSGameLoadingSceneContext(this);
        }
    }
}