using _Project.LoadingScreen.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.LoadingScreen.Scripts.Commands
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