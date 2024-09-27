using strange.extensions.command.impl;

namespace _Project.GameLifecycle.Scripts.Commands
{
	public class ApplicationPauseCommand : Command
	{
		[Inject] public bool Pause { get; set; }
		public override void Execute()
		{
		}
	}
}