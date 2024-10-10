using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.ForceUpdate.Scripts.Commands
{
    public class OpenPlayStorePageCommand : Command
    {
        public override void Execute()
        {
            Application.OpenURL(Constants.PLAY_STORE_PAGE_URL);
        }
    }
}