namespace _Project.ForceUpdate.Scripts.Models
{
    public interface IMinimumClientVersionNeededModel
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
    }
}