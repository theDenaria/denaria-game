namespace _Project.Authorization.Scripts.Commands
{
    public class RegisterWithMailAndPasswordCommandData
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public RegisterWithMailAndPasswordCommandData(string mail, string password, string displayName)
        {
            Mail = mail;
            Password = password;
            DisplayName = displayName;
        }
    }
}