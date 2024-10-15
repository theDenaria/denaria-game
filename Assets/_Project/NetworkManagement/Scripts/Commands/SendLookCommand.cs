using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.NetworkManagement.Scripts.Commands
{
    public class SendLookCommand : Command
    {
        [Inject] public Vector4 AxisAngles { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendRotation(AxisAngles);
        }
    }
}