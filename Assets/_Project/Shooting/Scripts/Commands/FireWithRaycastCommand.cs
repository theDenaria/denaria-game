using strange.extensions.command.impl;

namespace _Project.Shooting.Scripts.Commands
{
    public class FireWithRaycastCommand : Command
    {
        
        public override void Execute()
        {
            
        }
        
        /*public (Vector3, Vector3, Vector3) GetFireInput()
        {
            Vector3 screenPoint = new(Screen.width / 2, Screen.height / 2, 0);

            // Create a ray from the camera to the screen point
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            return (ray.origin, ray.direction, barrelPosition.position);
        }*/
    }
}