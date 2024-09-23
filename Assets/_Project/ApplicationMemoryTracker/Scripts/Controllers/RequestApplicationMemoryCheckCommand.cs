using _Project.ApplicationMemoryTracker.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.ApplicationMemoryTracker.Controllers
{
    public class RequestApplicationMemoryCheckCommand : Command
    {
        [Inject] public IApplicationMemoryService ApplicationMemoryService { get; set; }

        public override void Execute()
        {
            ApplicationMemoryService.RequestMemoryCheck();
        }
    }
}