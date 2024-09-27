using System;

namespace _Project.GameProgression.Scripts.Models
{
	[Serializable]
	public class GameProgressionModel : IGameProgressionModel
	{
		public GameProgress GameProgress { get; set; }
	}
}