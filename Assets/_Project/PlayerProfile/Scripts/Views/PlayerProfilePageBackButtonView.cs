using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class PlayerProfilePageBackButtonView : ViewZeitnot
	{
		[Header("References")]
		[SerializeField] private ButtonZeitnot profilePageButton;

		internal Signal onButtonClick = new Signal();

		private void OnEnable()
		{
			profilePageButton.onClick.AddListener(OnButtonClick);
		}

		private void OnDisable()
		{
			profilePageButton.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			onButtonClick.Dispatch();
		}
	}
}