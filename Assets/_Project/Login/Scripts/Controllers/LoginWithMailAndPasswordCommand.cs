namespace _Project.Login.Controllers
{
    public class LoginWithMailAndPasswordCommand : BaseLoginCommand
    {
        public override void Execute()
        {
            LoginService.LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData);
        }
    }
}