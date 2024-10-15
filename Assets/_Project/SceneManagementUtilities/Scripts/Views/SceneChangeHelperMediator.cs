using strange.extensions.mediation.impl;

namespace _Project.SceneManagementUtilities.Scripts.Views
{
    public class SceneChangeHelperMediator : Mediator
    {
        [Inject]
        public SceneChangeHelperView SceneChangeHelperView { get; set; }

        //[Inject]
        //public SaveSceneGroupsSignal SaveSceneGroupsSignal { get; set; }

        public override void OnRegister()
        {
            SceneChangeHelperView.init();
        }

        public override void OnRemove()
        {
        }

        private void Start()
        {
            
            //
        }

        private void SaveGenres()
        {
            //SaveSceneGroupsSignal.Dispatch();
        }
    }
}