using _Project.DynamicUIWindowSystem.Scripts.Views.Base;
using _Project.UIZeitnot.TextMeshProZeitnot;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.DynamicUIWindowSystem.Scripts.Views.Popups
{
    public class ProgressPopupWindowView : DynamicWindowZeitnot
    {
        [field: SerializeField] private Slider ProgressSlider { get; set; }
        [field: SerializeField] private TextMeshProZeitnot TitleText { get; set; }
        [field: SerializeField] private TextMeshProZeitnot ProgressInfoText { get; set; }
        [field: SerializeField] private TextMeshProZeitnot ProgressPercentText { get; set; }
        [field: SerializeField] private string ProgressPercentTextFormat { get; set; } = "% {D}";

        internal void Set(string title, string progressInfo, float progressRatio)
        {
            SetTitle(title);
            SetProgressInfo(progressInfo);
            SetProgressPercent(progressRatio);
        }

        internal void SetTitle(string title)
        {
            TitleText.text = title ?? "";
        }

        internal void SetProgressInfo(string progressInfo)
        {
            ProgressInfoText.text = progressInfo ?? "";
        }

        internal void SetProgressPercent(float progressRatio)
        {
            ProgressPercentText.text = GetProgressPercentText(progressRatio);
        }

        private string GetProgressPercentText(float progressRatio)
        {
            progressRatio = Mathf.Clamp01(progressRatio);
            UpdateProgressSlider(progressRatio);
            return ((int)(progressRatio * 100f)).ToString(ProgressPercentTextFormat);
        }

        private void UpdateProgressSlider(float progressRatio)
        {
            //TODO: We can animate here by using DoTween lerp animation.
            ProgressSlider.value = progressRatio;
        }

    }
}
