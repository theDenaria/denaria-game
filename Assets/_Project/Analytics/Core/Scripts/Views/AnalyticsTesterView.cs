using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;

namespace _Project.Analytics.Core.Scripts.Views
{
    public class AnalyticsTesterView : ViewZeitnot
    {
        //public ButtonZeitnot buttonPrefab;

        internal Signal buttonPressedSignal = new Signal();

        internal void init()
        {
            //buttonPrefab.onClick.AddListener(HandleButtonPressed);
        }

        private void OnDisable() {
            //buttonPrefab.onClick.RemoveListener(HandleButtonPressed);
        }
        
        public void HandleButtonPressed()
        {
            //buttonPressedSignal.Dispatch();
        }
        
    }
}