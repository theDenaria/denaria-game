using _Project.LoadingScreen.Scripts;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Controllers;
using _Project.SceneManagementUtilities.Models;
using _Project.SceneManagementUtilities.Services;
using _Project.SceneManagementUtilities.Signals;
using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.CrossContext;
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