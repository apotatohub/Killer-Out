namespace com.faithstudio.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class EditorAppPreviewMaker
    {
#if UNITY_EDITOR
        [MenuItem("AppPreview/Screenshot")]
        static public void OnTakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot(EditorUtility.SaveFilePanel("Save Screenshot As", "", "", "png"));

        }
#endif
    }
}

