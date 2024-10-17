using System.Collections;
using _Project.Matchmaking.Scripts.Enums;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Matchmaking.Scripts.Views
{
    public class MatchmakingMenuView : ViewZeitnot
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private Toggle normalGameToggle;
        [SerializeField] private Toggle rankedGameToggle;
        [SerializeField] private ButtonZeitnot playButton;
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private TextMeshProUGUI queueTimerText;

        internal Signal onSearchButtonClick = new Signal();
        internal Signal onCancelButtonClick = new Signal();

        private bool _isMatchmaking = false;
        private float _elapsedTime = 0f;
        private Coroutine _timerCoroutine;

        public MatchmakingPlatformEnum CurrentPlatform { get; private set; } = MatchmakingPlatformEnum.None;

        protected override void Start()
        {
            base.Start();
            HideMatchmakingMenu();
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnSearchButtonClick);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnSearchButtonClick);
        }


        public void ShowMatchmakingMenu(MatchmakingPlatformEnum platform)
        {
            CurrentPlatform = platform;
            canvas.enabled = true;
        }

        public void HideMatchmakingMenu()
        {
            canvas.enabled = false;
            UpdatePlayButton("SEARCH");
        }

        private void OnSearchButtonClick()
        {
            if (!_isMatchmaking)
            {
                onSearchButtonClick.Dispatch();
                _isMatchmaking = true;
            }
            else
            {
                onCancelButtonClick.Dispatch();
                _isMatchmaking = false;
            }
        }

        public void StartMatchmaking()
        {
            _isMatchmaking = true;
            UpdatePlayButton("CANCEL");
            // Show the timer and start it
            queueTimerText.gameObject.SetActive(true);
            _elapsedTime = 0f;
            _timerCoroutine = StartCoroutine(UpdateTimer());
        }

        public void CancelMatchmaking()
        {
            _isMatchmaking = false;
            UpdatePlayButton("SEARCH");

            // Stop the timer and hide it
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            queueTimerText.gameObject.SetActive(false);
            queueTimerText.text = "Queue Time: 00:00";  // Reset the timer display
        }

        private void UpdatePlayButton(string newLabel)
        {
            playButtonText.text = newLabel;
        }

        private IEnumerator UpdateTimer()
        {
            while (_isMatchmaking)
            {
                _elapsedTime += Time.deltaTime;
                int minutes = Mathf.FloorToInt(_elapsedTime / 60F);
                int seconds = Mathf.FloorToInt(_elapsedTime % 60F);
                queueTimerText.text = string.Format("Queue Time: {0:00}:{1:00}", minutes, seconds);

                yield return null;  // Wait for the next frame
            }
        }
    }
}