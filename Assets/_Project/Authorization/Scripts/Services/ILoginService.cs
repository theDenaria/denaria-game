using _Project.Authorization.Scripts.Commands;

namespace _Project.Authorization.Scripts.Services
{
    public interface ILoginService
    {
        void LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData loginWithMailAndPasswordCommandData);
        void LoginWithDeviceId(LoginWithDeviceIdCommandData loginWithDeviceIdCommandData);
        void LoginWithCustomId(LoginWithCustomIdCommandData loginWithMailAndPasswordCommandData);
    }
}