using _Project.UIZeitnot.PanelZeitnot.Scripts;
using _Project.UIZeitnot.PanelZeitnot.Scripts.Enums;
using _Project.UIZeitnot.TextMeshProZeitnot;
using _Project.WaitingCanvas.Scripts.WaitHandlers;
using DG.Tweening;
using strange.extensions.mediation.impl;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Project.WaitingCanvas.Scripts.Views
{
    public class WaitingCanvasView : View
    {
        [field: SerializeField] private PanelZeitnot Panel { get; set; }
        [field: SerializeField] private TextMeshProZeitnot MessageText { get; set; }
        [field: SerializeField] private DOTweenAnimation WaitingIndicatorRotationTweenAnimation { get; set; }
        [field: SerializeField] private ParticleSystem WaitingIndicatorParticleSystem { get; set; }
        [field: SerializeField] private GameObject SpinAnimation { get; set; }

        private List<IWaitHandler> ActiveWaitHandlers { get; set; } = new List<IWaitHandler>();
        
        internal void InitializeView()
        {
            ActiveWaitHandlers = new List<IWaitHandler>();
            Panel.OnPanelVisibilityChangedEvent += OnPanelVisibilityChangedEvent;
        }

        internal void FinalizeView()
        {
            Panel.OnPanelVisibilityChangedEvent -= OnPanelVisibilityChangedEvent;
            ActiveWaitHandlers = null;
        }

        internal void AddWaitHandler(IWaitHandler waitHandler)
        {
            if (ActiveWaitHandlers == null) { return; }
            ActiveWaitHandlers.Add(waitHandler);
            UpdateMessageText();
            if (ActiveWaitHandlers.Count > 0 && (Panel.VisibilityType != PanelZeitnotVisibilityTypes.Shown || Panel.VisibilityType != PanelZeitnotVisibilityTypes.Showing))
            {
                Panel.Show();
            }
        }

        internal void RemoveWaitHandler(IWaitHandler waitHandler)
        {
            if (ActiveWaitHandlers == null || waitHandler == null)
            {
                return;
            }
            ActiveWaitHandlers.Remove(waitHandler);
            UpdateMessageText();
            if (ActiveWaitHandlers.Count < 1)
            {
                Panel.Hide();
            
            }
        }

        private void ShowWaitingIndicator()
        {
           // WaitingIndicatorRotationTweenAnimation.DORestart();
           // WaitingIndicatorParticleSystem.Play(true);
           SpinAnimation.SetActive(true);
           
        }

        private void HideWaitingIndicator()
        {
            //WaitingIndicatorRotationTweenAnimation.DORewind();
          //  WaitingIndicatorParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
          SpinAnimation.SetActive(false);
         
        }

        private void UpdateMessageText()
        {
            if (ActiveWaitHandlers.Count < 1)
            {
                MessageText.text = "";
               
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IWaitHandler waitHandler in ActiveWaitHandlers)
            {
                if (!string.IsNullOrEmpty(waitHandler.Message))
                {
                    stringBuilder.AppendLine(waitHandler.Message);
                }
            }
            MessageText.text = stringBuilder.ToString();
            
        }

        private void OnPanelVisibilityChangedEvent(PanelZeitnotVisibilityTypes panelVisibilityType)
        {
            if (panelVisibilityType == PanelZeitnotVisibilityTypes.Showing)
            {
                ShowWaitingIndicator();
            }
            else if (panelVisibilityType == PanelZeitnotVisibilityTypes.Hiding)
            {
                HideWaitingIndicator();
            }
        }

    }
}
