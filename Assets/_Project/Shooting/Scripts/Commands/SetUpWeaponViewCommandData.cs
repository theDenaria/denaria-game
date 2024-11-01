using _Project.Shooting.Scripts.ScriptableObjects;

namespace _Project.Shooting.Scripts.Commands
{
    public class SetUpWeaponViewCommandData
    {
        public GunScriptableObject GunScriptableObject { get; set; }

        public SetUpWeaponViewCommandData(GunScriptableObject gunScriptableObject)
        {
            GunScriptableObject = gunScriptableObject;
        }
    }
}