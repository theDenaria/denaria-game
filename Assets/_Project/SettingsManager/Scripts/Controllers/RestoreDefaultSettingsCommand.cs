using strange.extensions.command.impl;
using _Project.SettingsManager.Scripts.Models;

public class RestoreDefaultSettingsCommand : Command
{
    [Inject]
    public SettingsModel settingsModel { get; set; }

    public override void Execute()
    {
        // Reset to default settings
        settingsModel = new SettingsModel();  // Resets all values
        settingsModel.Save();
        Debug.Log("Settings have been restored to default");
    }
}
