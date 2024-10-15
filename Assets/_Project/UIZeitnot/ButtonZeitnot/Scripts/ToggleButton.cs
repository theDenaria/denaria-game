using _Project.StrangeIOCUtility;
using System.Collections;
using System.Collections.Generic;
using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;

namespace _Project.UIZeitnot.ButtonZeitnot.Scripts
{
	public class ToggleButton : ViewZeitnot
	{
		[Header("References")]
		[SerializeField] private ButtonZeitnot button;
		[SerializeField] private Sprite onToggle;
		[SerializeField] private Sprite offToggle;

		public void SetToggleOn()
		{
			button.image.sprite = onToggle;
		}

		public void SetToggleOff()
		{
			button.image.sprite = offToggle;
		}
	}
}

