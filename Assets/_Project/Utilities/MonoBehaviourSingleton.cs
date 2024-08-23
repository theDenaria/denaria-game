using UnityEngine;

namespace _Project.Utilities
{
    //WARNING: We VERY RARELY use Singletons, because it is mostly an anti-pattern and contradicts SOLID principles.
    //Only use Singleton pattern as last resort. Prefer StrangeIOC map "ToSingleton()" feature instead.
    //See: https://www.davidtanzer.net/david's%20blog/2016/03/14/6-reasons-why-you-should-avoid-singletons.html
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    var objs = FindObjectsOfType (typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1) {
                        Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null) {
                        GameObject obj = new GameObject ();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T> ();
                    }
                }
                return _instance;
            }
        }
    }
}