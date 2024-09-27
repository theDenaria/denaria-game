using strange.extensions.command.impl;
using _Project.SettingsManager.Scripts.Models;

public class ApplySettingsCommand : Command
{
    [Inject]
    public SettingsModel settingsModel { get; set; }

    public override void Execute()
    {
        // Apply the settings by saving them via the model
        settingsModel.Save();
        Debug.Log("Settings have been applied");
    }
}
