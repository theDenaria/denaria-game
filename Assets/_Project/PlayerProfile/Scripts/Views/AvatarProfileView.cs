using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ImageZeitnot;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Views
{
	public class AvatarProfileView : ViewZeitnot
	{
		[Header("References")]
		[SerializeField] private ImageZeitnot profilePicture;

		internal Signal onViewEnabled = new Signal();

		public void Init()
		{
			onViewEnabled.Dispatch();
		}

		public void SetAvatar(Sprite avatar)
		{
			profilePicture.sprite = avatar;
		}
	}
}