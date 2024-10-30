using System.Collections.Generic;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using DefaultNamespace;
using strange.extensions.command.impl;

namespace _Project.Shooting.Scripts.Commands
{
    public class SpawnGunCommand : Command
    {
        [Inject] public IGunsModel GunsModel { get; set; }
        [Inject] public SpawnGunCommandData SpawnGunCommandData { get; set; }

        private GunType Gun { get; set; }
        private List<GunScriptableObject> Guns { get; set; }
        
        //[SerializeField] private PlayerIK InverseKinematics { get; set; } //TODO: Add later
        public GunScriptableObject ActiveGun { get; private set; }
        public override void Execute()
        {
            Debug.Log("xxx SpawnGunCommand");
            //GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

            GunScriptableObject gun = GunsModel.GetGunList()[0];
            if (gun == null)
            {
                Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
                return;
            }

            ActiveGun = gun;
            gun.Spawn(SpawnGunCommandData.GunParent, SpawnGunCommandData.ActiveMonoBehaviour);
            
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