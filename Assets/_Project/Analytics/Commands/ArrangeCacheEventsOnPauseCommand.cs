using strange.extensions.command.impl;
using UnityEngine;
using _Project.Utilities;

namespace _Project.Analytics.Commands
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