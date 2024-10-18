using strange.extensions.command.impl;
using UnityEngine;
using _Project.NetworkManagement.TPSServer.Scripts.Services;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerSendLookCommand : Command
    {
        [Inject] public Vector4 AxisAngles { get; set; }
        [Inject] public ITPSServerService TPSServerService { get; set; }

        public override void Execute()
        {
            TPSServerService.SendRotation(AxisAngles);
        }
    }
}