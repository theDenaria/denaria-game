using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;


public class LevelObjectsCollider : MonoBehaviour
{
    [System.Serializable]
    public class ColliderData
    {
        public string object_type;
        public string position;
        public string scale;
        public string rotation;
        public string collider;  // Store as JSON string

        public ColliderData(Vector3 pos, Vector3 sc, Quaternion rot)
        {
            object_type = "";
            position = Vector3ToString(pos);
            scale = Vector3ToString(sc);
            rotation = QuaternionToString(rot);
            collider = null;  // Convert MeshData to JSON string
        }

        // public void SetMeshData(MeshData mesh)
        // {
        //     mesh_data = JsonUtility.ToJson(mesh);
        // }


    }

    public static string Vector3ToString(Vector3 vector)
    {
        return $"{{\"x\": {vector.x}, \"y\": {vector.y}, \"z\": {vector.z}}}";
    }

    private static string QuaternionToString(Quaternion quaternion)
    {
        return $"{{ \"x\": {quaternion.x}, \"y\": {quaternion.y}, \"z\": {quaternion.z}, \"w\": {quaternion.w}}}";
    }


    public bool sendToLevelServer = false;
    // TODO: Move serverUrl to contants
    public string serverUrl = "http://localhost:3000/set-objects";

    void Start()
    {
        if (sendToLevelServer)
        {
            Debug.Log("CHILD COUNT: " + transform.childCount);
            ExportAndSendColliders();
        }
    }

    public void ExportAndSendColliders()
    {
        List<ColliderData> colliderDataList = new List<ColliderData>();

        foreach (GameObject go in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            TraverseHierarchy(go.transform, colliderDataList);
        }

        StartCoroutine(SendCollidersSequentially(colliderDataList));
    }

    private void TraverseHierarchy(Transform parent, List<ColliderData> colliderDataList)
    {
        Collider collider = parent.GetComponent<Collider>();

        if (collider != null)
        {
            ColliderData data = new(parent.position, parent.localScale, parent.rotation);
            if (collider is MeshCollider meshCollider)
            {
                Mesh mesh = meshCollider.sharedMesh;
                data.object_type = "MeshCollider";

                MeshData mesh_data;
                mesh_data.vertices = new List<Vector3>(mesh.vertices);
                mesh_data.triangles = new List<int>(mesh.triangles);

                data.collider = JsonUtility.ToJson(mesh_data);

            }
            else if (collider is BoxCollider boxCollider)
            {
                data.object_type = "BoxCollider";
                data.collider = Vector3ToString(boxCollider.size);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                data.object_type = "SphereCollider";
                SphereData sphereData;
                sphereData.radius = sphereCollider.radius;
                data.collider = JsonUtility.ToJson(sphereData);

            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                data.object_type = "CapsuleCollider";
                CapsuleData capsuleData;
                capsuleData.radius = capsuleCollider.radius;
                capsuleData.height = capsuleCollider.height;
                capsuleData.direction = capsuleCollider.direction;
                data.collider = JsonUtility.ToJson(capsuleData);
            }

            colliderDataList.Add(data);
        }

        foreach (Transform child in parent)
        {
            TraverseHierarchy(child, colliderDataList);
        }
    }

    private IEnumerator SendCollidersSequentially(List<ColliderData> colliderDataList)
    {
        foreach (ColliderData colliderData in colliderDataList)
        {
            string jsonData = JsonUtility.ToJson(colliderData);
            yield return StartCoroutine(SendPostRequest(jsonData));
        }
        Debug.Log("All Objects Sent!");
    }

    private IEnumerator SendPostRequest(string jsonData)
    {
        using UnityWebRequest www = UnityWebRequest.Post(serverUrl, jsonData, "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            Debug.LogError(www.url);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    // Bypass SSL certificate validation (unsafe, use only in development)
    private class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
