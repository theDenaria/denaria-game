using _Project.LoggingAndDebugging;
using strange.extensions.command.impl;

namespace _Project.ABTesting.Scripts.Commands
{
    public class SetTestGroupIdToUserPropertiesCommand : Command
    {
        [Inject] public int TestGroupId { get; set; }

        public override void Execute()
        {
            //Firebase.Analytics.FirebaseAnalytics.SetUserProperty("test_group_id", TestGroupId.ToString());
            DebugLoggerMuteable.Log("Test Group Id is set to user properties as: " + TestGroupId);
        }
    }
}