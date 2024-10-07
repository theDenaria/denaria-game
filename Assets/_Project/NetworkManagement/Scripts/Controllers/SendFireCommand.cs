using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Services;
using UnityEngine;

namespace _Project.NetworkManagement.Scripts.Controllers
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