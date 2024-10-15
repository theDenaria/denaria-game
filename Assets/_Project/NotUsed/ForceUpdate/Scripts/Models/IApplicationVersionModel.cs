namespace _Project.ForceUpdate.Scripts.Models
{
    public interface IApplicationVersionModel
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
    }
}