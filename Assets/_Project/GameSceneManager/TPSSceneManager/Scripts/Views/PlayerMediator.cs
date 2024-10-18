using strange.extensions.mediation.impl;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Views
{
    public class PlayerMediator : Mediator
    {
        [Inject] public PlayerView View { get; set; }


    }
}