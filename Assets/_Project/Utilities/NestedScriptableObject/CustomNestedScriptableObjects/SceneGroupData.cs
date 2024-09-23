using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[InlineEditor]
[CreateAssetMenu(fileName = "SceneGroupData", menuName = "SceneManagement/SceneGroupData", order = 0)]
#if UNITY_EDITOR
public class SceneGroupData : NestedScriptableObject
#else
public class SceneGroupData : ScriptableObject
#endif
{
    [field: SerializeField, OdinSerialize] public SceneGroupType SceneGroupType { get; set; }
    [field: SerializeField, OdinSerialize] public List<SceneObject> Scenes{ get; set; }
    [field: SerializeField, OdinSerialize] private bool WillReloadExistingScenes { get; set; }
    [field: SerializeField, OdinSerialize] private bool WillUseLoadingScreen { get; set; }
    //[field: SerializeField, OdinSerialize] public bool WillFocusToAScene { get; set; }//TODO: Only show Focused Scene if this is checked
        
    //[ShowIf("GetWillFocusToAScene")]
    //[ValueDropdown("GetAllScenes", IsUniqueList = true, DropdownTitle = "Select Scene", DrawDropdownForListElements = true, ExcludeExistingValuesInList = true)]
    [field: SerializeField, OdinSerialize] public SceneObject FocusedScene { get; set; }
        
    private IEnumerable GetAllScenes()
    {
        return Scenes;
    }
    private bool GetWillFocusToAScene()
    {
        return true; //WillFocusToAScene;
    }

    [Button("Add Scenes To Editor")]
    public void AddScenesToEditor()
    {
#if UNITY_EDITOR
        for (int i = 0; i < Scenes.Count; i++)
        {
            PlayerPrefs.SetString("SceneGroupToChange", SceneGroupType.ToString());

            if (i == 0)
            {
                for(int k = 0; k < SceneManager.sceneCountInBuildSettings; k++)
                {
                    if (Scenes[i].SceneName == Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(k)))
                    {
						EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(k));
                        continue;
					}
                }
            }
            else
            {
				for (int k = 0; k < SceneManager.sceneCountInBuildSettings; k++)
				{
					if (Scenes[i].SceneName == Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(k)))
					{
						EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(k), OpenSceneMode.Additive);
						continue;
					}
				}
            }
        }
#endif
    }
        
}