/*using _Project.LoggingAndDebugging;
using strange.extensions.command.impl;
using _Project.Utilities;

namespace _Project.Analytics.Commands
{
	public class SetTestParameterToUserPropertiesCommand : Command
	{
		public override void Execute()
		{
			Firebase.Analytics.FirebaseAnalytics.SetUserProperty("is_prod_version", Constants.IS_PROD_VERSION == true ? "true" : "false");
			DebugLoggerMuteable.Log("is_prod_version is set to user properties as: " + Constants.IS_PROD_VERSION);
		}
	}
}*/