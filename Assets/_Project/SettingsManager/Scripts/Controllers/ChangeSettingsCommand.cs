using _Project.SettingsManager.Scripts.Enums;
using _Project.SettingsManager.Scripts.Models;
using strange.extensions.command.impl;

namespace _Project.SettingsManager.Scripts.Controllers
{
    public class ChangeSettingsCommand : Command
    {
        [Inject] public ISettingsModel Model { get; set; }
        [Inject] public ChangeSettingsCommandData Data { get; set; }
        public override void Execute()
        {
            switch (Data.SettingsEnum)
            {
                case SettingsEnum.Resolution:
                    Model.ResolutionIndex = float.Parse(Data.Value);
                    break;
                case SettingsEnum.Fullscreen:
                    Model.IsFullscreen = bool.Parse(Data.Value);
                    break;
                case SettingsEnum.Quality:
                    Model.QualityLevel = int.Parse(Data.Value);
                    break;
                case SettingsEnum.MasterVolume:
                    Model.MasterVolume = float.Parse(Data.Value);
                    break;
                case SettingsEnum.GameSoundVolume:
                    Model.GameSoundVolume = float.Parse(Data.Value);
                    break;
                case SettingsEnum.MenuMusicVolume:
                    Model.MenuMusicVolume = float.Parse(Data.Value);
                    break;
                case SettingsEnum.SoundDevice:
                    Model.SoundDevice = Data.Value;
                    break;
                case SettingsEnum.MoveForwardKey:
                    Model.MoveForwardKey = Data.Value;
                    break;
                case SettingsEnum.MoveBackwardKey:
                    Model.MoveBackwardKey = Data.Value;
                    break;
                case SettingsEnum.MoveLeftKey:
                    Model.MoveLeftKey = Data.Value;
                    break;
                case SettingsEnum.MoveRightKey:
                    Model.MoveRightKey = Data.Value;
                    break;
                case SettingsEnum.JumpKey:
                    Model.JumpKey = Data.Value;
                    break;
                case SettingsEnum.CrouchKey:
                    Model.CrouchKey = Data.Value;
                    break;
            }
        }
    }
}

