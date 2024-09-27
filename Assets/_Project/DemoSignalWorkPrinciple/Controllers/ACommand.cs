using strange.extensions.command.impl;

namespace _Project.DemoSignalWorkPrinciple.Controllers
{
    public class ACommand : Command
    {
        public override void Execute()
        {
            Debug.Log("A Command started");
            
            Debug.Log("A Command finished");
        }
    }
}