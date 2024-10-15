using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Services;
using _Project.TownSquareLoadingScreen.Scripts.Signals;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class OwnPlayerSpawnedCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }
        [Inject] public OnPlayerSpawnCompletedSignal OnPlayerSpawnCompletedSignal { get; set; }

        public override void Execute()
        {
            DenariaServerService.StopSendingConnectMessage();//
            OnPlayerSpawnCompletedSignal.Dispatch();
        }
    }
}