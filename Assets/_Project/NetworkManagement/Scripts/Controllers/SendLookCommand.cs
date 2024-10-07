using strange.extensions.command.impl;
using UnityEngine;
using _Project.NetworkManagement.Scripts.Services;

namespace _Project.NetworkManagement.Scripts.Controllers
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