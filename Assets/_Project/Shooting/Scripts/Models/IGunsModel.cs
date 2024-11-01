using System.Collections.Generic;
using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.ScriptableObjects;

namespace _Project.Shooting.Scripts.Models
{
    public interface IGunsModel
    {
        public void FillGunsModel(List<GunScriptableObject> guns);
        public List<GunScriptableObject> GetGunList();
    }
}