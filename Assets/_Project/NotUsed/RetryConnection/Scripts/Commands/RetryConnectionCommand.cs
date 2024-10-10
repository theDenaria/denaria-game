using System.Collections.Generic;
using _Project.LoggingAndDebugging;
using _Project.RetryConnection.Scripts.Models;
using _Project.RetryConnection.Scripts.Signals;
using strange.extensions.command.impl;
//using _Project.Login.Services;

namespace _Project.RetryConnection.Scripts.Commands
{
    public class RetryConnectionCommand : Command
    {
        [Inject] public IRetryConnectionChannels RetryConnectionChannels { get; set; }
        [Inject] public ToggleRetryConnectionCanvasSignal ToggleRetryConnectionCanvasSignal { get; set; }
        
        #region ChannelServices
        //[Inject] public ILoginService LoginService { get; set; }
        #endregion
        
        public override void Execute()
        {
            foreach (string connectionChannel in RetryConnectionChannels.ChannelsList)
            {
                //if(connectionChannel.Equals(Constants.LOGIN_CONNECTION)) LoginService.AutoLoginWithDeviceId();
            }

            DebugLoggerMuteable.Log("RetryConnectionCommand");
            RetryConnectionChannels.ChannelsList = new List<string>();
            ToggleRetryConnectionCanvasSignal.Dispatch(false);
        }
        
    }
}