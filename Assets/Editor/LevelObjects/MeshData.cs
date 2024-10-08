using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

namespace LevelObjects
{
    [System.Serializable]
    public struct MeshData
    {
        public List<Vector3> vertices;
        public List<int> triangles;
    }
}