using System.Collections;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using _Project.UIZeitnot.ImageZeitnot;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Views
{
    public class CrosshairView : ViewZeitnot
    {
        [field: SerializeField] private ImageZeitnot CrosshairImage { get; set; }
        [field: SerializeField] private ImageZeitnot HitmarkImage { get; set; }
        
        private Color redColor = Color.red;
        private Color whiteColor = Color.white;

        private float duration = 0.1f; // Duration for which the first image stays red
        private float timer = 0f;      // Tracks the remaining time for the red color
        private bool isCoroutineRunning = false;
        
        internal void Init()
        {
            
        }

        private void OnDisable()
        {
            
        }

        public void EnableCanvas()
        {
            
        }
        
        
        // Call this function to start or extend the process
        public void PaintAndToggleImages()
        {
            timer = duration; // Reset or extend the timer
            if (!isCoroutineRunning)
            {
                StartCoroutine(PaintImagesCoroutine());
            }
        }
        
        private IEnumerator PaintImagesCoroutine()
        {
            isCoroutineRunning = true;

            // Paint the first image red
            CrosshairImage.color = redColor;

            // Enable the second image
            HitmarkImage.enabled = true;

            // Keep checking the timer
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            // Paint the first image white
            CrosshairImage.color = whiteColor;

            // Disable the second image
            HitmarkImage.enabled = false;

            isCoroutineRunning = false;
        }


    }
}