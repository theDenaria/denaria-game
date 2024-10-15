using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;

namespace _Project.TPSGameLoadingScreen.Scripts.Context
{
    public class TPSGameLoadingSceneContext : SignalContext
    {
        public TPSGameLoadingSceneContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            
            BindTPSGameLoadingSceneInjections();
        }
        
        protected override void postBindings()
        {
            base.postBindings();
        }
        
        private void BindTPSGameLoadingSceneInjections()
        {
            
        }

    }
}