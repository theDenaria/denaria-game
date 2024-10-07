using _Project.GameSceneManager.Scripts.Models;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Context
{
    public class GameSceneManagerBootstrap : ContextView
    {

        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
        {
            context = new GameSceneManagerContext(this);
        }
    }
}