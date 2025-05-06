using System;
using System.Linq;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace TATools.FormatSetting
{
    public class GlobalFormatSettingTool
    {
        public static List<string> selectPathList = new List<string>();
        [MenuItem("Assets/TATools/ForceReimport")]
        public static void ForceReimport()
        {
            string[] selectGuids = Selection.assetGUIDs;
            List<string> fileList = new List<string>();
            selectPathList.Clear();
            foreach (var selectGuid in selectGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(selectGuid);
                selectPathList.Add(path);
                GetDirectorFiles(path, ref fileList);
            }

            string pathPre = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/") + 1);
            for (int i = 0; i < fileList.Count; i++)
            {
                string filePath = fileList[i];
                filePath = filePath.Replace("\\", "/");
                string assetPath = filePath.Replace(pathPre, "");
                AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                if (assetImporter)
                {
                    assetImporter.SaveAndReimport();
                }
            }
            selectPathList.Clear();
        }

        public static bool ChkForceReimportPath(string assetPath)
        {
            bool isForceReimportPath = false;
            for (int i = 0; i < GlobalFormatSettingTool.selectPathList.Count; i++)
            {
                string path = GlobalFormatSettingTool.selectPathList[i];
                if (assetPath.Contains(path))
                {
                    isForceReimportPath = true;
                    break;
                }
            }

            return isForceReimportPath;
        }

        static void GetDirectorFiles(string dir, ref List<string> fileList)
        {
            if (File.Exists(dir))
            {
                if (dir.EndsWith(".meta") == false)
                {
                    fileList.Add(dir);
                }
                return;
            }
            DirectoryInfo d = new DirectoryInfo(dir);
            FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                GetDirectorFiles(fsinfo.FullName, ref fileList); //递归调用
            }
        }
    }
}