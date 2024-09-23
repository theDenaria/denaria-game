using strange.extensions.command.impl;
using UnityEngine;
using _Project.Utilities;

namespace _Project.ForceUpdate.Scripts.Controllers
{
    public class OpenPlayStorePageCommand : Command
    {
        public override void Execute()
        {
            Application.OpenURL(Constants.PLAY_STORE_PAGE_URL);
        }
    }
}