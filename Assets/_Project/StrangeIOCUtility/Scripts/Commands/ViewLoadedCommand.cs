using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.StrangeIOCUtility.Scripts.Commands
{
	public class ViewLoadedCommand : Command
	{
		[Inject] public ViewZeitnot view { get; set; }

		public override void Execute()
		{
			// view.OnLoad();
		}
	}
}