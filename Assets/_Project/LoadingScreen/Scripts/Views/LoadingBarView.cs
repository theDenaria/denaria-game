using System.Collections;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.SliderZeitnot;
using _Project.UIZeitnot.TextMeshProZeitnot;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.LoadingScreen.Scripts.Views
{
	public class LoadingBarView : ViewZeitnot
	{
		[Header("References")] public TextMeshProZeitnot loadingTMP;
		public SliderZeitnot loadingSlider;
		[field: SerializeField] private TextMeshProZeitnot gameVersionText;
		internal Signal startLoadingSignal = new Signal();
		private float totalProgress = 0;
		private float currentProgress = 0;

		internal Signal temporaryLoadingBarCompletedSignal = new Signal();

		internal void init()
		{
			startLoadingSignal.Dispatch();
			gameVersionText.text = "V " + Application.version;
		}

		public void AdvanceLoadingBar(float amount)
		{
			totalProgress += amount;
		}

		/// Fake load method, need to delete and replace it later
		public void StartLoadingBar()
		{
			StartCoroutine(FakeLoadCoroutine());

			IEnumerator FakeLoadCoroutine()
			{
				float randomLoadingTime = UnityEngine.Random.Range(2f, 3f);
				float timer = 0;
				float loadSpeed;

				while (timer < randomLoadingTime)
				{
					if (timer < randomLoadingTime / 4)
					{
						loadSpeed = UnityEngine.Random.Range(1f, 2f);
						loadingTMP.text = "Starting load...";
					}
					else if (timer < randomLoadingTime / 2)
					{
						loadSpeed = UnityEngine.Random.Range(0.25f, 0.75f);
						loadingTMP.text = "Loading...";
					}
					else if (timer < randomLoadingTime * 2 / 3)
					{
						loadSpeed = UnityEngine.Random.Range(2f, 4f);
						loadingTMP.text = "More than halfway there...";
					}
					else
					{
						loadSpeed = UnityEngine.Random.Range(0.25f, 0.75f);
						loadingTMP.text = "Finishing up...";
					}

					loadSpeed /= 1.5f;

					float loadingValue = Mathf.Clamp(timer / randomLoadingTime, 0, 0.9f);
					loadingSlider.value = loadingValue;

					timer += UnityEngine.Time.deltaTime * loadSpeed;

					yield return null;
				}
    
				loadingTMP.text = "Content is now unpacking...";
				temporaryLoadingBarCompletedSignal.Dispatch();

			}
		}
	}
}