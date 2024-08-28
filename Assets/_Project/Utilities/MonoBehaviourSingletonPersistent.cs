using UnityEngine;

namespace _Project.Utilities
{
    //WARNING: We VERY RARELY use Singletons, because it is mostly an anti-pattern and contradicts SOLID principles.
    //Only use Singleton pattern as last resort. Prefer StrangeIOC map "ToSingleton()" feature instead.
    //See: https://www.davidtanzer.net/david's%20blog/2016/03/14/6-reasons-why-you-should-avoid-singletons.html
    public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
        where T : Component
    {
        public static T Instance { get; private set; }
	
        public virtual void Awake ()
        {
            if (Instance == null) {
                Instance = this as T;
                DontDestroyOnLoad (this);
            } else {
                Destroy (gameObject);
            }
        }
    }
}