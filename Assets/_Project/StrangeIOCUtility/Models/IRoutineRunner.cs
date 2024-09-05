//Interface for a service.
//In this case, the service allows us to run a Coroutine outside
//the strict confines of a MonoBehaviour. (See RoutineRunner)

using UnityEngine;
using System.Collections;

namespace _Project.StrangeIOCUtility.Models
{
    public interface IRoutineRunner
    {
        Coroutine StartCoroutine(IEnumerator method);
        void StopCoroutine(Coroutine routine);
        void StopAllCoroutines();
    }
}