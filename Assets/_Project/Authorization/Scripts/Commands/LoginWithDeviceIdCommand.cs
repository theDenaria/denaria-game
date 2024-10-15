namespace _Project.Authorization.Scripts.Commands
{
    public class LoginWithDeviceIdCommand : BaseLoginCommand
    {
        [Inject] public LoginWithMailAndPasswordCommandData LoginWithMailAndPasswordCommandData { get; set; }

        public override void Execute()
        {
            
        }
    }
}