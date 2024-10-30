using System.Collections.Generic;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Services;
using DefaultNamespace;
using strange.extensions.command.impl;

namespace _Project.Shooting.Scripts.Commands
{
    public class SpawnGunCommand : Command
    {
        [Inject] public IGunsModel GunsModel { get; set; }
        [Inject] public SpawnGunCommandData SpawnGunCommandData { get; set; }
        
        [Inject] public IShootingMechanicService ShootingMechanicService { get; set; }

        private GunType Gun { get; set; }
        private List<GunScriptableObject> Guns { get; set; }
        
        //[SerializeField] private PlayerIK InverseKinematics { get; set; } //TODO: Add later
        public GunScriptableObject ActiveGun { get; private set; }
        public override void Execute()
        {
            //GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

            GunScriptableObject gun = GunsModel.GetGunList()[0];
            if (gun == null)
            {
                Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
                return;
            }

            ActiveGun = gun;
            ShootingMechanicService.SetGunModel(gun.Spawn(SpawnGunCommandData.GunParent, SpawnGunCommandData.ActiveMonoBehaviour));

            //TODO: Enable for IK
            /*Transform[] allChildren = GunParent.GetComponentsInChildren<Transform>();
            InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
            InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
            InverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
            InverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");*/
        }
    }
}