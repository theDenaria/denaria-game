using _Project.Login.Controllers;

namespace _Project.Login.Services
{
    public interface ILoginService
    {
        void LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData loginWithMailAndPasswordCommandData);
        void LoginWithDeviceId(LoginWithDeviceIdCommandData loginWithDeviceIdCommandData);
        void LoginWithCustomId(LoginWithCustomIdCommandData loginWithMailAndPasswordCommandData);
    }
}