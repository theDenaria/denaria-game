using strange.extensions.command.impl;
using _Project.NetworkManagement.TPSServer.Scripts.Services;
using UnityEngine;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerSendFireCommand : Command
    {
        [Inject] public TPSServerSendFireCommandData FireCommandData { get; set; }
        [Inject] public ITPSServerService TPSServerService { get; set; }

        public override void Execute()
        {
            TPSServerService.SendFire(FireCommandData.Origin, FireCommandData.Direction, FireCommandData.BarrelPosition);
        }
    }
}