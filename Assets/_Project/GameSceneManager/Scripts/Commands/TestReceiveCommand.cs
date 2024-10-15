using strange.extensions.command.impl;

namespace _Project.GameSceneManager.Scripts.Commands
{
    public class TestReceiveCommand : Command
    {
        // [Inject] public IPlayerIdMapModel PlayerIdMapModel { get; set; }

        public override void Execute()
        {
            Debug.Log("UUU TestReceiveCommand");
        }
    }
}