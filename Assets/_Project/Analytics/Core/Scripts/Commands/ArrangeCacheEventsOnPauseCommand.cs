using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.Core.Scripts.Commands
{
    public class ArrangeCacheEventsOnPauseCommand : Command
    {
        [Inject] public bool IsPause { get; set; }
        
        public override void Execute()
        {
            if (IsPause) return;
            if (Constants.CACHED_EVENTS_SENT)
            {
                foreach (string key in Constants.CACHE_EVENTS_PREF_KEYS)
                {
                    PlayerPrefs.SetString(key, Constants.NO_EVENT);
                }
            }
                
            Fail();
        }
    }
}