//Interface for a service.
//In this case, the service allows us to run a Coroutine outside
//the strict confines of a MonoBehaviour. (See RoutineRunner)

using System.Collections;
using UnityEngine;

namespace _Project.StrangeIOCUtility.Scripts.Utilities
{
    public interface IRoutineRunner
    {
        Coroutine StartCoroutine(IEnumerator method);
        void StopCoroutine(Coroutine routine);
        void StopAllCoroutines();
    }
}