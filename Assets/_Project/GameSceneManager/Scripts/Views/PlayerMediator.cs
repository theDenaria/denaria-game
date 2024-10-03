using strange.extensions.mediation.impl;
using UnityEngine;
using _Project.GameSceneManager.Scripts.Signals;
using _Project.NetworkManagement.Scripts.Signals;
using strange.extensions.signal.impl;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class PlayerMediator : Mediator
    {
        [Inject] public PlayerView View { get; set; }


    }
}