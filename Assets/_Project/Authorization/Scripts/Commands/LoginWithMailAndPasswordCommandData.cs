namespace _Project.Authorization.Scripts.Commands
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