using System;

namespace _Project.SceneManagementUtilities.Utilities
{
    //You can make binary addition.
    [Flags]
    public enum SceneGroupType
    {
        None = 0,
        Boot = 1,
        Loading = 2,
        DefaultLoading = 4,
        MainMenu = 8,
        ThirdPersonShooterGame = 16,
        TermsOfService = 32,
        Teaser = 64,
        PlayerProfile = 128,
        PlayerProfileEnterName = 256,
        PrivacyPolicy = 512,
        GameSettings = 1028,
    }

}