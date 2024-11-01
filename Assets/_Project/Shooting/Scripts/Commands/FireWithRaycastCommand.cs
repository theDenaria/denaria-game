using _Project.Shooting.Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class FireWithRaycastCommand : Command
    {
        [Inject] public IShootingMechanicService ShootingMechanicService { get; set; }
        public override void Execute()
        {
            ShootingMechanicService.Shoot();
        }
        
    }
}