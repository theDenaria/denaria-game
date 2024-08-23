using strange.examples.myfirstproject;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace _Project.StrangeIOCUtility
{
    //Note how we extend Command, not EventCommand (as suggested for Signal Extensions)
    public class StartCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView{get; set;}

        public override void Execute()
        {
            CreateExampleView();//TODO: Delete
        }

        private void CreateExampleView()
        {
            GameObject go = new GameObject();
            go.name = "ExampleView";
            go.AddComponent<ExampleView>();
            go.transform.parent = contextView.transform;
        }
    }
}