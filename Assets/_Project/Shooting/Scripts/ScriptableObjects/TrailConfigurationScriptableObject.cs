using UnityEngine;

namespace _Project.Shooting.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TrailConfiguration", menuName = "Denaria/Guns/Gun Trail Configuration", order = 4)]
    public class TrailConfigurationScriptableObject : ScriptableObject
    {
        [Header("For Trail Renderer")]
        public Material Material;
        public AnimationCurve WidthCurve;
        public float Duration = 0.5f;
        public float MinimumVertexDistance = 0.1f;
        public Gradient Color;
        
        [Header("Other Configuration")]
        public float MissDistance = 100f;
        public float SimulationSpeed = 100f;
    }
}