//using _Project.CBSUtility;
//using _Project.CBSUtility.Models;
//using _Project.CBSUtility.Signals;
using _Project.GameProgression.Scripts.Models;
using _Project.LoggingAndDebugging;
using _Project.Utilities;

namespace _Project.GameProgression.Scripts.Services
{
	public class GameProgressService : IGameProgressService
	{
		[Inject] public IGameProgressionModel GameProgressionModel { get; set; }
		//[Inject] public ICustomProfileData CustomProfileData { get; set; }
		//[Inject] public UpdateProfileDataSignal UpdateProfileDataSignal { get; set; }

		public GameProgress GetGameProgress()
		{
			return new GameProgress();
			/*string gameProgressString = CustomProfileData.ProfileDataDictionary[Constants.PLAYER_GAME_PROGRESS];
			
			
			if (gameProgressString.Equals(Constants.VALUE_IS_NOT_EXISTS))
			{
				gameProgressString = "0";
			}
			else
			{
				GameProgressionModel = JSONUtilityZeitnot.TryDeserializeObject<IGameProgressionModel>(gameProgressString);
			}
			
			DebugLoggerMuteable.Log("GameProgressionModel is fetched via GetGameProgress, and it was: " + GameProgressionModel.GameProgress);

			return GameProgressionModel.GameProgress;*/
		}
		
		public void SaveGameProgress(GameProgress progress)
		{
			return;
			
			/*
			//TO PREVENT OVERRIDING FIRST TIME LOGIN PROCESS BY REPETETIVE SCENES
			if ((int)progress <= (int)GetGameProgress())
			{
				GameProgressionModel.GameProgress = GetGameProgress();
				return;
			}

			GameProgressionModel.GameProgress = progress;
			
			string gameProgressModelJSON = JSONUtilityZeitnot.TrySerializeObject(GameProgressionModel, typeof(IGameProgressionModel));
			Debug.Log("JSON of GameProgressModel is:" + gameProgressModelJSON);	
			CustomProfileData.ProfileDataDictionary[Constants.PLAYER_GAME_PROGRESS] = ((int)progress).ToString();
			
			DebugLoggerMuteable.Log("GameProgressModel will be saved as: " + gameProgressModelJSON);

			UpdateProfileDataCommandData profileData = new UpdateProfileDataCommandData(Constants.PLAYER_GAME_PROGRESS, gameProgressModelJSON);
			UpdateProfileDataSignal.Dispatch(profileData);
			
			GameProgressionModel.GameProgress = progress;
			*/
		}
		
	}
}