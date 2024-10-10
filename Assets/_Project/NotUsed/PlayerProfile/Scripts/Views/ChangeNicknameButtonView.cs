using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class ChangeNicknameButtonView : ViewZeitnot
	{
		private ButtonZeitnot Button { get; set; }

		internal Signal onButtonClickSignal = new Signal();

		protected override void Awake()
		{
			base.Awake();
			Button = GetComponent<ButtonZeitnot>();
		}

		private void OnEnable()
		{
			Button.onClick.AddListener(OnButtonClick);
		}

		private void OnDisable()
		{
			Button.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			onButtonClickSignal.Dispatch();
		}
	}
}