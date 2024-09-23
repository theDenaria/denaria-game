using UnityEditor;
using UnityEngine;

namespace _Project.UIZeitnot.Editor
{
    #if UNITY_EDITOR
    [CustomEditor((typeof(Component)))]
    public static class ZeitnotSectionEditor
    {
        [MenuItem("GameObject/Zeitnot/ButtonZeitnot", false, 10)]
        private static void CreateButtonZeitnot(MenuCommand menuCommand)
        {
            GameObject prefab = Resources.Load<GameObject>("ButtonZeitnotBase");
            CreatePrefabInstance(prefab, menuCommand);
        }

        [MenuItem("GameObject/Zeitnot/ImageZeitnot", false, 10)]
        private static void CreateImageZeitnot(MenuCommand menuCommand)
        {
            GameObject prefab = Resources.Load<GameObject>("ImageZeitnot");
            CreatePrefabInstance(prefab, menuCommand);
        }

        [MenuItem("GameObject/Zeitnot/TextMeshProZeitnot", false, 10)]
        private static void CreateTextMeshProZeitnot(MenuCommand menuCommand)
        {
            GameObject prefab = Resources.Load<GameObject>("TextMeshProZeitnot");
            CreatePrefabInstance(prefab, menuCommand);
        }

        [MenuItem("GameObject/Zeitnot/PanelZeitnot", false, 10)]
        private static void CreatePanelZeitnot(MenuCommand menuCommand)
        {
            GameObject prefab = Resources.Load<GameObject>("PanelZeitnot");
            CreatePrefabInstance(prefab, menuCommand);
        }

        private static void CreatePrefabInstance(GameObject prefab, MenuCommand menuCommand)
        {
            if (prefab == null)
            {
                Debug.LogError("Prefab not found.");
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (instance != null && menuCommand.context is GameObject contextObject)
            {
                instance.transform.SetParent(contextObject.transform, false);
                Undo.RegisterCreatedObjectUndo(instance, "Create Prefab Instance");
                Selection.activeGameObject = instance;
            }
        }
    }
    #endif
}