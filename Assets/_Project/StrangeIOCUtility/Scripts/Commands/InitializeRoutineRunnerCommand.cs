using System;
using System.Collections;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using strange.extensions.command.impl;

namespace _Project.StrangeIOCUtility.Scripts.Commands
{
    [Obsolete("We use RoutineRunner's PostConstruct instead of initializing RoutineRunner by StrangeIoC command.", true)]
    public class InitializeRoutineRunnerCommand : Command
    {
        [Inject] public IRoutineRunner RoutineRunner { get; set; }

        public override void Execute()
        {
            RoutineRunner.StartCoroutine(CoRoutineRunnerInitializerRoutine());
        }

        private IEnumerator CoRoutineRunnerInitializerRoutine()
        {
            yield return null;
        }

    }
}