using System;

namespace _Project.PlayerProfile.Scripts.Models
{
	[Serializable]
	public class PlayerProfileModel : IPlayerProfileModel
	{
		public string Name { get; set; }
		public string Id { get; set; }
	}
}