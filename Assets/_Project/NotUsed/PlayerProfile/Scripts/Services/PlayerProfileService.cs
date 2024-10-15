using System.Collections.Generic;
using _Project.ABTesting.Scripts.Models;
using _Project.ABTesting.Scripts.Signals;
//using _Project.CBSUtility;
//using _Project.CBSUtility.Models;
//using _Project.CBSUtility.Signals;
using _Project.LoggingAndDebugging;
using _Project.PlayerProfile.Scripts.Models;
using _Project.Utilities;
using Sirenix.Utilities;

namespace _Project.PlayerProfile.Scripts.Services
{
	public class PlayerProfileService : IPlayerProfileService
	{
		[Inject] public IPlayerProfileModel PlayerProfileModel { get; set; }

		[Inject] public ITestGroupIdModel TestGroupIdModel { get; set; }
		[Inject] public TestGroupIdSetSignal TestGroupIdSetSignal { get; set; }
		//[Inject] public UpdateProfileDataSignal UpdateProfileDataSignal { get; set; }//TODO: Uncommment 27 Sept
		//[Inject] public ICustomProfileData CustomProfileData { get; set; }//TODO: Uncommment 27 Sept
		
		public void SetModelAtStart()
		{
			return;
			/*
			string playerProfileString = CustomProfileData.ProfileDataDictionary[Constants.PLAYER_PROFILE];
			if (playerProfileString.Equals(Constants.VALUE_IS_NOT_EXISTS) ||
			    playerProfileString.IsNullOrWhitespace())
			{
				PlayerProfileModel.Name = "Name";
			}
			else
			{
				PlayerProfileModel = JSONUtilityZeitnot.TryDeserializeObject<IPlayerProfileModel>(playerProfileString);
			}*/
		}

		public void SavePlayerNickname(string nickname)//TODO: Reformat Database of Genres Nicknames and Avatars
		{
			return;
			/*
			PlayerProfileModel.Name = nickname;
			
			string playerProfileModelJSON = JSONUtilityZeitnot.TrySerializeObject(PlayerProfileModel, typeof(IPlayerProfileModel));
			
			CustomProfileData.ProfileDataDictionary[Constants.PLAYER_PROFILE] = playerProfileModelJSON;

			UpdateProfileDataCommandData updateProfileDataCommandData = new UpdateProfileDataCommandData(Constants.PLAYER_PROFILE, playerProfileModelJSON);
			
			UpdateProfileDataSignal.Dispatch(updateProfileDataCommandData);*/
		}
		
		public void SaveTestGroupId(int testGroupId)
		{
			return;
			/*TestGroupIdModel.TestGroupId = testGroupId;	
		
			DebugLoggerMuteable.Log("xxx SaveTestGroupId() - 0");
			string testGroupIdModelJSON = JSONUtilityZeitnot.TrySerializeObject(TestGroupIdModel, typeof(ITestGroupIdModel));
			DebugLoggerMuteable.Log("xxx SaveTestGroupId() - 1");
			CustomProfileData.ProfileDataDictionary[Constants.PLAYER_TEST_GROUP_ID] = testGroupIdModelJSON;
			DebugLoggerMuteable.Log("xxx SaveTestGroupId() - 2");

            DebugLoggerMuteable.Log("xxx testGroupIdModelJSON: " + testGroupIdModelJSON);
            UpdateProfileDataCommandData updateProfileDataSignalData = new UpdateProfileDataCommandData(Constants.PLAYER_TEST_GROUP_ID, testGroupIdModelJSON);
			UpdateProfileDataSignal.Dispatch(updateProfileDataSignalData);
			TestGroupIdSetSignal.Dispatch(TestGroupIdModel.TestGroupId);
			DebugLoggerMuteable.Log("xxx SaveTestGroupId() - 3");*/
		}
		
		public int GetTestGroupId()
		{
			return -1;
			/*
			string testGroupIdString;

			if (!CustomProfileData.ProfileDataDictionary.TryGetValue(Constants.PLAYER_TEST_GROUP_ID,
				    out testGroupIdString))
			{
				DebugLoggerMuteable.Log("xxx GetTestGroupId() - 0");
				return Constants.TEST_GROUP_NOT_ASSIGNED;
			}
			
			if (!string.IsNullOrEmpty(testGroupIdString) && !testGroupIdString.Equals(Constants.VALUE_IS_NOT_EXISTS) && !testGroupIdString.IsNullOrWhitespace())
			{
				DebugLoggerMuteable.Log("xxx GetTestGroupId() - 1");
				TestGroupIdModel = JSONUtilityZeitnot.TryDeserializeObject<ITestGroupIdModel>(testGroupIdString);
				DebugLoggerMuteable.Log("xxx GetTestGroupId() - 2");

                DebugLoggerMuteable.Log("xxx testGroupIdString: " + testGroupIdString);
                if (TestGroupIdModel != null)
				{
					DebugLoggerMuteable.Log("xxx GetTestGroupId() - 3");
					return TestGroupIdModel.TestGroupId;
				}
			}

			DebugLoggerMuteable.Log("xxx GetTestGroupId() - 4");
			return Constants.TEST_GROUP_NOT_ASSIGNED;
			*/
		}
		
		public string GetPlayerName()
		{
			return PlayerProfileModel.Name;
		}
		
		public void SetPlayerProfileId(string id)
		{
			PlayerProfileModel.Id = id;
		}
		
		public string GetPlayerProfileId()
		{
			return PlayerProfileModel.Id;
		}
	}
}

