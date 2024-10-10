namespace _Project.Analytics.Core.Scripts.Models
{
    public class UnityTestEvent : Unity.Services.Analytics.Event
    {
        public UnityTestEvent() : base("zzzTestEvent")
        {
        }

        public string CurrentSceneIdParameter { set { SetParameter("currentSceneId", value); } }
        public string DummyStringParameter { set { SetParameter("dummyStringParameter", value); } }
        public int DummyIntegerParameter { set { SetParameter("dummyIntegerParameter", value); } }
        public float DummyFloatParameter { set { SetParameter("dummyFloatParameter", value); } }
        public bool DummyBoolParameter { set { SetParameter("dummyBoolParameter", value); } }
    }
}