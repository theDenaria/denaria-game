using System.Linq;
using _Project.Language.Scripts.Models;
using _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Language.Scripts.Commands
{
    public class FillLanguageModelCommand : Command
    {
        [Inject] public ILanguageModel LanguageModel { get; set; }

        public override void Execute()
        {
            UnityEngine.Debug.Log("FillLanguageModelCommand");
            TranslatableTextListModel translatableTextListModel = Resources.LoadAll<TranslatableTextListModel>("Languages").ToList()[0];
            UnityEngine.Debug.Log("FillLanguageModelCommand Resources");

            LanguageModel.FillLanguagesModel(translatableTextListModel);
        }
    }
}