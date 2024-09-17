using strange.extensions.mediation.impl;

namespace _Project.LoadingScreen.Scripts
{
	public class DynamicIntroductionTextMediator : Mediator
	{
		[Inject] public DynamicIntroductionTextView View { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			View.init();
		}

		public override void OnRemove()
		{
			base.OnRemove();
		}
	}
}