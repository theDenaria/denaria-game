using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Commands;
using _Project.Language.Scripts.Services;
using _Project.Language.Scripts.Signals;
using _Project.Language.Scripts.Utility;
using _Project.LoggingAndDebugging;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.Language.Scripts.Views
{
    [RequireComponent(typeof(LanguageView))]
    public class LanguageMediator : Mediator
    {
        [Inject] public LanguageView View { get; set; }
        [Inject] public FillTextByKeySignal FillTextByKeySignal { get; set; }

        [Inject] public ChangeLanguageSignal ChangeLanguageSignal { get; set; }

        private void OnEnable()
        {
            //View.FillTextByKey();
        }
        
        public override void OnRegister()
        {
            base.OnRegister();
            View.Init();
            FillTextByKeySignal.Dispatch(new FillTextByKeyCommandData(View, View.Key, View.WildStringDictionary));
            View.changeLanguageViewSignal.AddListener(FireChangeLanguageSignal);
        }
        
        [ListensTo(typeof(LanguageServiceInitializedSignal))]
        public void HandleAllServicesInitialized()
        {
            //View.FillTextByKey();
            FillTextByKeySignal.Dispatch(new FillTextByKeyCommandData(View, View.Key, View.WildStringDictionary));
        }

        [ListensTo(typeof(LanguageChangedSignal))]
        public void HandleLanguageChanged()
        {
            //View.FillTextByKey();
            FillTextByKeySignal.Dispatch(new FillTextByKeyCommandData(View, View.Key, View.WildStringDictionary));
        }

        public void FireChangeLanguageSignal(ChangeLanguageCommandData changeLanguageCommandData)
        {
            ChangeLanguageSignal.Dispatch(changeLanguageCommandData);
        }
    }
}