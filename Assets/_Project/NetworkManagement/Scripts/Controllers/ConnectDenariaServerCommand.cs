using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class ConnectDenariaServerCommand : Command
    {

        [Inject]
        public IDenariaServerService DenariaServerService { get; set; }

        [Inject]
        public ConnectDenariaServerCommandData ConnectDenariaServerCommandData { get; set; }


        public override void Execute()
        {
            Debug.Log("UUU ConnectDenariaServerCommand Execute");
            DenariaServerService.Init("asdas");
            DenariaServerService.ConnectToDenariaServer();

        }
    }
}