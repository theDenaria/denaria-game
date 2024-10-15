using _Project.SceneManagementUtilities.Scripts.Models;

namespace _Project.Analytics.Core.Scripts.Models
{
    //Combination of TestAnalyticsEvent and UnityTestEvent. This is the example to be followed.
    public class UnityTestEvent : Unity.Services.Analytics.Event, IAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; } //Get as much parameters as you can from injectable models and services. 
        public string EventName { get; set; }
        
        //Parameters of the event. Should be in sync with the event on the dashboard.
        public string CurrentSceneIdParameter { set { SetParameter("currentSceneId", value); } }
        public string DummyStringParameter { set { SetParameter("dummyStringParameter", value); } }
        public int DummyIntegerParameter { set { SetParameter("dummyIntegerParameter", value); } }
        public float DummyFloatParameter { set { SetParameter("dummyFloatParameter", value); } }
        public bool DummyBoolParameter { set { SetParameter("dummyBoolParameter", value); } }
        //

        public UnityTestEvent() : base("zzzNewTestEvent")//Empty constructor is needed for Injection Factory to work properly.
        {
            
        }
        
        //Get the specific parameters, for example the ones that are updated at playtime, via calling SetParametersAndReturn method from the class that holds relevant data.
        public UnityTestEvent SetParametersAndReturn(string dummyStringParameter, int dummyIntegerParameter, float dummyFloatParameter,  bool dummyBoolParameter)
        {
            EventName = "zzzNewTestEvent"; //Should be same with the name in constructor. Repetition, but needed to conform to interface and work with the system.
            
            string currentSceneId = CurrentSceneModel.CurrentSceneId;
            
            CurrentSceneIdParameter = currentSceneId;
            DummyStringParameter = dummyStringParameter;
            DummyIntegerParameter = dummyIntegerParameter;
            DummyFloatParameter = dummyFloatParameter;
            DummyBoolParameter = dummyBoolParameter;
            
            Debug.Log("xxx UnityTestEvent created");
            
            return this;
        }
    }
}