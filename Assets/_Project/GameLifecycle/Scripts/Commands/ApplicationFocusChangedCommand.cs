using strange.extensions.command.impl;

namespace _Project.GameLifecycle.Scripts.Commands
{
	public class ApplicationFocusChangedCommand : Command
	{
		[Inject] public bool Focus { get; set; }
		public override void Execute()
		{
		}
	}
}