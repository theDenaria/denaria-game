using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Commands
{
    public class PlayerRotationUpdateCommand : Command
    {
        [Inject] public PlayerRotationUpdateCommandData PlayerRotationUpdateCommandData { get; set; }
        [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerRotationUpdateCommandData.PlayerId))
            {
                // We do not need to update the own player's rotation, as it is updated locally
                return;
            }
            else
            {
                PlayerView player = PlayerIdMapModel.GetPlayerView(PlayerRotationUpdateCommandData.PlayerId);
                if (player != null)
                {
                    player.transform.rotation = new Quaternion(PlayerRotationUpdateCommandData.Rotation.x, PlayerRotationUpdateCommandData.Rotation.y, PlayerRotationUpdateCommandData.Rotation.z, PlayerRotationUpdateCommandData.Rotation.w);
                }
            }
        }
    }
}