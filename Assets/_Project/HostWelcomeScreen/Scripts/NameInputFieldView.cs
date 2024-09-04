using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using _Project.UIZeitnot.TextMeshProZeitnot;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.HostWelcomeScreen.Scripts
{
	public class NameInputFieldView : ViewZeitnot
	{
		[Header("References")] [SerializeField]
		private TextMeshProInputFieldZeitnot inputField;

		[SerializeField] private ButtonZeitnot submitNameButton;

		internal Signal<string> EnterNameSignal = new Signal<string>();

		internal void init()
		{
			submitNameButton.onClick.AddListener(SubmitName);
		}

		private void SubmitName()
		{
			if (inputField.text.Length <= 0)
			{
				return;
			}

			EnterNameSignal.Dispatch(inputField.text);
			gameObject.SetActive(false);
		}
	}
}