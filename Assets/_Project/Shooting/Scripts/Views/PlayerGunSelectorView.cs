using System.Collections.Generic;
using System.Linq;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.StrangeIOCUtility.Scripts.Views;
using DefaultNamespace;
using UnityEngine;

namespace _Project.Shooting.Scripts.Views
{
    [DisallowMultipleComponent]
    public class PlayerGunSelectorView : ViewZeitnot
    {
        [SerializeField] private GunType Gun { get; set; }
        [SerializeField] private Transform GunParent { get; set; }
        [SerializeField] private List<GunScriptableObject> Guns { get; set; }
        //[SerializeField] private PlayerIK InverseKinematics { get; set; } //TODO: Add later

        //[Space] 
        //[Header("Runtime Filled")] [SerializeField]
        [SerializeField] public GunScriptableObject ActiveGun { get; private set; }

        private void Start()
        {
            GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

            if (gun == null)
            {
                Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
                return;
            }

            ActiveGun = gun;
            Debug.Log("uuu before spawn");
            gun.Spawn(GunParent, this);
            Debug.Log("uuu after spawn");
            
            //TODO: Enable for IK
            /*Transform[] allChildren = GunParent.GetComponentsInChildren<Transform>();
            InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
            InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
            InverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
            InverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");*/
        }
        
    }
}