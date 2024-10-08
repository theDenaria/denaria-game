/*using System.Collections.Generic;
using _Project.Analytics.Enums;
using _Project.Analytics.Models;
using _Project.LoggingAndDebugging;
using Firebase;
using Firebase.Analytics;
using UnityEngine.Analytics;

namespace _Project.Analytics.Services
{
    public class FirebaseAnalyticsService : BaseAnalyticsService<FirebaseAnalyticsEvent, FirebaseAnalyticsEventParameter>
    {
        AnalyticsResultWrapper localAnalyticsResult;
        public FirebaseApp FirebaseApp { get; set; }
        public FirebaseAnalyticsEvent FirebaseAnalyticsEvent { get; set; }
        
        public override void ValidateAnalyticsEventType(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent is not Models.FirebaseAnalyticsEvent @event)
            { 
                DebugLoggerMuteable.Log("Please provide FirebaseAnalyticsEvent");
                return;
            }
    
            FirebaseAnalyticsEvent = @event;
        }

        public override AnalyticsResultWrapper SendEventViaAPI(IAnalyticsEvent analyticsEvent)
        {
            AnalyticsResult analyticsResult;
            
            List<Parameter> firebaseCompatibleParameterList = new List<Parameter>();
            
            foreach (KeyValuePair<string, FirebaseAnalyticsEventParameter> eventParameter in FirebaseAnalyticsEvent.EventParameters)
            {
                Parameter parameter;

                if (eventParameter.Value.Value is double doubleValue)
                {
                    parameter = new Parameter(eventParameter.Key, doubleValue);
                    firebaseCompatibleParameterList.Add(parameter);
                }else if (eventParameter.Value.Value is string stringValue)
                {
                    parameter = new Parameter(eventParameter.Key, stringValue);
                    firebaseCompatibleParameterList.Add(parameter);
                }else if (eventParameter.Value.Value is long longValue)
                {
                    parameter = new Parameter(eventParameter.Key, longValue);
                    firebaseCompatibleParameterList.Add(parameter);
                }
            }

            FirebaseAnalytics.LogEvent(analyticsEvent.EventName, firebaseCompatibleParameterList.ToArray());

            localAnalyticsResult = AnalyticsResultWrapper.Ok; //TODO: Learn to know if Firebase event is successfully logged.
            return localAnalyticsResult;
        }

        public override void LogSentAnalyticsEvent(BaseAnalyticsEvent<IAnalyticsEventParameter> analyticsEvent, AnalyticsResultWrapper analyticsResultWrapper)
        {
            string resultMessage = "AnalyticsResult of the event " + analyticsEvent.GetType().Name +
                                   analyticsEvent.EventName + ": " + localAnalyticsResult;

            foreach (KeyValuePair<string, IAnalyticsEventParameter> firebaseAnalyticsEventParameter in analyticsEvent
                         .EventParameters)
            {
                FirebaseAnalyticsEventParameter firebaseAnalyticsEventParameterValueCasted = (FirebaseAnalyticsEventParameter)firebaseAnalyticsEventParameter.Value;
                resultMessage += $"\n{firebaseAnalyticsEventParameter.Key}: {firebaseAnalyticsEventParameterValueCasted.Value}";
            }

            DebugLoggerMuteable.Log(resultMessage);
        }
        
    }
}*/