using _Project.SceneManagementUtilities;

namespace _Project.Login.Controllers
{
    public class LoginWithMailAndPasswordCommandData
    {
        public string Mail { get; set; }
        public string Password { get; set; }

        public LoginWithMailAndPasswordCommandData(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }
    }
}