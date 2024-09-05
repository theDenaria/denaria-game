﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.SceneManagementUtilities.Utilities
{
    //Unity scene object to easily assign scenes in the inspector.
    //See: https://gist.github.com/Hertzole/ac269f3148bc5192cc2eb6d472870d24
    [System.Serializable]
    public class SceneObject
    {
        public string SceneName;
    
        public static implicit operator string(SceneObject sceneObject)
        {
            return sceneObject.SceneName;
        }
    
        public static implicit operator SceneObject(string sceneName)
        {
            return new SceneObject() { SceneName = sceneName };
        }
    }
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneObject))]
    public class SceneObjectEditor : PropertyDrawer
    {
        protected SceneAsset GetSceneObject(string sceneObjectName)
        {
            if (string.IsNullOrEmpty(sceneObjectName))
                return null;
    
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
                if (scene.path.IndexOf(sceneObjectName) != -1)
                {
                    return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
                }
            }
    
            Debug.Log("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in the build settings.");
            return null;
        }
    
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneObj = GetSceneObject(property.FindPropertyRelative("SceneName").stringValue);
            var newScene = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
            if (newScene == null)
            {
                var prop = property.FindPropertyRelative("SceneName");
                prop.stringValue = "";
            }
            else
            {
                if (newScene.name != property.FindPropertyRelative("SceneName").stringValue)
                {
                    var scnObj = GetSceneObject(newScene.name);
                    if (scnObj == null)
                    {
                        Debug.LogWarning("The scene " + newScene.name + " cannot be used. To use this scene add it to the build settings for the project.");
                    }
                    else
                    {
                        var prop = property.FindPropertyRelative("SceneName");
                        prop.stringValue = newScene.name;
                    }
                }
            }
        }
    }
    #endif
}


