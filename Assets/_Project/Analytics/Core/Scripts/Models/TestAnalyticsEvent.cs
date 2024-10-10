using _Project.Analytics.UnityAnalytics.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Models;
using Unity.Services.Analytics;

namespace _Project.Analytics.Core.Scripts.Models
{
    public class TestAnalyticsEvent : UnityAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; } //Get as much parameters as you can from injectable models and services. 

        public TestAnalyticsEvent() //Empty contructor is needed for Injection Factory to work properly.
        {
        }
        
        //Get the specific parameters, for example the ones that are updated at playtime, via calling SetParametersAndReturn method from the class that holds relevant data.
        public TestAnalyticsEvent SetParametersAndReturn(string dummyStringParameter, int dummyIntegerParameter, float dummyFloatParameter,  bool dummyBoolParameter)
        {
            EventName = "zzzTestEvent";
            
            string currentSceneId = CurrentSceneModel.CurrentSceneId;
            
            EventParameters.Add(nameof(currentSceneId), new UnityAnalyticsEventParameter(currentSceneId));
            EventParameters.Add(nameof(dummyStringParameter), new UnityAnalyticsEventParameter(dummyStringParameter));
            EventParameters.Add(nameof(dummyIntegerParameter), new UnityAnalyticsEventParameter(dummyIntegerParameter));
            EventParameters.Add(nameof(dummyFloatParameter), new UnityAnalyticsEventParameter(dummyFloatParameter));
            //EventParameters.Add(nameof(dummyLongParameter), new UnityAnalyticsEventParameter(dummyLongParameter));
            EventParameters.Add(nameof(dummyBoolParameter), new UnityAnalyticsEventParameter(dummyBoolParameter));
            
            
            UnityTestEvent unityTestEvent = new UnityTestEvent();
            unityTestEvent.CurrentSceneIdParameter = currentSceneId;
            unityTestEvent.DummyStringParameter = dummyStringParameter;
            unityTestEvent.DummyIntegerParameter = dummyIntegerParameter;
            unityTestEvent.DummyFloatParameter = dummyFloatParameter;
            unityTestEvent.DummyBoolParameter = dummyBoolParameter;
            
            /*
            UnityTestEvent unityTestEvent2 = new UnityTestEvent
            {
                CurrentSceneIdParameter = currentSceneId,
                DummyStringParameter = dummyStringParameter,
                DummyIntegerParameter = dummyIntegerParameter,
                DummyFloatParameter = dummyFloatParameter,
                DummyBoolParameter = dummyBoolParameter,
            };*/
            
            AnalyticsService.Instance.RecordEvent(unityTestEvent);
            Debug.Log("xxx unityTestEvent sent");
            
            return this;
            
            
            
        }
        
    }
}