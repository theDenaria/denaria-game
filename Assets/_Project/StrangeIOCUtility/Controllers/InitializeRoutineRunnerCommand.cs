using _Project.StrangeIOCUtility.Models;
using strange.extensions.command.impl;
using System;
using System.Collections;

namespace _Project.StrangeIOCUtility.Controllers
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