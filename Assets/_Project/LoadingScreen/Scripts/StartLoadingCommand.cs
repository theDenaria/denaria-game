using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts
{
	public class StartLoadingCommand : Command
	{
		[Inject] public LoadingBarView loadingBarView { get; set; }

		public override void Execute()
		{
			loadingBarView.StartLoadingBar();
		}
	}
}