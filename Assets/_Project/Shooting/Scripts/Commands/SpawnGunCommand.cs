using System.Collections.Generic;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Services;
using _Project.Shooting.Scripts.Signals;
using DefaultNamespace;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class SpawnGunCommand : Command
    {
        [Inject] public IGunsModel GunsModel { get; set; }
        [Inject] public SpawnGunCommandData SpawnGunCommandData { get; set; }
        
        [Inject] public IShootingMechanicService ShootingMechanicService { get; set; }
        [Inject] public SetUpGunViewSignal SetUpGunViewSignal { get; set; }

        private GunType Gun { get; set; }
        private List<GunScriptableObject> Guns { get; set; }
        
        //[SerializeField] private PlayerIK InverseKinematics { get; set; } //TODO: Add later
        public GunScriptableObject ActiveGun { get; private set; }
        
        private MonoBehaviour ActiveMonoBehaviour;
        private GameObject GunModelInstance;
        
        
        public override void Execute()
        {
            //GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

            ActiveGun = GunsModel.GetGunList()[0];
            if (ActiveGun == null)
            {
                Debug.LogError($"No GunScriptableObject found for GunType: {ActiveGun}");
                return;
            }

            GameObject gunInstance = Spawn(SpawnGunCommandData.GunParent, SpawnGunCommandData.ActiveMonoBehaviour);
            ShootingMechanicService.SetGunModel(gunInstance);

            SetUpGunViewSignal.Dispatch(new SetUpWeaponViewCommandData(ActiveGun));

            //TODO: Enable for IK
            /*Transform[] allChildren = GunParent.GetComponentsInChildren<Transform>();
            InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
            InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
            InverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
            InverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");*/
        }
        
        public GameObject Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour, Camera ActiveCamera = null)
        {
            this.ActiveMonoBehaviour = ActiveMonoBehaviour;

            //GetCameraReference(ActiveCamera);
            
            //LastShootTime = 0; //In Editor, this will not be properly reset, in build it is fine.

            GameObject gunModelInstance = SetUpGunModel(Parent);
            
            return gunModelInstance;
        }
        
        private GameObject SetUpGunModel(Transform Parent)
        {
            GunModelInstance = GameObject.Instantiate(ActiveGun.GunModelPrefab, Parent, false);
            GunModelInstance.transform.localPosition = ActiveGun.SpawnPoint;
            GunModelInstance.transform.localRotation = Quaternion.Euler(ActiveGun.SpawnRotation);
            //ShootSystem = GunModelInstance.GetComponentInChildren<ParticleSystem>();

            return GunModelInstance;
        }

    }
}