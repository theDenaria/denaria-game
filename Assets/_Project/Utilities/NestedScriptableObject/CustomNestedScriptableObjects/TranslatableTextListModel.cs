using System.Collections.Generic;
using UnityEngine;

namespace _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects
{
    [CreateAssetMenu(fileName = "TranslatableTextListModel", menuName = "Denaria/Language/Translations", order = 0)]
    public class TranslatableTextListModel : ScriptableObject
    {
#if UNITY_EDITOR
        [NestedScriptableObjectList]
#endif
        public List<TranslatableTextModel> translatableTextModelList;

    }
}