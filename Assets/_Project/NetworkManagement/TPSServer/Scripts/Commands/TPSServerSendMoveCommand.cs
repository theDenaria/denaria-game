using strange.extensions.command.impl;
using UnityEngine;
using _Project.NetworkManagement.TPSServer.Scripts.Services;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerSendMoveCommand : Command
    {
        [Inject] public Vector2 MoveInput { get; set; }
        [Inject] public ITPSServerService TPSServerService { get; set; }

        public override void Execute()
        {
            TPSServerService.SendMove(MoveInput);
        }
    }
}