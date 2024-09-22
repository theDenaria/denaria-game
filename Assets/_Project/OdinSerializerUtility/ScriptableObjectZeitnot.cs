using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.OdinSerializerUtility
{
    //See: https://odininspector.com/tutorials/serialize-anything/implementing-the-odin-serializer
    [ShowOdinSerializedPropertiesInInspector]
    public class ScriptableObjectZeitnot : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        }
    }
}