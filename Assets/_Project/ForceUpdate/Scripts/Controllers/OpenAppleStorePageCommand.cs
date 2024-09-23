using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.ForceUpdate.Scripts.Controllers
{
    public class OpenAppleStorePageCommand : Command
    {
        public override void Execute()
        {
            Application.OpenURL(Constants.APPLE_STORE_PAGE_URL);
        }
    }
}