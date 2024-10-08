using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.NetworkManagement.Scripts.Commands
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