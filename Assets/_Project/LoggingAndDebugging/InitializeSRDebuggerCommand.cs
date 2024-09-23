using System.Diagnostics;
using SRDebugger.Services;
using SRF.Service;
using strange.extensions.command.impl;

namespace _Project.LoggingAndDebugging
{
    public class InitializeSRDebuggerCommand : Command
    {
        [Inject] public ILogRecordService LogRecordService { get; set; }
        
        public override void Execute()
        {
            InitializeSRDebugger();
        }

        //[Conditional(Constants.TEST_BRANCH_BUILD), Conditional(Constants.UNITY_EDITOR)]
        private void InitializeSRDebugger()
        {
            Debug.Log("Initializing SRDebugger");
            SRDebug.Init();
            Debug.Log("Initialized SRDebugger");
            SRServiceManager.GetService<IOptionsService>(); //SRDebug.Init(); does not initialize Options Panel. 
                
            LogRecordService.SubscribeToLogs();
            Debug.Log("ILogRecordService SubscribeToLogs completed.");
        }

    }
}