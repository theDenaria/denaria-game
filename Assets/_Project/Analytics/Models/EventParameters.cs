using System.Collections.Generic;

namespace _Project.Analytics.Models
{
    public class EventParameters<T> : Dictionary<string, T> where T : IAnalyticsEventParameter
    {
        
    }
}