using System;
using _Project.ShowLoading.Signals;
using strange.extensions.mediation.impl;

namespace _Project.ShowLoading.Mediators
{
    public class LoadingMediator : Mediator
    {
        [Inject] public LoadingView LoadingView { get; set; }
        [Inject] public HideLoadingAnimationSignal HideLoadingAnimationSignal { get; set; }
        [Inject] public ShowLoadingAnimationSignal ShowLoadingAnimationSignal { get; set; }

        public override void OnRegister()
        {
            ShowLoadingAnimationSignal.AddListener(ShowLoadingAnimation);
            HideLoadingAnimationSignal.AddListener(HideLoadingAnimation);
            
            //LoadingPanel.LoadingOnVisibilityChanged += OnLoadingOnVisibilityChanged;//That is NN class
            //NNBusSystem.OnDatePrinterAwake += HideLoadingAnimation;//TODO: Uncomment and change 21 August

        }
        public override void OnRemove()
        {
            ShowLoadingAnimationSignal.AddListener(ShowLoadingAnimation);
            HideLoadingAnimationSignal.RemoveListener(HideLoadingAnimation);
            //LoadingPanel.LoadingOnVisibilityChanged -= OnLoadingOnVisibilityChanged;//That is NN class
            //NNBusSystem.OnDatePrinterAwake -= HideLoadingAnimation;//TODO: Uncomment and change 21 August
        }

        private void OnLoadingOnVisibilityChanged(bool isVisible)
        {
            
            if (isVisible)
            {
                ShowLoadingAnimation();
            }
            else
            {
                HideLoadingAnimation();
            }
        }

        private void HideLoadingAnimation()
        {
            LoadingView.HideLoadingAnimation();
        }
        
        private void ShowLoadingAnimation()
        {
            LoadingView.ShowLoadingAnimation();
        }
        
    }
}