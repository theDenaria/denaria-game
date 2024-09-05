using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.ShowLoading.Mediators
{
    public class LoadingView : View
    {
        [field: SerializeField]private GameObject loadingAnimation { get; set; }

        private void Start()
        {
            loadingAnimation.SetActive(false);
        }

        public void ShowLoadingAnimation()
        {
            loadingAnimation.SetActive(true);
        }

        public void HideLoadingAnimation()
        {
            loadingAnimation.SetActive(false);
        }
    }
}
