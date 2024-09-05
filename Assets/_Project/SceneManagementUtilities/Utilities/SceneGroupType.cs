using System;

namespace _Project.SceneManagementUtilities.Utilities
{
    [Flags]
    public enum SceneGroupType
    {
        None = 0,
        Loading = 1,
        TermsOfService = 2,
        Teaser = 4,
        MainMenu = 8,//TODO: Maybe do binary addition. Consists shop, top bar, nav bar.
        PlayerProfile = 16,
        PlayerProfileEnterName = 32,
        PrivacyPolicy = 64,
        GameSettings = 128,
        DefaultLoadingScene = 256
    }

}