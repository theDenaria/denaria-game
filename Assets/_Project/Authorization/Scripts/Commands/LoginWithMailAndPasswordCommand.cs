namespace _Project.Authorization.Scripts.Commands
{
    public class LoginWithMailAndPasswordCommand : BaseLoginCommand
    {
        [Inject] public LoginWithMailAndPasswordCommandData LoginWithMailAndPasswordCommandData { get; set; }
        
        public override void Execute()
        {
            LoginService.LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData);
        }
    }
}