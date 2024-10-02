namespace _Project.Login.Controllers
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