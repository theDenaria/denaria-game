namespace _Project.Authorization.Scripts.Commands
{
    public class LoginWithCustomIdCommand : BaseLoginCommand
    {
        [Inject] public LoginWithCustomIdCommandData LoginWithCustomIdCommandData { get; set; }
        
        public override void Execute()
        {
            LoginService.LoginWithCustomId(LoginWithCustomIdCommandData);
        }
    }
}