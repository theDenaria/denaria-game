using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Services;
using UnityEngine;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class SendFireCommand : Command
    {
        [Inject] public (Vector3, Vector3, Vector3) FireInput { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendFire(FireInput.Item1, FireInput.Item2, FireInput.Item3);
        }
    }
}