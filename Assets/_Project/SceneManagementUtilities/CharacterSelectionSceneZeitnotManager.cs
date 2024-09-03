using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.SceneManagementUtilities
{
    public class CharacterSelectionSceneZeitnotManager : SceneZeitnot
    {
        [SerializeField] private GameObject Camera;
        [SerializeField] private GameObject Lights;
        [SerializeField] private GameObject Environment;
    
        public override bool AllowMultipleInstances => false;
    
        ///protected override PseudoSceneTypes GetSceneType() => PseudoSceneTypes.Selection;
        ///protected override string GetScenePath() => ScenesHelper.kCharacterSelectionScene;
    
        private void Awake()
        {
            ///SceneGroupTypes = SceneGroupTypes.Selection;
            ///Path = ScenesHelper.kCharacterSelectionScene;
            
            ///if (SceneChangeService.Instance)
            {
               /// SceneChangeService.Instance.AddPseudoScene(GetScenePath(), this);
            }
        
            // SetObjectsActive(false);
        }

        public override void SetObjectsActive(bool value)
        {
            Camera.SetActive(value);
            Lights.SetActive(value);
            Environment.SetActive(value);
        }
    }

}