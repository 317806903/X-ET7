using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

namespace ET
{
    public class BuildPostProcessor
    {
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.Android)
            {
            }
        }
    }

}