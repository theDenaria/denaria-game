using _Project.Authorization.Scripts.Commands;

namespace _Project.Authorization.Scripts.Services
{
    public interface IRegisterService
    {
        void RegisterWithMailAndPassword(RegisterWithMailAndPasswordCommandData registerWithMailAndPasswordCommandData);
    }
}