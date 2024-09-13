using System.Collections.Generic;
using UnityEngine;

namespace _Project.Utilities
{
	public class Constants
	{
		// BEFORE STORE PUBLISH SET THIS TO TRUE !!!
		public const bool IS_PROD_VERSION = false;

		public static readonly string DEVICE_ID_KEY = "deviceUid";
		//public static readonly string INTRODUCTION_SCENE_NAME = "IntroductionScene";

		public const string MISCELLANEOUS_CONFIG = "MiscellaneousConfig";

		#region LOGS

		public const string TEST_BRANCH_BUILD = "TEST_BRANCH_BUILD";
		public const string UNITY_EDITOR = "UNITY_EDITOR";

		public static readonly string LOGS_PATH = Application.persistentDataPath
		                                          + "/GameLogs_"
		                                          //+ SystemInfo.deviceUniqueIdentifier 
		                                          //+ "_" 
		                                          + SystemInfo.deviceName
		                                          + "_"
		                                          + SystemInfo.deviceModel
		                                          + ".zip";

		#endregion

		#region GAME PROGRESS

		public const string GAME_LANGUAGE_ENGLISH = "English";

		#endregion

		#region CBS_KEY

		public const int RETRY_CONNECTION_DELAY_MS = 750;
		public const int RETRY_CONNECTION_MAX_ATTEMPT = 5;
		public const int RETRY_CONNECTION_DELAY_UPDATE_DATA_DELAY_MS = 250;

		public const string GAME_SETTINGS = "GameSettings";
		//public const string PLAYER_GAME_LANGUAGE = "PlayerGameLanguage";
		//public const string PLAYER_IS_ADULT_CONTENT_ENABLED = "PlayerIsAdultContentEnabled";
		//public const string PLAYER_IS_SOUND_ENABLED = "PlayerIsSoundEnabled";

		public const string PLAYER_GAME_PROGRESS = "PlayerGameProgress";

		public const string PLAYER_TERMS_OF_SERVICE_ACCEPTED = "TermsOfServiceAccepted";
		public const string PLAYER_ID = "PlayerId";
		public const string PLAYER_TEST_GROUP_ID = "PlayerTestGroupId";
		public const int TEST_GROUP_NOT_ASSIGNED = -1;

		public const string PLAYER_PROFILE = "PlayerProfile";
		//public const string PLAYER_DISPLAY_NAME = "PlayerDisplayName";
		//public const string PLAYER_AVATAR_INDEX = "PlayerAvatarIndex";

		public const string VALUE_IS_NOT_EXISTS = "ERROR_VALUE_ValueIsNotInstantiatedYet";

		public const string CURRENCY_CODE = "DR";

		#endregion

		#region SCREEN_TRACKING

		// Screen Ids
		public const string LOGO_SCENE = "game_logo_screen";
		public const string AGREEMENT_SCENE = "agreement_screen";
		public const string TERMS_OF_SERVICE_TEXT_SCENE = "terms_of_service_text";
		public const string PRIVACY_POLICY_TEXT_SCENE = "privacy_policy_text";
		public const string LOADING_SCENE = "loading_screen";
		public const string MAIN_MENU_SCENE = "main_menu_screen";
		public const string USER_PROFILE_SCENE = "user_profile_screen";
		public const string STORE_SCENE = "store_screen";
		public const string SETTING_SCENE = "setting_screen";
		public const string FORCE_UPDATE_SCENE = "force_update_popup_screen";

		// Screen change requests
		public const string AGREEMENT_SCENE_AGREE = "agree_button";
		public const string SCENE_COMPLETED = "scene_completed";
		public const string CONTINUE_BUTTON = "continue_button";
		public const string BACK_BUTTON = "back_button";
		public const string OUT_OF_PROFILE_GEM_BUTTONS = "out_of_profile_gem_buttons";
		public const string GEMS_BUTTON = "gem_buttons";
		public const string NAVIGATION_BAR_BUTTON = "navigation_bar_button";
		public const string FORCE_UPDATE_NEEDED = "force_update_needed";

		#endregion

		#region TransactionAnalytics

		public const string TRANSACTION_TYPE_GEM = "gem";
		public const string TRANSACTION_TYPE_CHEST_GEM = "chest_gem";

		public const string TRANSACTION_LINK_OUT_OF_PROFILE = "out_of_profile";
		public const string TRANSACTION_LINK_AD_REWARD = "ad_reward";
		public const string TRANSACTION_LINK_GEM_PACK = "gem_pack";
		public const string TRANSACTION_LINK_LIVE_OPS_CALENDAR_REWARD = "live_ops_calendar_reward";

