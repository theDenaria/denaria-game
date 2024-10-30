using _Project.Shooting.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Shooting.Scripts.Commands
{
    public class StopShootingCommand : Command
    {
        [Inject] public IShootingMechanicService ShootingMechanicService { get; set; }
        
        public override void Execute()
        {
            
        }
    }
}