using _Project.GameProgression.Scripts.Models;
using _Project.GameProgression.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.GameProgression.Scripts.Controllers
{
	public class SaveGameProgressCommand : Command
	{
		[Inject] public IGameProgressService GameProgressService { get; set; }

		[Inject] public GameProgress GameProgress { get; set; }

		public override void Execute()
		{
			GameProgressService.SaveGameProgress(GameProgress);
		}
	}
}