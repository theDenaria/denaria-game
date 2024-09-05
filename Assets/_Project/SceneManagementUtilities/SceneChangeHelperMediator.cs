using strange.extensions.mediation.impl;

namespace _Project.SceneManagementUtilities
{
    public class SceneChangeHelperMediator : Mediator
    {
        [Inject]
        public SceneChangeHelperView SceneChangeHelperView { get; set; }

        //[Inject]
        //public SaveSceneGroupsSignal SaveSceneGroupsSignal { get; set; }

        public override void OnRegister()
        {
            //GenreContinueButtonView.onButtonClickSignal.AddListener(SaveGenres);
            SceneChangeHelperView.init();
        }

        public override void OnRemove()
        {
            //GenreContinueButtonView.onButtonClickSignal.RemoveListener(SaveGenres);
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