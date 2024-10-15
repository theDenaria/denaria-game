using System.Threading.Tasks;
using strange.extensions.command.impl;

namespace _Project.DemoSignalWorkPrinciple.Scripts.Commands
{
    public class BCommand : Command
    {
        public async override void Execute()
        {
            Debug.Log("B Command started");

            await Task.Delay(1000);
            
            Debug.Log("B Command finished");
        }
    }
}