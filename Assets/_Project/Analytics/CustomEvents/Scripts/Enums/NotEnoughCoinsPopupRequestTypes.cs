namespace _Project.Analytics.CustomEvents.Scripts.Enums
{
    public enum NotEnoughCoinsPopupRequestTypes
    {
        /// <summary>
        ///     Popup closed by tapping the close (x) button
        /// </summary>
        close_button_tapped = 0,

        /// <summary>
        ///     Player tapped a purchase button
        /// </summary>
        purchase_button_tapped = 1,

        /// <summary>
        ///     Player quit game while popup was visible
        /// </summary>
        quit_game = 2
    }
}
