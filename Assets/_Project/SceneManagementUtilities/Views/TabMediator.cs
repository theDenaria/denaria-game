/*using _Project.NavigationBar.Scripts.Commands;
using _Project.NavigationBar.Scripts.Enums;
using _Project.OutOfProfiles.Signals;
using _Project.SceneManagementUtilities.Signals;*/
using strange.extensions.mediation.impl;

namespace _Project.SceneManagementUtilities.Views
{
    //TODO: Uncomment later - 14 August 2024
    public class TabMediator : Mediator
    {
        /*
        [Inject] public TabView View { get; set; }
        [Inject] public ToggleOutOfProfilesCanvasSignal ToggleOutOfProfilesCanvasSignal { get; set; }
        [Inject] public ControlAndToggleOutOfProfileCanvasSignal ControlAndToggleOutOfProfileCanvasSignal { get; set; }
        [Inject] public TabCanvasRegisteredSignal TabCanvasRegisteredSignal { get; set; }
        public override void OnRegister()
        {
            base.OnRegister();
            View.onCardSwipeCanvasToggle.AddListener(OnCardSwipeCanvasToggle);
            TabCanvasRegisteredSignal.Dispatch();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onCardSwipeCanvasToggle.RemoveListener(OnCardSwipeCanvasToggle);
        }
        
        [ListensTo(typeof(OnNavigationButtonClickSignal))]
        public void ToggleCanvas(NavigationButtonType navigationButtonType)
        {
            View.ToggleCanvas(navigationButtonType);//TODO: Use Signals later
        }

        private void OnCardSwipeCanvasToggle(bool toggle)
        {
            if(!toggle) ToggleOutOfProfilesCanvasSignal.Dispatch(toggle);
            else ControlAndToggleOutOfProfileCanvasSignal.Dispatch();
        }
        */
    }
}