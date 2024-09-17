using System;
using _Project.SceneManagementUtilities.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.SceneManagementUtilities
{
    public class LoadingSceneZeitnotManager : SceneZeitnot {
    public static LoadingSceneZeitnotManager Instance;
    private const float kTooltipRefreshRate = 5f;

    [Header("Activate/Deactivate Objects")] 
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject LoadingPanel;

    [Header("Loading Panel Links")] 
    [SerializeField] private GameObject ProgressBar;
    [SerializeField] private Text TextField;
    //[SerializeField] private LoadingTips Tips;
    [SerializeField] private TMP_Text TipText;

    public Func<float> ProgressFunc;
    private bool _isActive = false;
    private float _toolTipTimer = 0f;

    public override bool AllowMultipleInstances => false;
    ///protected override PseudoSceneTypes GetSceneType() => PseudoSceneTypes.None;
    ///protected override string GetScenePath() => ScenesHelper.kSceneLoader;

    private void Awake()
    {
        Instance = this;
        SceneGroupType = SceneGroupType.None;
        ///Path = ScenesHelper.kSceneLoader;
        
        ///SceneChangeService.Instance.AddPseudoScene(GetScenePath(), this);

        // as default visuals are hidden
        SetObjectsActive(false);
    }

    public override void SetObjectsActive(bool value)
    {
        Camera.SetActive(value);
        Canvas.SetActive(value);
        LoadingPanel.SetActive(value);
        _isActive = value;
    }

    private void Update()
    {
        if(!_isActive) return;
        
        if (ProgressFunc != null)
        {
            var progress = ProgressFunc();
            SetFillAmount(progress);
        }

        if (Time.time >= _toolTipTimer)
        {
            _toolTipTimer = Time.time + kTooltipRefreshRate;
            SetTooltip();
        }
    }

    private void SetFillAmount(float value)
    {
        ///if (ProgressBar != null)
            ///ProgressBar.GetComponent<UIProgressBar>().fillAmount = Mathf.Clamp01(value);
    }

    private void SetTooltip()
    {
        //TipText.text = "<color=\"orange\"><b>Tips: </b></color>" + Tips.GetRandomTip();
    }

    public void SetVisibility(bool isActive)
    {
        if (_isActive != isActive)
        {
            var fillAmount = isActive ? 0f : 1f;
            SetFillAmount(fillAmount);
        }

        SetObjectsActive(isActive);
    }

    public void ShowMessage(string text)
    {
        TextField.text = text;
    }
}
}