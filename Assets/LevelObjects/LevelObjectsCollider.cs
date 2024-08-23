using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;


public class LevelObjectsCollider : MonoBehaviour
{
    public bool sendToLevelServer = false;
    // TODO: Move serverUrl to contants
    public string serverUrl = "http://localhost:3000/";

    public string version = "0_1";

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
            ColliderData data = new(version, parent.position, parent.localScale, parent.rotation);
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
                CubeData boxData;
                boxData.x = boxCollider.size.x;
                boxData.y = boxCollider.size.y;
                boxData.z = boxCollider.size.z;
                data.collider = JsonUtility.ToJson(boxData);
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
        yield return StartCoroutine(SendGetRequest("prepare?version=" + version));

        foreach (ColliderData colliderData in colliderDataList)
        {
            string jsonData = JsonUtility.ToJson(colliderData);
            yield return StartCoroutine(SendPostRequest("set-object", jsonData));
        }
        Debug.Log("All Objects Sent!");
    }

    private IEnumerator SendPostRequest(string route, string jsonData)
    {
        using UnityWebRequest www = UnityWebRequest.Post(serverUrl + route, jsonData, "application/json");
        www.certificateHandler = new BypassCertificate();
        Debug.Log("Request url" + serverUrl + route);
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

    private IEnumerator SendGetRequest(string route)
    {
        using UnityWebRequest www = UnityWebRequest.Get(serverUrl + route);
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
            // If you want to handle the response data, you can access it like this:
            // string responseText = www.downloadHandler.text;
            // Debug.Log(responseText);
        }
    }

    // Bypass SSL certificate validation (unsafe, use only in development)
    // private class BypassCertificate : CertificateHandler
    // {
    //     protected override bool ValidateCertificate(byte[] certificateData)
    //     {
    //         return true;
    //     }
    // }
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Always accept
            return true;
        }
    }
}
