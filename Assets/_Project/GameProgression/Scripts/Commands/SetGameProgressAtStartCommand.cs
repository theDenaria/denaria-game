using _Project.GameProgression.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.GameProgression.Scripts.Commands
{
	public class SetGameProgressAtStartCommand : Command
	{
		[Inject] public IGameProgressService GameProgressService { get; set; }

		public override void Execute()
		{
			GameProgressService.SaveGameProgress(GameProgressService.GetGameProgress());
		}
	}
}