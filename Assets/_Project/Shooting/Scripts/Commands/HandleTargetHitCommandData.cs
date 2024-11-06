using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class HandleTargetHitCommandData
    {
        public RaycastHit Hit { get; set; }
        public Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }
        
        public HandleTargetHitCommandData(RaycastHit hit,Vector3 startPoint, Vector3 endPoint)
        {
            Hit = hit;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}