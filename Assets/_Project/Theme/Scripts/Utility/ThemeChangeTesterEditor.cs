using UnityEditor;
using UnityEngine;

namespace _Project.Theme.Scripts.Utility
{
    [CustomEditor(typeof(ThemeChangeTester))]
    public class ThemeChangeTesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector fields
            DrawDefaultInspector();

            // Reference to the target object
            ThemeChangeTester tester = (ThemeChangeTester)target;

            // Add a button to call the SelectTheme method
            if (GUILayout.Button("Select Theme"))
            {
                tester.SelectTheme(); // Calls the SelectTheme method with the inspector value
            }
        }
    }
}