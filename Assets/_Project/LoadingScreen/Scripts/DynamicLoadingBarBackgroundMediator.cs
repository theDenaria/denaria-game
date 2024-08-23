using strange.extensions.mediation.impl;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.LoadingScreen.Scripts
{
	public class DynamicLoadingBarBackgroundMediator : Mediator
	{
		[Inject] public DynamicLoadingBarBackgroundView View { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
		}

		public override void OnRemove()
		{
			base.OnRemove();
		}
	}
}