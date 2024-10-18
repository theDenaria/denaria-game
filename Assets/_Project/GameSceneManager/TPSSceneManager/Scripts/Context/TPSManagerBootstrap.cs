using strange.extensions.context.impl;
using UnityEngine;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Context
{
    public class TPSManagerBootstrap : ContextView
    {

        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new TPSSceneManagerContext(this);
        }
    }
}