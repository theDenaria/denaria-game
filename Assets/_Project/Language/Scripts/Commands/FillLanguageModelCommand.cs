using System.Collections.Generic;
using System.Linq;
using _Project.Language.Scripts.Models;
using _Project.Language.Scripts.ScriptableObjects.Resources;
using _Project.Language.Scripts.Services;
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
            List<LanguageScriptableObject> languageScriptableObjectList = Resources.LoadAll<LanguageScriptableObject>("Languages").ToList();
            UnityEngine.Debug.Log("FillLanguageModelCommand Resources");

            LanguageModel.FillLanguagesModel(languageScriptableObjectList);
        }
    }
}