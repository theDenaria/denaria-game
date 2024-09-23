using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts
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