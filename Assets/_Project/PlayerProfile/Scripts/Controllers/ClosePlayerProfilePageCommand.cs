using _Project.SceneManagementUtilities.Signals;
using _Project.SceneManagementUtilities.Utilities;
using strange.extensions.command.impl;

namespace _Project.PlayerProfile.Scripts.Controllers
{
	public class ClosePlayerProfilePageCommand : Command
	{
		[Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }

		public override void Execute()
		{
			ChangeSceneGroupSignal.Dispatch(SceneGroupType.MainMenu, new LoadingOptions());
		}
	}
}