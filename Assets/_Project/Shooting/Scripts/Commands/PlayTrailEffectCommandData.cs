using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class PlayTrailEffectCommandData
    {
        public Vector3 TrailOrigin { get; set; }
        public Vector3 Hit { get; set; }
        //public RaycastHit Hit { get; set; }

        public PlayTrailEffectCommandData(Vector3 trailOrigin, Vector3 hit) //,RaycastHit hit
        {
            TrailOrigin = trailOrigin;
            Hit = hit;
            //Hit = hit;
        }
    }
}