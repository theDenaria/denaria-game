using UnityEngine;

namespace _Project.Shooting.Scripts.Enemy
{
    public interface ISlowable 
    {
        void Slow(AnimationCurve SlowCurve);
    }
}
