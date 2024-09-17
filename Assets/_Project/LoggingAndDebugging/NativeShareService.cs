namespace _Project.LoggingAndDebugging
{
    public delegate void OnFinishSharedDelegate();

    public class NativeShareService : INativeShareService//Lazy
    {
        public void ShareFile(string path, OnFinishSharedDelegate onFinishShared)
        {
            //TODO: Uncomment after adding Native Share. -14 August 2024
            /*new NativeShare().AddFile(path)
                .SetCallback((result, shareTarget) =>
                {
                    Debug.Log("Share result: " + result + ", selected app: " + shareTarget);
                    onFinishShared?.Invoke();
                })
                .Share();*/
        }

        public void ShareText(string subject, string text, OnFinishSharedDelegate onFinishShared)
        {
            //TODO: Uncomment after adding Native Share. -14 August 2024
            /*new NativeShare().SetSubject(subject)
                .SetText(text)
                .SetCallback((result, shareTarget) =>
                {
                    Debug.Log("Share result: " + result + ", selected app: " + shareTarget);
                    onFinishShared?.Invoke();
                })
                .Share();
                */
        }

    }
}
