using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace _Project.StrangeIOCUtility
{
    public class StrangeIOCProjectRoot : ContextView
    {
        void Awake() //TODO: DOCUMENT SAYS THIS SHOULD BE START METHOD. HOWEVER, THIS SOLVES CONTEXT NOT FOUND PROBLEM ON START-UP. 
		{
            //Instantiate the context, passing it this instance.
            context = new SignalMVCSContext(this, ContextStartupFlags.MANUAL_MAPPING);
            context.Start ();

            //This is the most basic of startup choices, and probably the most common.
            //You can also opt to pass in ContextStartFlag options, such as:
            //
            //context = new MyFirstContext(this, ContextStartupFlags.MANUAL_MAPPING);
            //context = new MyFirstContext(this, ContextStartupFlags.MANUAL_MAPPING | ContextStartupFlags.MANUAL_LAUNCH);
            //
            //These flags allow you, when necessary, to interrupt the startup sequence.
        }
    }
}