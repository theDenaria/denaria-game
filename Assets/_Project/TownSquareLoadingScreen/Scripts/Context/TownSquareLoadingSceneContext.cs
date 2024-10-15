using _Project.SceneManagementUtilities.Controllers;
using _Project.SceneManagementUtilities.Signals;
using _Project.StrangeIOCUtility.CrossContext;
using _Project.TownSquareLoadingScreen.Scripts.Views;
using UnityEngine;

namespace _Project.TownSquareLoadingScreen.Scripts.Context
{
    public class TownSquareLoadingSceneContext : SignalContext
    {
        public TownSquareLoadingSceneContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            
            BindTownSquareLoadingSceneInjections();
        }
        
        protected override void postBindings()
        {
            base.postBindings();
        }
        
        private void BindTownSquareLoadingSceneInjections()
        {
            mediationBinder.Bind<WaitForTownSquareSceneLoadHandlerView>().To<WaitForTownSquareSceneLoadHandlerMediator>();
        }

    }
}