
namespace _Project.PlayerProfile.Scripts.Services
{
	public interface IPlayerProfileService
	{
		public void SetModelAtStart();
		public void SavePlayerNickname(string nickname);
		public void SaveTestGroupId(int id);
		public int GetTestGroupId();
		public string GetPlayerName();
		public void SetPlayerProfileId(string id);
		public string GetPlayerProfileId();
	}
}

