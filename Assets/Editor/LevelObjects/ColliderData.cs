using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using System;

namespace LevelObjects
{
    [System.Serializable]
    public class ColliderData
    {
        public string version;
        public string object_type;
        public string position;
        public string scale;
        public string rotation;
        public string collider;  // Store as JSON string

        public ColliderData(string version_i, Vector3 position_i, Vector3 scale_i, Quaternion rotation_i)
        {
            version = version_i;
            object_type = "";
            position = Vector3ToString(position_i);
            scale = Vector3ToString(scale_i);
            rotation = QuaternionToString(rotation_i);
            collider = "";  // Convert MeshData to JSON string
        }

        public static string Vector3ToString(Vector3 vector)
        {
            return $"{{\"x\": {vector.x}, \"y\": {vector.y}, \"z\": {vector.z}}}";
        }

        private static string QuaternionToString(Quaternion quaternion)
        {
            return $"{{ \"x\": {quaternion.x}, \"y\": {quaternion.y}, \"z\": {quaternion.z}, \"w\": {quaternion.w}}}";
        }
    }
}

