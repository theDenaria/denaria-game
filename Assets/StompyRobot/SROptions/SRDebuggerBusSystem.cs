using System;

namespace StompyRobot.SROptions
{
    public static class SRDebuggerBusSystem
    {
        //TODO: Discuss with the team, this class looks like unnecessary.
        public static Action OnClearDatabaseAndPlayerPrefsAndQuitGame;
        public static void CallOnClearDatabaseAndPlayerPrefsAndQuitGame() { OnClearDatabaseAndPlayerPrefsAndQuitGame?.Invoke(); }

        public static Action OnAddGemsToAccount;
        public static void CallOnAddGemsToAccount() { OnAddGemsToAccount?.Invoke(); }

        public static Action OnSubstractGemsFromAccount;
        public static void CallOnSubstractGemsFromAccount() { OnSubstractGemsFromAccount?.Invoke(); }

        public static Action OnSubstractAllGemsFromAccount;
        public static void CallOnSubstractAllGemsFromAccount() { OnSubstractAllGemsFromAccount?.Invoke(); }

        public static Action OnOpenEpisodeTestConfiguratorScene;
        public static void CallOpenEpisodeTestConfiguratorScene() { OnOpenEpisodeTestConfiguratorScene?.Invoke(); }

        public static Action onShowTimeGateButtons;
        public static void OnShowTimeGateButtons() { onShowTimeGateButtons?.Invoke(); }

        public static Action OnAddTestDayOffsetToCBS = delegate { };
        public static void CallAddTestDayOffsetToCBS() { OnAddTestDayOffsetToCBS.Invoke(); }

        public static Action OnResetAllLiveOpsCalendars = delegate { };
        public static void CallResetAllLiveOpsCalendars() { OnResetAllLiveOpsCalendars.Invoke(); }

    }
}