using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;
using TMPro;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Views
{
    public class PlayerUIMediator : Mediator
    {
        [Inject] public PlayerUIView View { get; set; }
        [Inject] public TPSServerReceiveHealthUpdateSignal TPSServerReceiveHealthUpdateSignal { get; set; }
        [Inject] public TPSServerReceiveAmmoUpdateSignal TPSServerReceiveAmmoUpdateSignal { get; set; }

        [ListensTo(typeof(TPSServerReceiveHealthUpdateSignal))]
        private void UpdateHealthUI(float currentHealth, float maxHealth)
        {
            // Update the health bar fill
            View.UpdateHealthBar(currentHealth / maxHealth);

            // Update the health text
            View.UpdateHealthText($"{currentHealth} / {maxHealth}");
        }

        [ListensTo(typeof(TPSServerReceiveAmmoUpdateSignal))]
        private void UpdateAmmoUI(int currentAmmo, int totalAmmo)
        {
            View.UpdateAmmoUI($"{currentAmmo}", $"{totalAmmo}");
        }
    }
}
