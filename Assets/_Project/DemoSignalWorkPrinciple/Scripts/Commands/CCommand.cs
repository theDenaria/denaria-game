using strange.extensions.command.impl;

namespace _Project.DemoSignalWorkPrinciple.Scripts.Commands
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