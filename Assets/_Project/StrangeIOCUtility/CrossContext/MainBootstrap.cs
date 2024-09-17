using strange.extensions.context.impl;
using UnityEngine;

namespace _Project.StrangeIOCUtility.CrossContext
{
	[DefaultExecutionOrder(-1)]
	public class MainBootstrap : ContextView
	{
		void
			Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
			DontDestroyOnLoad(gameObject);
			context = new MainContext(this);
		}
	}

}