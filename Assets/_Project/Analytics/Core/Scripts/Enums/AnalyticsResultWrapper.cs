namespace _Project.Analytics.Core.Scripts.Enums
{
    public enum AnalyticsResultWrapper
    {
        /// <summary>
        ///   <para>Analytics API result: Success.</para>
        /// </summary>
        Ok = 0,
        /// <summary>
        ///   <para>Analytics API result: Analytics not initialized.</para>
        /// </summary>
        NotInitialized = 1,
        /// <summary>
        ///   <para>Analytics API result: Analytics is disabled.</para>
        /// </summary>
        AnalyticsDisabled = 2,
        /// <summary>
        ///   <para>Analytics API result: Too many parameters.</para>
        /// </summary>
        TooManyItems = 3,
        /// <summary>
        ///   <para>Analytics API result: Argument size limit.</para>
        /// </summary>
        SizeLimitReached = 4,
        /// <summary>
        ///   <para>Analytics API result: Too many requests.</para>
        /// </summary>
        TooManyRequests = 5,
        /// <summary>
        ///   <para>Analytics API result: Invalid argument value.</para>
        /// </summary>
        InvalidData = 6,
        /// <summary>
        ///   <para>Analytics API result: This platform doesn't support Analytics.</para>
        /// </summary>
        UnsupportedPlatform = 7,
    }
}