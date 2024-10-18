using strange.extensions.command.impl;
using UnityEngine;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Commands
{
    public class DenariaServerSendLookCommand : Command
    {
        [Inject] public Vector4 AxisAngles { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendRotation(AxisAngles);
        }
    }
}