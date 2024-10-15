using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UIZeitnot.Views
{
    [RequireComponent(typeof(LayoutElement))]
    public class ContentLayoutElementControlView : ViewZeitnot
    {
        [field: SerializeField] private LayoutElement LayoutElement { get; set; }
        [field: SerializeField] private CanvasGroup CanvasGroup { get; set; }

        public bool IsLayoutEnabled { get; private set; }

        private RectTransform RectTransform { get; set; }

        protected override void Awake()
        {
            base.Awake();
            RectTransform = GetComponent<RectTransform>();
        }

        protected override void OnDestroy()
        {
            RectTransform = null;
            base.OnDestroy();
        }

        public void SetLayoutEnability(bool isEnabled)
        {
            if (isEnabled)
            {
                SetLayoutAsEnabled();
            }
            else
            {
                SetLayoutAsDisabled();
            }
        }

        private void SetLayoutAsEnabled()
        {
            LayoutElement.enabled = true;
            CanvasGroup.alpha = 1f;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
            LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
        }

        private void SetLayoutAsDisabled()
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
            LayoutElement.enabled = false;
            LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
        }

    }
}
