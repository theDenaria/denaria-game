using strange.extensions.command.impl;
using UnityEngine;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Commands
{
    public class DenariaServerSendMoveCommand : Command
    {
        [Inject] public Vector2 MoveInput { get; set; }
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.SendMove(MoveInput);
        }
    }
}