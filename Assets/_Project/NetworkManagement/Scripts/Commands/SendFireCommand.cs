using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.Scripts.Commands
{
    public class SendFireCommand : Command
    {
        [Inject] public SendFireCommandData FireCommandData { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendFire(FireCommandData.Origin, FireCommandData.Direction, FireCommandData.BarrelPosition);
        }
    }
}