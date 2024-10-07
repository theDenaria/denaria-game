using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Controller
{
    public class TestReceiveCommand : Command
    {
        // [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            Debug.Log("UUU TestReceiveCommand");
        }
    }
}