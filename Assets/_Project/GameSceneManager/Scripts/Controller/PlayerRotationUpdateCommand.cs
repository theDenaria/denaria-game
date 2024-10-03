using _Project.GameSceneManager.Scripts.Models;
using _Project.GameSceneManager.Scripts.Views;
using _Project.NetworkManagement.Scripts.Enums;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.GameSceneManager.Scripts.Controller
{
    public class PlayerRotationUpdateCommand : Command
    {
        [Inject] public PlayerRotationUpdateCommandData PlayerRotationUpdateCommandData { get; set; }
        [Inject] public PlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            if (PlayerIdMapModel.IsOwnPlayer(PlayerRotationUpdateCommandData.PlayerId))
            {
                // We do not need to update the own player's rotation, as it is updated locally
                return;
            }
            else
            {
                PlayerView player = PlayerIdMapModel.GetPlayer(PlayerRotationUpdateCommandData.PlayerId);
                if (player != null)
                {
                    player.transform.rotation = new Quaternion(PlayerRotationUpdateCommandData.Rotation.x, PlayerRotationUpdateCommandData.Rotation.y, PlayerRotationUpdateCommandData.Rotation.z, PlayerRotationUpdateCommandData.Rotation.w);
                }
                else
                {
                    GameObject newPlayerObj = Object.Instantiate(PlayerIdMapModel.EnemyPlayerPrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
                    PlayerView newPlayer = newPlayerObj.GetComponent<PlayerView>();
                    newPlayer.SetPlayerId(PlayerRotationUpdateCommandData.PlayerId);
                    PlayerIdMapModel.AddPlayer(PlayerRotationUpdateCommandData.PlayerId, newPlayer);
                }
            }
        }
    }
}