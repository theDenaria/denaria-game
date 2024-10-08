using _Project.StrangeIOCUtility.Scripts.Context;
using strange.extensions.context.impl;

namespace _Project.StrangeIOCUtility.Scripts.NotUsed
{
	public class GameBootstrapWithScreens : ContextView
	{

		void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
			context = new SignalMVCSContextWithScreens(this);
			//context.Start(); ///TODO: THIS CAUSES ERRORS BUT NEEDED.
		}
	}
}
		