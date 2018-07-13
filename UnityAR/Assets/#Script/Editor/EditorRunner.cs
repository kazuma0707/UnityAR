
namespace UnityEditorExtension.Scripts
{
    using UnityEditor;

    class EditorRunner
    {

        [MenuItem("Edit/Run _F5", priority = 140)]
        private static void Run()
        {
            EditorApplication.isPlaying = true;
        }

        [MenuItem("Edit/Run _F5", validate = true)]
        private static bool CanRun()
        {
            return !EditorApplication.isPlaying;
        }

        [MenuItem("Edit/Stop #_F5", priority = 141)]
        private static void Stop()
        {
            EditorApplication.isPlaying = false;
        }

        [MenuItem("Edit/Stop #_F5", validate = true)]
        private static bool CanStop()
        {
            return EditorApplication.isPlaying;
        }
    }
}