		#endregion

		#region IAP

		public const string REAL_MONEY_CODE = "RM";

		public static int TIME_GATE_FAST_FORWARD_COST;
		public static float TRANSACTION_MAX_WAIT_TIME_SEC;

		public const string REAL_MONEY_UI_STRING_FORMAT = "{0:F2}$";

		#endregion

		#region ConnectionChannels

		public const string LOGIN_CONNECTION = "LoginConnectionChannel";
		
		#endregion

		#region DeviceStorageChecker

		public const float DEVICE_STORAGE_CHECK_INTERVAL_SEC = 10f;
		public const int INSUFFICIENT_STORAGE_SPACE_ALERT_THRESHOLD_MB = 512;
		public const float CLEAN_STORAGE_UP_BUTTON_SLEEP_TIME = 1f;

		#endregion

		#region ApplicationMemoryTracker

		public const long MEMORY_BYTE_TO_MB_DIVIDER = 1048576; // 1024^2
		public const float APPLICATION_MEMORY_CHECK_INTERVAL_SEC = 5f;

		public const long
			APPLICATION_MEMORY_CLEANING_COOLDOWN_MSEC = 15000; // To avoid memory cleaning process so often

		/// <summary>
		///	To avoid getting out of memory issues which will cause application crashes. The value varies on device type and device total memory size.
		/// </summary>
		public const long APPLICATION_CRITICAL_MEMORY_THRESHOLD_MB =
#if UNITY_EDITOR
			4000; // For Unity Editor (it already uses extra resources so we need more memory allocation space there)
#elif UNITY_ANDROID
			800;		// For Android devices. That value is fine for a 4GB+ memory owner device.
#elif UNITY_IOS
			800;		// TODO: We should test that value on IOS devices.
#else
			2000;		// For other standalone builds.
#endif

		#endregion

		#region AppleTracking

		public const string PLAYER_APPLE_TRACKING_AGREE = "PlayerAppleTrackingAgree";
		public const string SHOW_APPLE_TRACKING = "ShowAppleTracking";

		#endregion

		#region LOADING_BAR_BACKGROUND_CHANGE_SETTINGS
		
		public const float BACKGROUND_DURATION = 3;
		public const float FADE_DURATION = 0.25f;
		
		#endregion

		#region PUSH_NOTIFICATION

		public const string IS_PUSH_NOTIFICATIONS_ENABLED = "IsPushNotificationsEnabled";
		public const string IS_PUSH_NOTIFICATIONS_ASKED_NATIVELY = "IsPushNotificationsAskedNatively";

		///In case first open fails and value in CBS cannot be retrived, use this value
		public const int USER_CALLBACK_NOTIFICATION_TIME_IN_MINUTES = 1440;
		
		public const string USER_HAS_NOT_ENTERED_GAME_NOTIFICATION_TEXT = "Hey! We missed you so much!";
		
		#endregion

		#region CachedAnalyticEvent

		public static readonly string[] CACHE_EVENTS_PREF_KEYS =
		{
			"ScreenTrackingEventPlayerPrefKey",
			"NotEnoughCoinsLeftEventPlayerPrefKey",
			"NotificationPopupQuitEventPrefKey",
			"ForceUpdatePopupGameLeftEventPrefKey"
		};

		public static readonly string SCREEN_TRACKING_EVENT_PREF_KEY = CACHE_EVENTS_PREF_KEYS[0];
		public static readonly string NOT_ENOUGH_COINS_CANVAS_LEFT_EVENT_PREF_KEY = CACHE_EVENTS_PREF_KEYS[3];
		public static readonly string NOTIFICATION_POPUP_QUIT_EVENT_PREF_KEY = CACHE_EVENTS_PREF_KEYS[4];
		public static readonly string FORCE_UPDATE_POPUP_GAME_LEFT_EVENT_PREF_KEY = CACHE_EVENTS_PREF_KEYS[5];

		public const string NO_EVENT = "NoEvent";
		public static bool CACHED_EVENTS_SENT = false;

		#endregion

		#region AB_TESTING

		public const int TEST_GROUP_COUNT = 10;

		#endregion

		#region REWARDED_ADS

		public const float FREE_GEMS_REWARDED_AD_SLIDER_FILL_TIME = 1;

		public static string REWARDED_ADS_UNIT_ID;
		public const string AD_PLACEMENT = "AdPlacement";
		public const string FREE_GEMS_REWARDED_AD_LABEL = "FreeGems";

