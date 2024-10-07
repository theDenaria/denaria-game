using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;

namespace _Project.Analytics.Views
{
    public class AnalyticsTesterView : ViewZeitnot
    {
        public ButtonZeitnot buttonPrefab;

        internal Signal ButtonPressedSignal = new Signal();

        internal void init()
        {
            //buttonPrefab.onClick.AddListener(HandleButtonPressed);
        }

        private void OnDisable() {
            //buttonPrefab.onClick.RemoveListener(HandleButtonPressed);
        }
        
        public void HandleButtonPressed()
        {
            //ButtonPressedSignal.Dispatch();
        }

        protected override void Start()
        {
            //base.Awake();
            ButtonPressedSignal.Dispatch();
        }
    }
}