using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using _Project.UIZeitnot.TextMeshProZeitnot;
using strange.extensions.signal.impl;
using System.Collections;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class UserIdCopyToClipboardButtonView : ViewZeitnot
	{
		[field: SerializeField] private ButtonZeitnot copyToClipboardButton;
		[field: SerializeField] private TextMeshProZeitnot playfabIdText;
		[field: SerializeField] private GameObject notificationImagePrefab;

		internal Signal onButtonClick = new Signal();
		internal Signal onViewRegistered = new Signal();

		internal void Init()
		{
			onViewRegistered.Dispatch();
		}

		private void OnEnable()
		{
			copyToClipboardButton.onClick.AddListener(OnButtonClick);
		}

		private void OnDisable()
		{
			copyToClipboardButton.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			onButtonClick.Dispatch();
			Instantiate(notificationImagePrefab, transform.parent);
		}

		public void SetPlayfabId(string playfabId)
		{
			this.playfabIdText.text = playfabId;
		}
	}
}