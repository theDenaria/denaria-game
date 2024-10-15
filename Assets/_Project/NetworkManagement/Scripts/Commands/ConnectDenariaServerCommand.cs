using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.Scripts.Commands
{
    public class ConnectDenariaServerCommand : Command
    {

        [Inject]
        public IDenariaServerService DenariaServerService { get; set; }

        [Inject]
        public ConnectDenariaServerCommandData ConnectDenariaServerCommandData { get; set; }


        public override void Execute()
        {
            DenariaServerService.Init(ConnectDenariaServerCommandData.PlayerId, ConnectDenariaServerCommandData.SessionTicket);
            DenariaServerService.ConnectToDenariaServer();
        }
    }
}