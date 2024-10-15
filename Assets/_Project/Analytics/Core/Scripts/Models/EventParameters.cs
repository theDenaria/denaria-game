using System.Collections.Generic;

namespace _Project.Analytics.Core.Scripts.Models
{
    public class EventParameters<T> : Dictionary<string, T> where T : IAnalyticsEventParameter
    {
        
    }
}