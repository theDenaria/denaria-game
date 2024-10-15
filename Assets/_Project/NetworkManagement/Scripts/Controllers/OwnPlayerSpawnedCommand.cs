using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Services;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class OwnPlayerSpawnedCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.StopSendingConnectMessage();
        }
    }
}