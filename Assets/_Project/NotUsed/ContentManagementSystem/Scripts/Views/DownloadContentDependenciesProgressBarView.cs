using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.SliderZeitnot;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.ContentManagementSystem.Scripts.Views
{
    public class DownloadContentDependenciesProgressBarView : ViewZeitnot
    {
        [field: SerializeField, Required()] private CanvasGroup CanvasGroup { get; set; }
        [field: SerializeField] private SliderZeitnot DownloadProgressSlider { get; set; }

        internal void Show()
        {
            CanvasGroup.alpha = 1f;
        }

        internal void Hide()
        {
            CanvasGroup.alpha = 0f;
        }

        internal void SetProgressRatio(float progressRatio)
        {
            //TODO: We can animate slider's fill ratio with DoTween lerp animation here.
            DownloadProgressSlider.value = Mathf.Clamp01(progressRatio);
        }

    }
}
