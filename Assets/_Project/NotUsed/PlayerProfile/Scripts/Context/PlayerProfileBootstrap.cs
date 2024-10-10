using strange.extensions.context.impl;

namespace _Project.PlayerProfile.Scripts.Context
{
	public class PlayerProfileBootstrap : ContextView
	{
		void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
			context = new PlayerProfileContext(this);
		}
	}
}