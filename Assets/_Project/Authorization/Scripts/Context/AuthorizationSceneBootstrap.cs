using strange.extensions.context.impl;

namespace _Project.Login.Scripts.Context
{
    public class AuthorizationSceneBootstrap : ContextView
    {
        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new AuthorizationSceneContext(this);
        }
    }
}