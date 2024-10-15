using System.Collections.Generic;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.TextMeshProZeitnot;
using UnityEngine;

namespace _Project.LoadingScreen.Scripts.Views
{
	public class DynamicIntroductionTextView : ViewZeitnot
	{
		[Header("References")] public TextMeshProZeitnot introText;

		[Header("Settings")] public List<string> introTexts = new List<string>();
		public float durationForEachText;

		private int Index { get; set; }

		internal void init()
		{
			InvokeRepeating("SetText", 0, durationForEachText);
		}

		private void SetText()
		{
			introText.text = introTexts[Index % introTexts.Count];
			Index++;
		}
	}
}