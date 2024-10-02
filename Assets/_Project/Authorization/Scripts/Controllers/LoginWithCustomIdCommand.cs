namespace _Project.Login.Controllers
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