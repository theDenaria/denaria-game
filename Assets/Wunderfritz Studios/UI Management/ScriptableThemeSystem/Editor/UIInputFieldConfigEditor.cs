/* 
 * Version: 1.0.1
 * 
 * Description: 
 * This Editor script adds a button labeled "Update Input Field Injectors" 
 * to manually update the ThemeInjectorInputField.
 * 
 * In addition, the script modifies the inspector to include a toggleable section for the 
 * caretColor property based on the value of the customCaretColor bool property.
 * 
 * (c) Copyright by Wunderfritz Studios.
*/

using UnityEditor;
using UnityEngine;

namespace WunderfritzTools.UiManagement.ScriptableThemeSystem
{
    [CustomEditor(typeof(UIInputFieldConfig))]
    public class UIInputFieldConfigEditor : Editor
    {
        private SerializedProperty backgroundSpriteProp;
        private SerializedProperty normalColorProp;
        private SerializedProperty highlightedColorProp;
        private SerializedProperty pressedColorProp;
        private SerializedProperty selectedColorProp;
        private SerializedProperty disabledColorProp;
        private SerializedProperty inputFontMaterialProp;
        private SerializedProperty inputTextColorProp;
        private SerializedProperty placeholderFontMaterialProp;
        private SerializedProperty placeholderColorProp;
        private SerializedProperty customCaretColorProp;
        private SerializedProperty caretColorProp;
        private SerializedProperty selectionColorProp;

        private void OnEnable()
        {
            backgroundSpriteProp = serializedObject.FindProperty("backgroundSprite");
            normalColorProp = serializedObject.FindProperty("normalColor");
            highlightedColorProp = serializedObject.FindProperty("highlightedColor");
            pressedColorProp = serializedObject.FindProperty("pressedColor");
            selectedColorProp = serializedObject.FindProperty("selectedColor");
            disabledColorProp = serializedObject.FindProperty("disabledColor");
            inputFontMaterialProp = serializedObject.FindProperty("inputFontMaterial");
            inputTextColorProp = serializedObject.FindProperty("inputTextColor");
            placeholderFontMaterialProp = serializedObject.FindProperty("placeholderFontMaterial");
            placeholderColorProp = serializedObject.FindProperty("placeholderColor");
            customCaretColorProp = serializedObject.FindProperty("customCaretColor");
            caretColorProp = serializedObject.FindProperty("caretColor");
            selectionColorProp = serializedObject.FindProperty("selectionColor");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(backgroundSpriteProp);
            EditorGUILayout.PropertyField(normalColorProp);
            EditorGUILayout.PropertyField(highlightedColorProp);
            EditorGUILayout.PropertyField(pressedColorProp);
            EditorGUILayout.PropertyField(selectedColorProp);
            EditorGUILayout.PropertyField(disabledColorProp);
            EditorGUILayout.PropertyField(inputFontMaterialProp);
            EditorGUILayout.PropertyField(inputTextColorProp);
            EditorGUILayout.PropertyField(placeholderFontMaterialProp);
            EditorGUILayout.PropertyField(placeholderColorProp);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(customCaretColorProp);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                Repaint();
            }

            if (customCaretColorProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(caretColorProp);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(selectionColorProp);

            GUILayout.Space(10);

            UIInputFieldConfig inputFieldConfig = target as UIInputFieldConfig;

            if (GUILayout.Button("Update Input Field Injectors"))
            {
                inputFieldConfig.UpdateInputFieldConfigInjectors();
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
