using strange.extensions.context.impl;
using UnityEngine;

namespace _Project.GameSceneManager.TownSquareSceneManager.Scripts.Context
{
    public class TownSquareManagerBootstrap : ContextView
    {

        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new TownSquareSceneManagerContext(this);
        }
    }
}