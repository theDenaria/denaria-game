using _Project.StrangeIOCUtility.Scripts.Context;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Context
{
	public class EnterNameSceneContext : SignalContext
	{
		public EnterNameSceneContext(MonoBehaviour view) : base(view)
		{
		}

		protected override void mapBindings()
		{
			base.mapBindings();

			BindEnterNameScene();
		}

		private void BindEnterNameScene()
		{
			//mediationBinder.Bind<NameInputFieldView>().To<NameInputFieldMediator>();

			//commandBinder.Bind<SetNameSignal>().To<SetNameCommand>();
		}
	}
}