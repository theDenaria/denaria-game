using _Project.LoadingScreen.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts.Commands
{
	public class AdvanceLoadingBarCommand : Command
	{
		[Inject] public AdvanceLoadingBarSignal AdvanceLoadingBarSignal { get; set; }

		public override void Execute()
		{
			AdvanceLoadingBarSignal.Dispatch();
		}
	}
}