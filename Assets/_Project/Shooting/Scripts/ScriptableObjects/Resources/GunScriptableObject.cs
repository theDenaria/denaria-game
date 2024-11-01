using System;
using System.Collections;
using _Project.Shooting.Scripts.Signals;
using DefaultNamespace;
using LlamAcademy.Guns;
using strange.extensions.injector.impl;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

namespace _Project.Shooting.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunConfiguration", menuName = "Denaria/Guns/Gun Configuration", order = 0)]
    public class GunScriptableObject : ScriptableObject
    {
        //[Inject] public OnTargetHitSignal OnTargetHitSignal { get; set; }

        //public ImpactType ImpactType; //TODO: Enable
        
        public GunType Type;
        public string Name;
        public GameObject GunModelPrefab;
        public Vector3 SpawnPoint;
        public Vector3 SpawnRotation;
        public Vector3 SpawnScale; //TODO: Use later

        public ShootConfigurationScriptableObject ShootConfiguration;
        public TrailConfigurationScriptableObject TrailConfiguration;
        public DamageConfigurationScriptableObject DamageConfiguration;
        
    }
}