using strange.extensions.command.impl;

namespace _Project.DemoSignalWorkPrinciple.Controllers
{
    public class CCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("C Command started");
            
            Debug.Log("C Command finished");
        }
    }
}