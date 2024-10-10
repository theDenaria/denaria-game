using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.DeviceFrameRateLimiter.Scripts.Commands
{
    public class LimitDeviceFrameRateCommand : Command
    {
        private const int HighFrameRate = 60;
        private const int LowFrameRate = 30;

        public override void Execute()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = CalculateTargetFrameRate();
        }

        private int CalculateTargetFrameRate()
        {
            uint denominator = Screen.currentResolution.refreshRateRatio.denominator;
            // If denominator is zero, return a default frame rate
            if (denominator == 0)
            {
                Debug.LogWarning("Refresh rate denominator is zero. Defaulting to HighFrameRate.");
                return HighFrameRate;
            }

            uint screenRefreshRate = Screen.currentResolution.refreshRateRatio.numerator / denominator;

            return (screenRefreshRate >= HighFrameRate) ? HighFrameRate : LowFrameRate;
        }
    }
}