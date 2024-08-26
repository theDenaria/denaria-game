using System;
using _Project.ForceUpdate.Scripts.Models;
using _Project.ForceUpdate.Scripts.Services;
using _Project.ForceUpdate.Scripts.Signals;
using _Project.ForceUpdate.Scripts.TitleData;
using _Project.SceneManagementUtilities;
using _Project.SceneManagementUtilities.Models;
using _Project.SceneManagementUtilities.Signals;
using strange.extensions.command.impl;
using _Project.Utilities;
using UnityEngine;

namespace _Project.ForceUpdate.Scripts.Controllers
{
    public class CheckForceUpdateNeededCommand : Command
    {
        [Inject] public IMinimumClientVersionNeededModel MinimumClientVersionNeededModel { get; set; }
        [Inject] public IApplicationVersionModel ApplicationVersionModel { get; set; }
        [Inject] public IMinimumClientVersionNeededService MinimumClientVersionNeededService { get; set; }
        [Inject] public OpenForceUpdatePopupSignal OpenForceUpdatePopupSignal { get; set; }
        
        [Inject] public SceneChangedSignal SceneChangedSignal { get; set; }

        public override async void Execute()
        {
            Retain();
            await MinimumClientVersionNeededService.GetTitleData(Constants.MINIMUM_CLIENT_VERSION_NEEDED_TITLE_DATA, OnCallbackComplete);
        }

        private void OnCallbackComplete(MinimumClientVersionNeededTitleData minimumClientVersionNeededTitleData)
        {
            FillMinimumClientVersionNeededModel(minimumClientVersionNeededTitleData);
            bool isForceUpdateNeeded = CheckIsForceUpdateNeeded();

            if (isForceUpdateNeeded)
            {
                UnityEngine.Debug.Log("Application Version is older than MinimumClientVersionNeeded.");
                OpenForceUpdatePopupSignal.Dispatch();
                UnityEngine.Debug.Log("OpenForceUpdatePopupSignal.Dispatched");
                
                SceneChangedSignal.Dispatch(new NotifySceneChangeCommandData(Constants.FORCE_UPDATE_SCENE,Constants.FORCE_UPDATE_NEEDED, "success"));

                Fail();//So that game does not continue downloading etc. in the background.
            }
            Release();
        }

        private void FillMinimumClientVersionNeededModel(MinimumClientVersionNeededTitleData minimumClientVersionNeededTitleData)
        {
            MinimumClientVersionNeededModel.MajorVersion = minimumClientVersionNeededTitleData.majorVersion;
            MinimumClientVersionNeededModel.MinorVersion = minimumClientVersionNeededTitleData.minorVersion;
            MinimumClientVersionNeededModel.PatchVersion = minimumClientVersionNeededTitleData.patchVersion;
        }

        private bool CheckIsForceUpdateNeeded()
        {
            TryParseIntoApplicationVersionModel();
            
            if (ApplicationVersionModel.MajorVersion > MinimumClientVersionNeededModel.MajorVersion)
            {
                return false;
            }
            if (ApplicationVersionModel.MajorVersion < MinimumClientVersionNeededModel.MajorVersion)
            {
                return true;
            }
            
            // If major versions are equal, compare minor versions
            if (ApplicationVersionModel.MinorVersion > MinimumClientVersionNeededModel.MinorVersion)
            {
                return false;
            }
            if (ApplicationVersionModel.MinorVersion < MinimumClientVersionNeededModel.MinorVersion)
            {
                return true;
            }
            
            // If minor versions are also equal, compare patch versions
            if (ApplicationVersionModel.PatchVersion > MinimumClientVersionNeededModel.PatchVersion)
            {
                return false;
            }
            if (ApplicationVersionModel.PatchVersion < MinimumClientVersionNeededModel.PatchVersion)
            {
                return true;
            }
            
            return false;
        }
        
        private void TryParseIntoApplicationVersionModel()
        {
            try
            {
                UnityEngine.Debug.Log("Application.version is: " + Application.version);
                UnityEngine.Debug.Log("Minimum Client Version Needed Model is: " +
                                      MinimumClientVersionNeededModel.MajorVersion + "."
                                      + MinimumClientVersionNeededModel.MinorVersion + "."
                                      + MinimumClientVersionNeededModel.PatchVersion);
                
                string currentVersionString = Application.version;

                string[] versionStringSplit = currentVersionString.Split('.');

                ApplicationVersionModel.MajorVersion = int.Parse(versionStringSplit[0]);
                UnityEngine.Debug.Log("ApplicationVersionModel.MajorVersion is: " + ApplicationVersionModel.MajorVersion);

                ApplicationVersionModel.MinorVersion = int.Parse(versionStringSplit[1]);
                UnityEngine.Debug.Log("ApplicationVersionModel.MinorVersion is: " + ApplicationVersionModel.MinorVersion);

                ApplicationVersionModel.PatchVersion = int.Parse(versionStringSplit[2]);
                UnityEngine.Debug.Log("ApplicationVersionModel.PatchVersion is: " + ApplicationVersionModel.PatchVersion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}