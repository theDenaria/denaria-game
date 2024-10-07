using _Project.SceneManagementUtilities.Models;

namespace _Project.Analytics.Models
{
    public class TestAnalyticsEvent : UnityAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; } //Get as much parameters as you can from injectable models and services. 

        public TestAnalyticsEvent() //Empty contructor is needed for Injection Factory to work properly.
        {
        }
        
        //Get the specific parameters, for example the ones that are updated at playtime, via calling SetParametersAndReturn method from the class that holds relevant data.
        public TestAnalyticsEvent SetParametersAndReturn(float dummyFloatParameter, long dummyLongParameter, string dummyStringParameter)
        {
            EventName = "zzzTestEvent";
            
            string currentSceneId = CurrentSceneModel.CurrentSceneId;
            
            EventParameters.Add(nameof(currentSceneId), new UnityAnalyticsEventParameter(currentSceneId));
            EventParameters.Add(nameof(dummyFloatParameter), new UnityAnalyticsEventParameter(dummyFloatParameter));
            EventParameters.Add(nameof(dummyLongParameter), new UnityAnalyticsEventParameter(dummyLongParameter));
            EventParameters.Add(nameof(dummyStringParameter), new UnityAnalyticsEventParameter(dummyStringParameter));
            
            return this;
        }
        
    }
}