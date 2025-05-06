using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace ET
{
    public static class BuildServer
    {
        [MenuItem("Pack/BuildServer_win", false, 400)]
        public static async ETTask BuildServer_win()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
            await BuildServerInternal(buildTarget);
        }

        [MenuItem("Pack/BuildServer_linux", false, 401)]
        public static async ETTask BuildServer_linux()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            BuildTarget buildTarget = BuildTarget.StandaloneLinux64;
            await BuildServerInternal(buildTarget);
        }

        private static async ETTask BuildServerInternal(BuildTarget buildTarget)
        {
            string genCode;
            if (buildTarget == BuildTarget.StandaloneWindows64)
            {
                genCode = @"powershell -ExecutionPolicy Bypass -File Publish-win-x64.ps1";
            }
            else if (buildTarget == BuildTarget.StandaloneLinux64)
            {
                genCode = @"powershell -ExecutionPolicy Bypass -File Publish-linux-x64.ps1";
            }
            else
            {
                genCode = @"powershell -ExecutionPolicy Bypass -File Publish-win-x64.ps1";
            }

            ShellHelper.Run($"{genCode}", "../");

            string tmp12 = $"{Application.dataPath}/../../Publish/";
            DirectoryInfo directoryInfo = new (tmp12);
            var tt = directoryInfo.FullName+"/x";
            EditorUtility.RevealInFinder(tt);
        }
    }
}