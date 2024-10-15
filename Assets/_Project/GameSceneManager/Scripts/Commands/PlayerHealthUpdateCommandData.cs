namespace _Project.GameSceneManager.Scripts.Commands
{
    public class PlayerHealthUpdateCommandData
    {
        public string PlayerId { get; set; }
        public float Health { get; set; }

        public PlayerHealthUpdateCommandData(string playerId, float health)
        {
            PlayerId = playerId;
            Health = health;
        }
    }
}