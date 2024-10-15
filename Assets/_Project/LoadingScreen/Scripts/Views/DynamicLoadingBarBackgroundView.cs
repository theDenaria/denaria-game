using System.Collections.Generic;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ImageZeitnot;
using _Project.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _Project.LoadingScreen.Scripts.Views
{
	public class DynamicLoadingBarBackgroundView : ViewZeitnot
	{
		[field: SerializeField] private List<ImageZeitnot> backgroundImages = new List<ImageZeitnot>();

		private int backgroundIndex = 0;
		private float backgroundInterval;
		private float fadeDuration;
		private float timer = 0;

		protected override void Start() // Do not make init(), it causes bug
		{
			base.Start();
			
			backgroundInterval = Constants.BACKGROUND_DURATION;
			fadeDuration = Constants.FADE_DURATION;

			foreach (var image in backgroundImages)
			{
				image.color = new Color(1, 1, 1, 0);
			}

			backgroundImages[0].color = new Color(1, 1, 1, 1);
		}

		private void Update()
		{
			timer += Time.deltaTime;

			if (timer >= backgroundInterval)
			{
				timer = 0;
				ChangeBackground();
			}
		}

		public void ChangeBackground()
		{
			backgroundIndex++;

			backgroundIndex = backgroundIndex >= backgroundImages.Count ? 0 : backgroundIndex;

			for(int i = 0; i < backgroundImages.Count; i++)
			{
				if (backgroundIndex != i)
				{
					backgroundImages[i].DOFade(0, fadeDuration);
					continue;
				}

				backgroundImages[i].DOFade(1, fadeDuration);
			}
		}
	}
}