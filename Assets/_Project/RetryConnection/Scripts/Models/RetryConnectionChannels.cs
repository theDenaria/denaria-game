using System.Collections.Generic;

namespace _Project.RetryConnection.Scripts.Models
{
    public class RetryConnectionChannels : IRetryConnectionChannels
    {
        public List<string> ChannelsList { get; set; } = new List<string>();
    }
}