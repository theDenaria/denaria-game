using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace _Project.GameSceneManager.TPSSceneManager.Scripts.Views
{
    public class PlayerUIView : ViewZeitnot
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI currentAmmoText;
        [SerializeField] private TextMeshProUGUI totalAmmoText;
        [SerializeField] private TextMeshProUGUI healthText;

        internal void UpdateAmmoUI(string ammoText, string totalAmmoText)
        {
            currentAmmoText.text = ammoText;
            this.totalAmmoText.text = totalAmmoText;
        }

        internal void UpdateHealthBar(float healthPercentage)
        {
            healthBar.fillAmount = healthPercentage;
        }

        internal void UpdateHealthText(string healthText)
        {
            this.healthText.text = healthText;
        }
    }
}
