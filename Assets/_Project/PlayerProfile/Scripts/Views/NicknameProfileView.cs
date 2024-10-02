using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.TextMeshProZeitnot;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class NicknameProfileView : ViewZeitnot
	{
		[Header("References")]
		[SerializeField] private TextMeshProZeitnot nicknameTMP;

		internal Signal onViewEnabled = new Signal();

		public void Init()
		{
			onViewEnabled.Dispatch();
		}

		public void SetNickname(string nickname)
		{
			nicknameTMP.text = nickname;
		}
	}
}