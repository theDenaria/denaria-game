namespace _Project.ForceUpdate.Scripts.Models
{
    public class ApplicationVersionModel : IApplicationVersionModel
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
    }
}