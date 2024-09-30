using _Project.SettingsManager.Scripts.Enums;

namespace _Project.SettingsManager
{
    public class ChangeSettingsCommandData
    {
        public SettingsEnum SettingsEnum { get; set; }
        public string Value { get; set; }
        public ChangeSettingsCommandData(SettingsEnum settingsEnum, string value)
        {
            SettingsEnum = settingsEnum;
            Value = value;
        }
    }
}