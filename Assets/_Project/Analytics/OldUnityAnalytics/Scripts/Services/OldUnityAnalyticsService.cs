using System.Collections.Generic;
using _Project.Analytics.Core.Scripts.Enums;
using _Project.Analytics.Core.Scripts.Models;
using _Project.Analytics.Core.Scripts.Services;
using _Project.Analytics.UnityAnalytics.Scripts.Models;
using _Project.LoggingAndDebugging;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine.Analytics;

namespace _Project.Analytics.UnityAnalytics.Scripts.Services
{
    public class OldUnityAnalyticsService : BaseAnalyticsService<OldUnityAnalyticsEvent,OldUnityAnalyticsEventParameter>
    {
        public override void ValidateAnalyticsEventType(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent is not OldUnityAnalyticsEvent)
            { 
                DebugLoggerMuteable.Log("Please provide UnityAnalyticsEvent");
                return;
            }
    
            OldUnityAnalyticsEvent oldUnityAnalyticsEvent = (OldUnityAnalyticsEvent) analyticsEvent;
        }
        
        public override AnalyticsResultWrapper SendEventViaAPI(IAnalyticsEvent analyticsEvent)
        {
            
            AnalyticsResult analyticsResult;
            Dictionary<string, object> eventParameterDictionary = new FlexibleDictionary<string, object>();
            
            OldUnityAnalyticsEvent oldUnityAnalyticsEvent = (OldUnityAnalyticsEvent)analyticsEvent;
            
            
            foreach (KeyValuePair<string, OldUnityAnalyticsEventParameter> eventParameter in oldUnityAnalyticsEvent.EventParameters)
            {
                OldUnityAnalyticsEventParameter parameter;
                
                if (eventParameter.Value.Value is float floatValue)
                {
                    parameter = new OldUnityAnalyticsEventParameter(floatValue);
                    //eventParameterDictionary.Add(eventParameter.Key, parameter);
                    
                    eventParameterDictionary.Add(eventParameter.Key, floatValue);

                }else if (eventParameter.Value.Value is int intValue)
                {
                    parameter = new OldUnityAnalyticsEventParameter(intValue);
                    //eventParameterDictionary.Add(eventParameter.Key, parameter);
                    
                    eventParameterDictionary.Add(eventParameter.Key, intValue);

                }else if (eventParameter.Value.Value is string stringValue)
                {
                    parameter = new OldUnityAnalyticsEventParameter(stringValue);
                    //eventParameterDictionary.Add(eventParameter.Key, parameter);
                    eventParameterDictionary.Add(eventParameter.Key, stringValue);
                }
            }
            
            
            
            analyticsResult = UnityEngine.Analytics.Analytics.CustomEvent(analyticsEvent.EventName, eventParameterDictionary);
            //analyticsResult = UnityEngine.Analytics.Analytics(analyticsEvent.EventName, eventParameterDictionary);

            AnalyticsResultWrapper analyticsResultWrapper = (AnalyticsResultWrapper)((int)analyticsResult);
            
            return analyticsResultWrapper;

            /*
            
            
            string resultMessage;
            foreach (KeyValuePair<string, IAnalyticsEventParameter> unityAnalyticsEventParameter in analyticsEvent.EventParameters)
            {
                OldUnityAnalyticsEventParameter unityAnalyticsEventParameterValueCasted = (OldUnityAnalyticsEventParameter)unityAnalyticsEventParameter.Value;
                resultMessage += $"\n{unityAnalyticsEventParameter.Key}: {unityAnalyticsEventParameterValueCasted.Value}";
            }

            
            //TODO: Uncomment following line and add opt out button to enable Unity Analytics.
            AnalyticsResult analyticsResult = UnityEngine.Analytics.Analytics.CustomEvent(analyticsEvent.EventName, analyticsEvent.EventParameters);
            
            //TODO: Comment following line to disable Unity Analytics.
            //AnalyticsResult analyticsResult = AnalyticsResult.Ok; //TODO: This is just for testing, delete later.
            
            return (AnalyticsResultWrapper)analyticsResult;
            */
            
            
        }

        public override void LogSentAnalyticsEvent(BaseAnalyticsEvent<IAnalyticsEventParameter> unityAnalyticsEvent, AnalyticsResultWrapper analyticsResultWrapper)
        {
            string resultMessage = "AnalyticsResult of the event " + unityAnalyticsEvent.GetType().Name +
                                   unityAnalyticsEvent.EventName + ": " + analyticsResultWrapper;

            foreach (KeyValuePair<string, IAnalyticsEventParameter> unityAnalyticsEventParameter in unityAnalyticsEvent
                         .EventParameters)
            {
                resultMessage += $"\n{unityAnalyticsEventParameter.Key}: {unityAnalyticsEventParameter.Value}";
            }

            DebugLoggerMuteable.Log(resultMessage);
        }
        
    }
}