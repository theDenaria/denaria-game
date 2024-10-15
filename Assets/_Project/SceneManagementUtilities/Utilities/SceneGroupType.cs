using System;

namespace _Project.SceneManagementUtilities.Utilities
{
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
        AvatarSelection = 2056,
        Authorization = 4112,
        TownSquare = 8224,
        TPSGameLoadingScene = 16448,
        TownSquareLoadingScene = 32896,
    }

}