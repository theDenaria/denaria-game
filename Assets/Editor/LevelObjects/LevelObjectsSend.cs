using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;

namespace LevelObjects
{
    public class LevelObjectsSendWindow : EditorWindow
    {
        private string serverUrl = "http://localhost:3000/";
        private string version = "0_1";
        private bool isExporting = false;
        private GameObject selectedObject;
        private string[] sceneObjectNames;
        private GameObject[] sceneObjects;
        private int selectedIndex = 0;

        private EditorCoroutine sendingCoroutine;
        private bool isCancelled = false;

        [MenuItem("Tools/Level Objects Collider Exporter")]
        public static void ShowWindow()
        {
            GetWindow<LevelObjectsSendWindow>("Collider Exporter");
        }

        void OnEnable()
        {
            RefreshSceneObjects();
        }

        void RefreshSceneObjects()
        {
            sceneObjects = FindObjectsOfType<GameObject>(true);
            sceneObjectNames = new string[sceneObjects.Length];
            for (int i = 0; i < sceneObjects.Length; i++)
            {
                sceneObjectNames[i] = sceneObjects[i].name;
            }
        }

        void OnGUI()
        {
            GUILayout.Label("Level Objects Sender Settings", EditorStyles.boldLabel);
            serverUrl = EditorGUILayout.TextField("Server URL", serverUrl);
            version = EditorGUILayout.TextField("Version", version);

            EditorGUILayout.Space();

            if (GUILayout.Button("Refresh Scene Objects"))
            {
                RefreshSceneObjects();
            }

            selectedIndex = EditorGUILayout.Popup("Select Object", selectedIndex, sceneObjectNames);
            if (selectedIndex >= 0 && selectedIndex < sceneObjects.Length)
            {
                selectedObject = sceneObjects[selectedIndex];
            }

            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(isExporting || selectedObject == null);
            if (GUILayout.Button("Send Level Objects"))
            {
                isExporting = true;
                isCancelled = false;
                ExportAndSendColliders();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!isExporting);
            if (GUILayout.Button("Stop Sending"))
            {
                StopSending();
            }
            EditorGUI.EndDisabledGroup();

            if (isExporting)
            {
                EditorGUILayout.HelpBox("Sending level objects...", MessageType.Info);
            }
        }

        private void ExportAndSendColliders()
        {
            if (selectedObject == null)
            {
                Debug.LogError("No object selected!");
                isExporting = false;
                return;
            }

            List<ColliderData> colliderDataList = new List<ColliderData>();

            Collider[] allColliders = selectedObject.GetComponentsInChildren<Collider>(true);
            foreach (Collider collider in allColliders)
            {
                ColliderData data = CreateColliderData(collider);
                if (data != null)
                {
                    colliderDataList.Add(data);
                }
            }

            sendingCoroutine = EditorCoroutineUtility.StartCoroutine(SendCollidersSequentially(colliderDataList), this);
        }

        private void StopSending()
        {
            if (sendingCoroutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(sendingCoroutine);
                sendingCoroutine = null;
            }
            isCancelled = true;
            isExporting = false;
            Debug.Log("Sending process cancelled.");
        }

        private ColliderData CreateColliderData(Collider collider)
        {
            ColliderData data = new ColliderData(version, collider.transform.position, collider.transform.localScale, collider.transform.rotation);

            if (collider is MeshCollider meshCollider)
            {
                Mesh mesh = meshCollider.sharedMesh;
                data.object_type = "MeshCollider";

                MeshData mesh_data = new MeshData();
                mesh_data.vertices = new List<Vector3>(mesh.vertices);
                mesh_data.triangles = new List<int>(mesh.triangles);

                data.collider = JsonUtility.ToJson(mesh_data);
            }
            else if (collider is BoxCollider boxCollider)
            {
                data.object_type = "BoxCollider";
                CubeData boxData = new CubeData();
                boxData.x = boxCollider.size.x;
                boxData.y = boxCollider.size.y;
                boxData.z = boxCollider.size.z;
                data.collider = JsonUtility.ToJson(boxData);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                data.object_type = "SphereCollider";
                SphereData sphereData = new SphereData();
                sphereData.radius = sphereCollider.radius;
                data.collider = JsonUtility.ToJson(sphereData);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                data.object_type = "CapsuleCollider";
                CapsuleData capsuleData = new CapsuleData();
                capsuleData.radius = capsuleCollider.radius;
                capsuleData.height = capsuleCollider.height;
                capsuleData.direction = capsuleCollider.direction;
                data.collider = JsonUtility.ToJson(capsuleData);
            }
            else
            {
                return null; // Unsupported collider type
            }

            return data;
        }

        private IEnumerator SendCollidersSequentially(List<ColliderData> colliderDataList)
        {
            yield return EditorCoroutineUtility.StartCoroutine(SendGetRequest("prepare?version=" + version), this);

            foreach (ColliderData colliderData in colliderDataList)
            {
                if (isCancelled)
                {
                    break;
                }
                string jsonData = JsonUtility.ToJson(colliderData);
                yield return EditorCoroutineUtility.StartCoroutine(SendPostRequest("set-object", jsonData), this);
            }

            if (!isCancelled)
            {
                Debug.Log("All Objects Sent!");
            }
            isExporting = false;
            isCancelled = false;
            Repaint();
        }

        private IEnumerator SendPostRequest(string route, string jsonData)
        {
            using UnityWebRequest www = UnityWebRequest.Post(serverUrl + route, jsonData, "application/json");
            Debug.Log("Sending POST data " + jsonData);
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
                Debug.LogError($"Response Code: {www.responseCode}");
                Debug.LogError($"URL: {www.url}");
                Debug.LogError($"Request Body: {jsonData}");
                if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {
                    Debug.LogError($"Response Body: {www.downloadHandler.text}");
                }
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

        private IEnumerator SendGetRequest(string route)
        {
            using UnityWebRequest www = UnityWebRequest.Get(serverUrl + route);
            Debug.Log("Sending GET request to " + www.url);
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                Debug.LogError(www.url);
            }
            else
            {
                Debug.Log("GET request successful!");
            }
        }

        public class BypassCertificate : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                // Always accept
                return true;
            }
        }
    }
}