using strange.extensions.context.impl;

namespace _Project.StrangeIOCUtility
{
	public class GameBootstrap : ContextView
	{
		void
			Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
			//context = new SignalMVCSContext(this);//TODO: Maybe reference it inside DontdestroyOnLoad

			context = FirstSceneRootSingletonPersistent.Instance.GetContext(this);
			//context.Start(); ///TODO: THIS CAUSES ERRORS BUT NEEDED.
		}

		protected override void OnDestroy()
		{
		}
	}
}