using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "TranslatableTextModel", menuName = "Denaria/Language/TranslatableTextModel", order = 1)]
#if UNITY_EDITOR
    public class TranslatableTextModel : global::NestedScriptableObject
#else
    public class TranslatableTextModel : ScriptableObject
#endif
    {
        [field: SerializeField, OdinSerialize] public string Key { get; set; }
        [field: SerializeReference, OdinSerialize] public TranslationsDictionary Translations { get; set; }
        
        public TranslatableTextModel()
        {
            Key = string.Empty;
            // Initialize the Translations dictionary with an entry for each supported language
            Translations = new TranslationsDictionary();

            // Ensure every language has an entry, even if it's empty
            foreach (Languages language in Enum.GetValues(typeof(Languages)))
            {
                Translations[language] = string.Empty; // You can set a default value (e.g., empty string)
            }
        }
    }
}