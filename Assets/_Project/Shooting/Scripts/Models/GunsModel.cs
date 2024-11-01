using System.Collections.Generic;
using _Project.Shooting.Scripts.ScriptableObjects;

namespace _Project.Shooting.Scripts.Models
{
    public class GunsModel : IGunsModel
    {
        public List<GunScriptableObject> Guns = new ();
        public void FillGunsModel(List<GunScriptableObject> guns)
        {
            Guns = guns;
        }

        public List<GunScriptableObject> GetGunList()
        {
            return Guns;
        }
    }
}