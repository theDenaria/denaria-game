using _Project.ABTesting.Scripts.Signals;
//using _Project.PlayerProfile.Scripts.Services;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.ABTesting.Scripts.Commands
{
    public class SetTestGroupIdAtStartCommand : Command
    {
        //[Inject] public IPlayerProfileService PlayerProfileService { get; set; }
        [Inject] public TestGroupIdSetSignal TestGroupIdSetSignal { get; set; }

        public override void Execute()
        {
            return;
            /*int testGroupId = PlayerProfileService.GetTestGroupId();
            if (testGroupId == Constants.TEST_GROUP_NOT_ASSIGNED)
            {
                PlayerProfileService.SaveTestGroupId(Random.Range(1, Constants.TEST_GROUP_COUNT));
            }
            else
            {
                TestGroupIdSetSignal.Dispatch(testGroupId);
            }*/
        }
    }
}