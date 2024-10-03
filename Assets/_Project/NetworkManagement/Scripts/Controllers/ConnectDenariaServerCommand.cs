using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class ConnectDenariaServerCommand : Command
    {
        private IDenariaServerService _denariaServerService;

        [Inject]
        public IDenariaServerService DenariaServerService
        {
            get
            {
                Debug.Log("UUU Get DenariaServerService");
                return _denariaServerService;
            }
            set
            {
                Debug.Log("UUU Set DenariaServerService");
                _denariaServerService = value;
            }
        }
        // private ConnectDenariaServerCommandData _connectDenariaServerCommandData;

        // [Inject]
        // public ConnectDenariaServerCommandData ConnectDenariaServerCommandData
        // {
        //     get
        //     {
        //         Debug.Log("UUU Get ConnectDenariaServerCommandData");
        //         return _connectDenariaServerCommandData;
        //     }
        //     set
        //     {
        //         Debug.Log("UUU Set ConnectDenariaServerCommandData");
        //         _connectDenariaServerCommandData = value;
        //     }
        // }


        public override void Execute()
        {
            Debug.Log("UUU ConnectDenariaServerCommand Execute");
            DenariaServerService.ConnectToDenariaServer("asdasd");

        }
    }
}