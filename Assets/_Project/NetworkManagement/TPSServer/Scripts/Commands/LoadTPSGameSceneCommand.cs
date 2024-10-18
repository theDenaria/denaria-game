
using _Project.SceneManagementUtilities.Scripts.Signals;
using _Project.SceneManagementUtilities.Utilities;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class LoadTPSGameSceneCommand : Command
    {
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { get; set; }
        public override void Execute()
        {
            ChangeSceneGroupSignal.Dispatch(SceneGroupType.ThirdPersonShooterGame, new LoadingOptions());
        }
    }
}