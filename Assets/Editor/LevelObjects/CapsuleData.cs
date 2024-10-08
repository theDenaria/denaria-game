using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;


namespace LevelObjects
{
    [System.Serializable]
    public struct CapsuleData
    {
        public float radius;
        public float height;
        public int direction;
    }
}