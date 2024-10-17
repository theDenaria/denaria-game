using UnityEngine;
using UnityEngine.UI;
using TMPro;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using _Project.StrangeIOCUtility.Scripts.Views;

namespace _Project.Matchmaking.Scripts.Views
{
    public class ReadyCheckView : ViewZeitnot
    {
        public Canvas readyCheckCanvas;

        [field: SerializeField] public GameObject playerReadyCirclePrefab;
        [field: SerializeField] public Transform playerReadyCircleParent;

        [field: SerializeField] public Sprite readySprite;

        [field: SerializeField] private ButtonZeitnot readyButton;
        [field: SerializeField] private Image[] playerReadyCircles;

        internal Signal onReadyButtonClick = new Signal();

        private void OnEnable()
        {
            readyButton.onClick.AddListener(OnReadyButtonClick);
        }

        private void OnDisable()
        {
            readyButton.onClick.RemoveListener(OnReadyButtonClick);
        }

        public void ShowReadyCheckCanvas()
        {
            readyCheckCanvas.enabled = true;
        }

        public void HideReadyCheckCanvas()
        {
            readyCheckCanvas.enabled = false;
        }

        private void OnReadyButtonClick()
        {
            onReadyButtonClick.Dispatch();
        }

        public void DisableReadyButton()
        {
            readyButton.interactable = false;
            readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Waiting";
        }
    }
}
