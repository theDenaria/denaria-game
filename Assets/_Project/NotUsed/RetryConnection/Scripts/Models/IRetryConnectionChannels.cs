using System.Collections.Generic;

namespace _Project.RetryConnection.Scripts.Models
{
    public interface IRetryConnectionChannels
    {
        List<string> ChannelsList { get; set; }
    }
}