using _Project.DeviceUtility.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.DeviceUtility.Scripts.Commands
{
    public class ForceQuitApplicationCommand : Command
    {
        [Inject] public IDeviceUtilityService DeviceUtilityService { get; set; }

        public override void Execute()
        {
            DeviceUtilityService.QuitApplication();
        }
    }
}