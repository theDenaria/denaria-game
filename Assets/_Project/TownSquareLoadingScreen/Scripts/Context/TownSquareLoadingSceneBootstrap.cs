using strange.extensions.context.impl;

namespace _Project.TownSquareLoadingScreen.Scripts.Context
{
    public class TownSquareLoadingSceneBootstrap : ContextView
    {
        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new TownSquareLoadingSceneContext(this);
        }
    }
}