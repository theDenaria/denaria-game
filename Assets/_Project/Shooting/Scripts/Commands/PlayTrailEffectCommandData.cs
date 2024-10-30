using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class PlayTrailEffectCommandData
    {
        public Vector3 TrailOrigin { get; set; }
        public Vector3 HitPoint { get; set; }
        public RaycastHit Hit { get; set; }

        public PlayTrailEffectCommandData(Vector3 trailOrigin, Vector3 hitPoint, RaycastHit hit)
        {
            TrailOrigin = trailOrigin;
            HitPoint = hitPoint;
            Hit = hit;
        }
    }
}