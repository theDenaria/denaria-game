using _Project.PlayerProfile.Scripts.Controllers;
using _Project.PlayerProfile.Scripts.Views;
using _Project.SceneManagementUtilities.Views;
using _Project.StrangeIOCUtility.CrossContext;
using UnityEngine;

namespace _Project.PlayerProfile.Scripts.Context
{
	public class PlayerProfileContext : SignalContext
	{
		public PlayerProfileContext(MonoBehaviour view) : base(view)
		{
		}

		protected override void mapBindings()
		{
			base.mapBindings();

			BindPlayerProfilePage();
		}

		private void BindPlayerProfilePage()
		{
			mediationBinder.Bind<AvatarProfileView>().To<AvatarProfileMediator>();
			mediationBinder.Bind<NicknameProfileView>().To<NicknameProfileMediator>();
			mediationBinder.Bind<ChangeAvatarButtonView>().To<ChangeAvatarButtonMediator>();
			mediationBinder.Bind<ChangeNicknameButtonView>().To<ChangeNicknameButtonMediator>();
			mediationBinder.Bind<PlayerProfilePageBackButtonView>().To<PlayerProfilePageBackButtonMediator>();
			mediationBinder.Bind<TabView>().To<TabMediator>();

			//mediationBinder.Bind<OpenSettingsSceneButtonView>().To<OpenSettingsSceneButtonMediator>();

			commandBinder.Bind<ClosePlayerProfilePageSignal>().To<ClosePlayerProfilePageCommand>();
			commandBinder.Bind<UpdateAvatarViewSignal>().To<UpdateAvatarViewCommand>();
			commandBinder.Bind<UpdateNicknameViewSignal>().To<UpdateNicknameViewCommand>();
		}
	}
}