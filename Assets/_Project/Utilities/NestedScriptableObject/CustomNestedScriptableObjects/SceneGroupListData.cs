using System.Collections.Generic;
#if UNITY_EDITOR
//using _Project.Utilities.Editor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "SceneGroupListData", menuName = "SceneManagement/SceneGroupListData", order = 1)]
public class SceneGroupListData : ScriptableObject//, ISceneGroupModel
{
#if UNITY_EDITOR
    [NestedScriptableObjectList]
#endif
    public List<SceneGroupData> sceneGroupList = new List<SceneGroupData>();

    /*public abstract class Nested<T> where T: SceneGroupData, new()
        {
            public List<T> items;
            protected Nested()
            {
                items = new List<T>();
                T item = new T();
                items.Add(item);
            }
        }

        public class DerivedClass : Nested<SceneGroupData>{}*/
}