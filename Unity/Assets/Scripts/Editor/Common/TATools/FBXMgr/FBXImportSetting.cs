using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using TATools.FormatSetting;

namespace TATools.FBXFormatSetting
{
    public class FBXImportSetting : AssetPostprocessor
    {
        static bool settingEnabled = true;
        /// <summary>
        /// 打开Unity编辑器的时候设置一遍开关,方便在Unity未启动之前就关掉
        /// </summary>
        [InitializeOnLoadMethod]
        static void OnOpenUnityEditor()
        {
            settingEnabled = true;
            Debug.Log($"Unity启动/工具初始化成功： 设置FBX格式:settingEnabled={settingEnabled}");
        }

        [MenuItem("TATools/FBX格式设置/设置FBX自动格式/开启")]
        static void EnableSetFBX()
        {
            settingEnabled = true;
            MatchingFBXRuleMgr.Instance = null;
            Debug.Log("设置FBX自动格式/开启");
        }

        [MenuItem("TATools/FBX格式设置/设置FBX自动格式/关闭")]
        static void DisableSetFBX()
        {
            settingEnabled = false;
            Debug.Log("设置FBX自动格式/关闭");
        }

        void OnPreprocessModel()
        {
            if (assetPath.StartsWith("Packages/"))
            {
                return;
            }
            if (!settingEnabled)
            {
                bool bNotImport = true;
                if (GlobalFormatSettingTool.ChkForceReimportPath(assetPath))
                {
                    bNotImport = false;
                }

                if (bNotImport)
                {
                    Debug.LogError("当前自动控制FBX格式是关闭的");
                    return;
                }
            }

			ModelImporter fbxImporter = assetImporter as ModelImporter;
            SetFBXImportSetting(assetPath, fbxImporter);
        }

        static void SetFBXImportSetting(string path, ModelImporter fbxImporter)
        {
            string fileName = Path.GetFileName(path);
            FBXCatalogHelper fbxCatalogHelper = new FBXCatalogHelper(path, fbxImporter);
            FBXCatalog fbxCatalog = fbxCatalogHelper.fbxCatalog;
            if (fbxCatalogHelper.matchingRule.dealType == (int)TATools.FBXFormatSetting.FBXCatalog.NotMatch)
            {
                Debug.LogError($"存在未被匹配处理规则的资源:{path}");
            }
            else
            {
                Debug.Log($"{path} 已匹配规则:{fbxCatalogHelper.matchingRule.matchRule}");
            }

            FBXImportSettingTemplate template = new FBXImportSettingTemplateHelper(fbxCatalog, fbxImporter).template;
            if (template != null)
            {
                SetFBXImportSettingFromTemplate(template, fbxImporter);
            }

        }

        static void SetFBXImportSettingFromTemplate(FBXImportSettingTemplate template, ModelImporter fbxImporter)
        {
            fbxImporter = template.fbxImporter;
            fbxImporter.SaveAndReimport();
        }

    }
}
