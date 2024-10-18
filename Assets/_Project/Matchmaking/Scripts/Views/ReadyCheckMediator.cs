using System.Collections.Generic;
using _Project.Matchmaking.Scripts.Commands;
using _Project.Matchmaking.Scripts.Signals;
using _Project.PlayerSessionInfo.Scripts.Models;
using strange.extensions.mediation.impl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Matchmaking.Scripts.Views
{
    public class ReadyCheckMediator : Mediator
    {
        [Inject] public ReadyCheckView View { get; set; }

        [Inject] public IPlayerSessionInfoModel PlayerSessionInfoModel { get; set; }

        [Inject] public ReadyButtonClickedSignal ReadyButtonClickedSignal { get; set; }

        private Dictionary<string, GameObject> playerReadyCircleDict = new Dictionary<string, GameObject>();

        public override void OnRegister()
        {
            base.OnRegister();
            View.onReadyButtonClick.AddListener(HandleOnReadyButtonClick);

        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onReadyButtonClick.RemoveListener(HandleOnReadyButtonClick);
        }

        public void InitializeReadyCheckCanvas(string[] playerId)
        {
            var startingPosX = 0f;

            if (playerId.Length % 2 == 0)
            {
                startingPosX = ((playerId.Length / 2 * 30) - 15) * -1f;
            }
            else
            {
                startingPosX = (((playerId.Length + 1) / 2 * 30) - 15) * -1f;
            }

            for (int i = 0; i < playerId.Length; i++)
            {
                GameObject playerReadyCircle = Instantiate(View.playerReadyCirclePrefab, View.playerReadyCircleParent);
                playerReadyCircle.transform.localPosition = new Vector3(startingPosX + (i * 30), 0, 0);
                playerReadyCircleDict.Add(playerId[i], playerReadyCircle);
            }
        }

        [ListensTo(typeof(MatchFoundSignal))]
        public void HandleMatchFoundSignal(MatchFoundCommandData matchFoundCommandData)
        {
            // pass player ids under players
            var playerIds = matchFoundCommandData.Players.ConvertAll(p => p.player_id).ToArray();
            InitializeReadyCheckCanvas(playerIds);
            View.ShowReadyCheckCanvas();
        }

        [ListensTo(typeof(PlayerReadyMessageReceivedSignal))]
        public void HandlePlayerReadyMessageReceivedSignal(string playerId)
        {
            playerReadyCircleDict[playerId].GetComponent<Image>().sprite = View.readySprite;
            if (playerId == PlayerSessionInfoModel.PlayerId)
            {
                View.DisableReadyButton();
            }
        }

        private void HandleOnReadyButtonClick()
        {
            ReadyButtonClickedSignal.Dispatch();
        }

    }
}