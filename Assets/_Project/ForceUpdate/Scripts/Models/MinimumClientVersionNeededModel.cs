using System;

namespace _Project.ForceUpdate.Scripts.Models
{
    //https://semver.org/
    //https://softwareengineering.stackexchange.com/questions/368030/is-a-breaking-change-bug-fix-a-semver-patch-or-a-major
    [Serializable]
    public class MinimumClientVersionNeededModel : IMinimumClientVersionNeededModel
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }

        public MinimumClientVersionNeededModel()
        {
            
        }
    }
}