using _Project.GameProgression.Scripts.Models;

namespace _Project.GameProgression.Scripts.Services
{
	public interface IGameProgressService
	{
		public void SaveGameProgress(GameProgress progress);
		public GameProgress GetGameProgress();
	}
}