		public const string REWARDED_ADS_CONFIG = "RewardedAdsConfig";
		
		public const string FREE_GEMS_AD_REWARDS_SHOP_INDEX = "FreeGemsAdRewardsShopIndex";
		public const string IS_FREE_GEM_AD_REWARDS_IN_COOLDOWN = "IsFreeGemAdRewardsInCooldown";
		public const string FREE_GEM_AD_REWARDS_TARGET_AD_UNLOCK_TIME = "FreeGemAdRewardsTargetAdUnlockTime";

		public static List<int> FREE_GEMS_AD_REWARDS = new List<int>();
		public static int FREE_GEM_AD_REWARD_COOLDOWN_IN_MINUTES;

		#endregion

		#region ANIMATIONS

		#endregion

		#region Cloud Content Delivery

		public const double GET_CCD_CONFIGURATION_TIMEOUT = 1;

		#endregion

		#region STORE_PAGE_URLS

		public const string PLAY_STORE_PAGE_URL = "";

		public const string APPLE_STORE_PAGE_URL = "";
		public const string MINIMUM_CLIENT_VERSION_NEEDED_TITLE_DATA = "MinimumClientVersionNeeded";

		#endregion

		#region EXCLUDE_KEYS

		public static readonly string[] TEAM_KEYS = new[]
		{
			"",
		};

		public static readonly string DENARIA_TEAM_MEMBER_NAME = "DENARIA_TEAM_MEMBER";

		#endregion

		public struct GemAnimation
		{
			public const string GemPrefab = "Prefabs/Gem";
			public const int SmallAmountOfGems = 10;
			public const int MediumAmountOfGems = 20;
			public const int BigAmountOfGems = 30;
		}

		public struct ConfigDataFetcherController
		{
			public const int CheckControllerDelayMS = 100;
			public const string RewardedAdsConfigKey = "REWARDED_ADS_CONFIG_DATA";
			public const string MiscellaneousConfigKey = "MISCELLANEOUS_CONFIG_DATA";
			public const string StoreModelCollectionConfigKey = "STORE_MODEL_COLLECTION_CONFIG_DATA";
		}

		public struct LoginSucceedFetcherController
		{
			public const int CheckControllerDelayMS = 200;
			public const string StoreSpecialOfferInitializedKey = "STORE_SPECIAL_OFFER_INITIALIZED";
			public const string ConfigDataFetchCompleteKey = "CONFIG_DATA_FETCH_COMPLETE";
			public const string ServerTimeFetchCompleteKey = "SERVER_TIME_FETCH_COMPLETE";
			public const string PlayerDataFetchCompleteKey = "PLAYER_DATA_FETCH_COMPLETE";
			public const string CurrencyDataFetchCompleteKey = "CURRENCY_DATA_FETCH_COMPLETE";
			public const string UpdateLiveOpsCalendarKey = "UPDATE_LIVE_OPS_CALENDAR_COMPLETE";
		}

		public struct AddressableDataFetcherController
		{
			public const int CheckControllerDelayMS = 100;
			public const string CloudContentDeliveryInitializedKey = "CLOUD_CONTENT_DELIVERY_INITIALIZED";
			public const string ContentManagementServiceInitializedKey = "CONTENT_MANAGEMENT_SERVICE_INITIALIZED";
			public const string InitializePremiumOptionsModelCompleteKey = "INITIALIZE_PREMIUM_OPTIONS_MODEL_COMPLETE";
		}

		public struct Addressables
		{
			public const int CheckDownloadContentDependenciesProgressDelayMS = 100;
		}

		public struct AddressablesGroupLabelNames
		{
			
		}

		public struct GameStartDataFetcherController
		{
			public const int CheckControllerDelayMS = 200;
			public const string FirebaseProgressKey = "FIREBASE_PROGRESS_COMPLETED";
			public const string AddressableProgressKey = "ADDRESSABLE_PROGRESS_COMPLETED";
		}

		public struct NestedScriptableObject
		{
			public const string CUSTOM_NESTED_SCRIPTABLE_OBJECT_LOCATION =
				"Assets/_Project/Utilities/NestedScriptableObject/CustomNestedScriptableObjects";
		}
		
		public struct FirebaseInitialization
		{
			public const int MAXIMUM_RETRY_ATTEMPTS = 3;
			public const int DELAY_BETWEEN_ATTEMPTS_IN_MS = 200;
		}
		
	}
}
