using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Models;
using Unity.Networking.Transport;
using UnityEngine;
using _Project.NetworkManagement.Scripts.Services;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class SendMoveCommand : Command
    {
        [Inject] public Vector2 MoveInput { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendMove(MoveInput);
        }
    }
}