namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Controller
{
    public class PlayerAmmoUpdateCommandData
    {
        public string PlayerId { get; set; }
        public int CurrentAmmo { get; set; }
        public int TotalAmmo { get; set; }

        public PlayerAmmoUpdateCommandData(string playerId, int currentAmmo, int totalAmmo)
        {
            PlayerId = playerId;
            CurrentAmmo = currentAmmo;
            TotalAmmo = totalAmmo;
        }
    }
}