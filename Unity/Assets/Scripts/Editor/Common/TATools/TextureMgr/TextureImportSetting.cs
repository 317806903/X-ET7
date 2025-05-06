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

namespace TATools.TextureFormatSetting
{
    public class TextureImportSetting : AssetPostprocessor
    {
        static bool settingEnabled = true;
        /// <summary>
        /// 打开Unity编辑器的时候设置一遍开关,方便在Unity未启动之前就关掉
        /// </summary>
        [InitializeOnLoadMethod]
        static void OnOpenUnityEditor()
        {
            settingEnabled = true;
            Debug.Log($"Unity启动/工具初始化成功： 设置图片格式:settingEnabled={settingEnabled}");
        }

        [MenuItem("TATools/贴图格式设置/设置图片自动格式/开启")]
        static void EnableSetTexture()
        {
            settingEnabled = true;
            MatchingTextureRuleMgr.Instance = null;
            Debug.Log("设置图片自动格式/开启");
        }

        [MenuItem("TATools/贴图格式设置/设置图片自动格式/关闭")]
        static void DisableSetTexture()
        {
            settingEnabled = false;
            Debug.Log("设置图片自动格式/关闭");
        }

        void OnPreprocessTexture()
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
                    Debug.LogError("当前自动控制图片格式是关闭的");
                    return;
                }
            }

            TextureImporter textureImporter = assetImporter as TextureImporter;
            SetTextureImportSetting(assetPath, textureImporter);
        }

        static void SetTextureImportSetting(string path, TextureImporter textureImporter)
        {
            string fileName = Path.GetFileName(path);
            TextureCatalogHelper textureCatalogHelper = new TextureCatalogHelper(path, textureImporter);
            TextureCatalog textureCatalog = textureCatalogHelper.textureCatalog;
            if (textureCatalogHelper.matchingRule.dealType == (int)TATools.TextureFormatSetting.TextureCatalog.NotMatch)
            {
                Debug.LogError($"存在未被匹配处理规则的资源:{path}");
            }
            else
            {
                Debug.Log($"{path} 已匹配规则:{textureCatalogHelper.matchingRule.matchRule}");
            }

            if (string.IsNullOrEmpty(textureCatalogHelper.maxSizeErr) == false)
            {
                Debug.LogError(textureCatalogHelper.maxSizeErr);
            }
            int maxSize = textureCatalogHelper.maxSize;
            TextureImportSettingTemplate template = new TextureImportSettingTemplateHelper(textureCatalog, maxSize, textureImporter).template;
            if (template != null)
            {
                SetTextureImportSettingFromTemplate(template, textureImporter);
            }
        }

        static void SetTextureImportSettingFromTemplate(TextureImportSettingTemplate template, TextureImporter textureImporter)
        {
            textureImporter = template.textureImporter;
            textureImporter.SetPlatformTextureSettings(template.androidSetting);
            textureImporter.SetPlatformTextureSettings(template.iphoneSetting);
            textureImporter.SetPlatformTextureSettings(template.standaloneSetting);
        }

    }
}
