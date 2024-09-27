using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;

namespace _Project.DemoSignalWorkPrinciple.Views
{
    public class ButtonView : ViewZeitnot
    {
        internal Signal buttonClick = new Signal(); 
        private ButtonZeitnot Button { get; set; }
        internal void init() // No OnStart, OnEnable, OnAwake Methods
        {
            Button = GetComponent<ButtonZeitnot>();
            Button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            buttonClick.Dispatch(); // In contact with mediator trough internal signal
        }
    }
}