using System.Collections.Generic;
using System.Linq;
using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.StrangeIOCUtility.Scripts.Views;
using DefaultNamespace;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Views
{
    [DisallowMultipleComponent]
    public class PlayerGunSelectorView : ViewZeitnot
    {
        [SerializeField] internal Transform GunParent { get; set; }
        
        internal Signal onPlayerGunSelectorReady = new Signal();

        internal void Init()
        {
            onPlayerGunSelectorReady.Dispatch();
            
            Debug.Log("uuu after spawn");
        }
    }
}