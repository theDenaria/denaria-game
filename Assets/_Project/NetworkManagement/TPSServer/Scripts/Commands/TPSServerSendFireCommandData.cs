using UnityEngine;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerSendFireCommandData
    {
        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 BarrelPosition { get; set; }

        public TPSServerSendFireCommandData(Vector3 origin, Vector3 direction, Vector3 barrelPosition)
        {
            Origin = origin;
            Direction = direction;
            BarrelPosition = barrelPosition;
        }
    }
}