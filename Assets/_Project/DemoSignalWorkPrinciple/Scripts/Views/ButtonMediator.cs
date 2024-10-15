using _Project.DemoSignalWorkPrinciple.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.DemoSignalWorkPrinciple.Scripts.Views
{
    public class ButtonMediator : Mediator
    {
        [Inject] public ButtonView View { get; set; }
        [Inject] public ASignal ASignal { get; set; }
        /*
        [Inject] public ASignal ASignal { get; set; }
        [Inject] public BSignal BSignal { get; set; }
        [Inject] public CSignal CSignal { get; set; }
        */
        private int demoCounter = 0;
        public override void OnRegister()
        {
            base.OnRegister();
         
            View.buttonClick.AddListener(OnButtonClick);
            
            View.init(); // Initializes View
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            View.buttonClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Debug.Log("Button Click : " + (++demoCounter).ToString());
            
            ASignal.Dispatch();
            /*
            ASignal.Dispatch();
            BSignal.Dispatch();
            CSignal.Dispatch();
            */
        }
    }
}