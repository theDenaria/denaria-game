using strange.extensions.context.impl;

namespace _Project.LoadingScreen.Scripts.Context
{
	public class LoadingBarBootstrap : ContextView
	{
		void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
			context = new LoadingBarContext(this);
		}
	}
}