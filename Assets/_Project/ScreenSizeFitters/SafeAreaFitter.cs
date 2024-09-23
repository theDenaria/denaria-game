using UnityEngine;

namespace _Project.ScreenSizeFitters
{
    [ExecuteAlways]
    public class SafeAreaFitter : MonoBehaviour
    {
        public Canvas canvas;
        private RectTransform safeAreaRectTransform;

        Rect currentSafeArea = new Rect();
        private ScreenOrientation currentScreenOrientation = ScreenOrientation.AutoRotation;

        // Start is called before the first frame update
        void Start()
        {
            safeAreaRectTransform = GetComponent<RectTransform>();
            currentScreenOrientation = Screen.orientation;
            currentSafeArea = Screen.safeArea;

            AdjustSafeArea();
        }

        private void AdjustSafeArea()
        {
            if (safeAreaRectTransform == null)
            {
                return;
            }

            Rect safeArea = Screen.safeArea;

            Vector2 minimumAnchor = safeArea.position;
            Vector2 maximumAnchor = safeArea.position + safeArea.size;

            minimumAnchor.x /=
                canvas.pixelRect.width; // same as: minimumAnchor.x = minimumAnchor.x / canvas.pixelRect.width;
            minimumAnchor.y /= canvas.pixelRect.height;

            maximumAnchor.x /= canvas.pixelRect.width;
            maximumAnchor.y /= canvas.pixelRect.height;


            safeAreaRectTransform.anchorMin = minimumAnchor;
            safeAreaRectTransform.anchorMax = maximumAnchor;

            currentScreenOrientation = Screen.orientation;
            currentSafeArea = Screen.safeArea;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentScreenOrientation != Screen.orientation || currentSafeArea != Screen.safeArea)
            {
                AdjustSafeArea();
            }
        }
    }
}