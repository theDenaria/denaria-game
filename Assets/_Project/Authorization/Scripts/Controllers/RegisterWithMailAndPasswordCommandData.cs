namespace _Project.Login.Controllers
{
    public class RegisterWithMailAndPasswordCommandData
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        RegisterWithMailAndPasswordCommandData(string mail, string password, string displayName)
        {
            Mail = mail;
            Password = password;
            DisplayName = displayName;
        }
    }
}