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

namespace TATools.AudioFormatSetting
{
    public class AudioImportSetting : AssetPostprocessor
    {
        static bool settingEnabled = true;
        /// <summary>
        /// 打开Unity编辑器的时候设置一遍开关,方便在Unity未启动之前就关掉
        /// </summary>
        [InitializeOnLoadMethod]
        static void OnOpenUnityEditor()
        {
            settingEnabled = true;
            Debug.Log($"Unity启动/工具初始化成功： 设置Audio格式:settingEnabled={settingEnabled}");
        }

        [MenuItem("TATools/Audio格式设置/设置Audio自动格式/开启")]
        static void EnableSetAudio()
        {
            settingEnabled = true;
            MatchingAudioRuleMgr.Instance = null;
            Debug.Log("设置Audio自动格式/开启");
        }

        [MenuItem("TATools/Audio格式设置/设置Audio自动格式/关闭")]
        static void DisableSetAudio()
        {
            settingEnabled = false;
            Debug.Log("设置Audio自动格式/关闭");
        }

        void OnPreprocessAudio()
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
                    Debug.LogError("当前自动控制Audio格式是关闭的");
                    return;
                }
            }

            AudioImporter audioImporter = assetImporter as AudioImporter;
            SetAudioImportSetting(assetPath, audioImporter);
        }

        static void SetAudioImportSetting(string path, AudioImporter audioImporter)
        {
            string fileName = Path.GetFileName(path);
            AudioCatalogHelper audioCatalogHelper = new AudioCatalogHelper(path, audioImporter);
            AudioCatalog audioCatalog = audioCatalogHelper.audioCatalog;
            if (audioCatalogHelper.matchingRule.dealType == (int)TATools.AudioFormatSetting.AudioCatalog.NotMatch)
            {
                Debug.LogError($"存在未被匹配处理规则的资源:{path}");
            }
            else
            {
                Debug.Log($"{path} 已匹配规则:{audioCatalogHelper.matchingRule.matchRule}");
            }

            AudioImportSettingTemplate template = new AudioImportSettingTemplateHelper(audioCatalog, audioImporter).template;
            if (template != null)
            {
                SetAudioImportSettingFromTemplate(template, audioImporter);
            }

        }

        static void SetAudioImportSettingFromTemplate(AudioImportSettingTemplate template, AudioImporter audioImporter)
        {
            audioImporter = template.audioImporter;
            audioImporter.SaveAndReimport();
        }

    }
}
