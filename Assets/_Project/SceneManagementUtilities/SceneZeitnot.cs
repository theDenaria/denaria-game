using _Project.SceneManagementUtilities.Utilities;
using UnityEngine;
namespace _Project.SceneManagementUtilities
{
    public abstract class SceneZeitnot : MonoBehaviour
    {
        public SceneGroupType SceneGroupType { get; set; }
        public string Path { get; set; }

        ///protected abstract PseudoSceneTypes GetSceneType();
        ///protected abstract string GetScenePath();
    
        // protected virtual GameObject GetEnvironment();

        public virtual bool AllowMultipleInstances => false;

        public virtual SceneZeitnot Initialize()
        {
            return this;
        }

        public virtual void Release() { }
    
        public virtual void SetObjectsActive(bool value) { }

        public void ShowEnvironment()
        {
        
        }

        public void HideEnvironment()
        {
        
        }

        public void Fadein(float duration)
        {
        
        }
    
        public void Fadeout(float duration)
        {
        
        }
    }

}