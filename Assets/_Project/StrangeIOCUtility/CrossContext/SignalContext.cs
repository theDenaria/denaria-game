using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace _Project.StrangeIOCUtility.CrossContext
{
	public class SignalContext : MVCSContext//This Context is used to bind the suggested SignalCommandBinder
                                            //instead of the default EventCommandBinder.
	{

		public SignalContext(MonoBehaviour view) : base(view)
		{
		}

		public SignalContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}

		// Unbind the default EventCommandBinder and rebind the SignalCommandBinder
		protected override void addCoreComponents()
		{
			base.addCoreComponents();
			injectionBinder.Unbind<ICommandBinder>();
			injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
		}

		protected override void mapBindings()
		{

		}
	}

}