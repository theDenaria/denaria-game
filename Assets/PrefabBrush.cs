using UnityEngine;
using UnityEditor;

public class PrefabBrush : EditorWindow
{
    public GameObject[] prefabsToPaint;   // Array of prefabs to paint
    public float brushSize = 1f;          // Brush size
    public int paintAmount = 1;           // Number of prefabs to paint per click

    private bool isPainting = false;

    [MenuItem("Tools/Prefab Brush")]
    public static void ShowWindow()
    {
        GetWindow<PrefabBrush>("Prefab Brush");
    }

    void OnGUI()
    {
        // GUI elements for setting the prefabs array, brush size, and paint amount
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty prefabsProperty = serializedObject.FindProperty("prefabsToPaint");

        EditorGUILayout.PropertyField(prefabsProperty, true);
        serializedObject.ApplyModifiedProperties();

        brushSize = EditorGUILayout.FloatField("Brush Size", brushSize);
        paintAmount = EditorGUILayout.IntField("Paint Amount", paintAmount);

        // Button to toggle painting mode
        if (GUILayout.Button(isPainting ? "Stop Painting" : "Start Painting"))
        {
            isPainting = !isPainting;
            SceneView.duringSceneGui -= OnSceneGUI;

            if (isPainting)
            {
                SceneView.duringSceneGui += OnSceneGUI;
            }
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        // Check if the left mouse button is clicked
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            PaintPrefabs();
            e.Use(); // Prevents other GUI events from triggering
        }

        // Display the brush preview in the Scene view
        if (e.type == EventType.Repaint)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Handles.color = Color.green;
                Handles.DrawWireDisc(hit.point, hit.normal, brushSize);
            }
        }
    }

    private void PaintPrefabs()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            for (int i = 0; i < paintAmount; i++)
            {
                Vector3 randomOffset = Random.insideUnitSphere * brushSize;
                randomOffset.y = 0; // Keeps the objects aligned on the surface
                Vector3 spawnPosition = hit.point + randomOffset;

                // Select a random prefab from the array
                GameObject prefabToPaint = prefabsToPaint[Random.Range(0, prefabsToPaint.Length)];

                // Generate a random rotation around the Y axis
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                // Generate a random scale for X, Y, and Z axes between 0.8-1.2
                Vector3 randomScale = Vector3.one * Random.Range(0.8f, 1.2f);

                // Instantiate the prefab with random rotation and scale
                GameObject instantiatedPrefab = Instantiate(prefabToPaint, spawnPosition, randomRotation);

                // Apply the random scale
                instantiatedPrefab.transform.localScale = randomScale;
            }
        }
    }
}
