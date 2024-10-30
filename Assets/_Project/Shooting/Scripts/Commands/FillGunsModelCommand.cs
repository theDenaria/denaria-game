using System.Collections.Generic;
using System.Linq;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using strange.extensions.command.impl;
using strange.extensions.injector.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class FillGunsModelCommand : Command
    {
        [Inject] public IGunsModel GunsModel { get; set; }
        public GunScriptableObject GunScriptableObjectInstance;

        public override void Execute()
        {
            UnityEngine.Debug.Log("FillGunsModelCommand");
            List<GunScriptableObject> gunScriptableObjectList = Resources.LoadAll<GunScriptableObject>("Guns").ToList();
            UnityEngine.Debug.Log("FillGunsModelCommand Resources");

            GunsModel.FillGunsModel(gunScriptableObjectList);

            var injector = new InjectionBinder();  // Or retrieve the main injector if available

            foreach (GunScriptableObject gunScriptableObject in gunScriptableObjectList)
            {
                injector.injector.Inject(gunScriptableObject);
            }
        }
    }
}