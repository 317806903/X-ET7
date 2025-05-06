using System;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ET;
using UnityEngine;

public class UILocalize
{
    public static void SpawnEUIResetKey(GameObject gameObject)
    {
        if (null == gameObject)
        {
            Debug.LogError("UICode Select GameObject is null!");
            return;
        }

        try
        {
            DoUITextResetKey(gameObject);
        }
        finally
        {
        }
    }

    public static void DoUITextResetKey(GameObject go)
    {
        if (null == go)
        {
            return;
        }

        var uiTextLocalizeMonoViews = go.GetComponentsInChildren<UITextLocalizeMonoView>(true);
        foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in uiTextLocalizeMonoViews)
        {
            uiTextLocalizeMonoView.DoChangeLanguage_ResetKey();
        }
    }

    [MenuItem("Tools/检测prefab中的文本是否已经配置在excel")]
    public static void ChkLocalizeConfig_UI()
    {
        //生成 "_LocalizeConfig_UI.txt";
        ET.ToolsEditor.ExcelExporterUI();
        //读取 "_LocalizeConfig_UI.txt";
        string _LocalizeConfig_UI_Path = Application.dataPath + "/Config/Excel/AbilityConfig/TextKeyValue/_LocalizeConfig_UI.txt";

        if (!File.Exists(_LocalizeConfig_UI_Path))
        {
            Debug.LogError(" 当前不存在_LocalizeConfig_UI.txt!!!");
            return;
        }

        Dictionary<string, string> dicExecel = new();
        string[] execelLocalizeConfig_UI = File.ReadAllLines(_LocalizeConfig_UI_Path);
        foreach (string value in execelLocalizeConfig_UI)
        {
            string[] tmp = value.Split("|");
            if (tmp.Length < 2)
            {
                continue;
            }
            if (dicExecel.TryGetValue(tmp[0], out string content))
            {
                if (content == tmp[1])
                {

                }
                else
                {
                    Debug.LogError($"tmp[0] 存在多个value [{tmp[1]}] 和 [{content}]");
                }
            }
            else
            {
                dicExecel.Add(tmp[0], tmp[1]);
            }
        }
        File.Delete(_LocalizeConfig_UI_Path);

        //获取UIPrefab目录中所有prefab，遍历 UITextLocalizeMonoView
        Dictionary<string, string> dicPrefab = new();
        string UIPrefabDirPath = Application.dataPath + "/ResAB/UIPrefab";
        if (Directory.Exists(UIPrefabDirPath))
        {
            DirectoryInfo direction = new DirectoryInfo(UIPrefabDirPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".prefab"))
                {
                    string prefabPath = "Assets" + files[i].FullName.Remove(0, Application.dataPath.Length);
                    prefabPath = prefabPath.Replace("\\", "/");

                    GameObject go = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(System.Object)) as GameObject;
                    if (go != null)
                    {
                        var uiTextLocalizeMonoViews = go.GetComponentsInChildren<UITextLocalizeMonoView>(true);
                        foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in uiTextLocalizeMonoViews)
                        {
                            var(textKey, textDefaultValue) = uiTextLocalizeMonoView.GetInfo();
                            if (string.IsNullOrEmpty(textKey))
                            {
                                Debug.LogError($"存在textKey为空  [{files[i].Name}][{uiTextLocalizeMonoView.gameObject.name}]");
                                continue;
                            }
                            if (dicPrefab.TryGetValue(textKey, out string textDefaultValueExist))
                            {
                                if (textDefaultValue == textDefaultValueExist)
                                {
                                    continue;
                                }
                                else
                                {
                                    Debug.LogError($"存在多个textKey[{textKey}],但是默认值不同  [{textDefaultValueExist}][{textDefaultValue}]  [{files[i].Name}][{uiTextLocalizeMonoView.gameObject.name}]");
                                    continue;
                                }
                            }
                            dicPrefab.Add(textKey, textDefaultValue);
                        }
                    }
                }
            }
        }
        //比对得到 新增的textKey 和 之前有，现在没的excelKey
        Dictionary<string, string> dicPrefabClone = new(dicPrefab);
        foreach (var tmp in dicPrefabClone)
        {
            if (dicExecel.ContainsKey(tmp.Key))
            {
                dicExecel.Remove(tmp.Key);
                dicPrefab.Remove(tmp.Key);
            }
        }

        string __NotLocalized_UI_Path = Application.dataPath + "/Config/Excel/AbilityConfig/TextKeyValue/__NotLocalized_UI.txt";
        List<string> tmp2 = new();
        tmp2.Add($"---------这是 prefab中存在, 但是excel中没有配置的项");
        foreach (var dicPrefabInfo in dicPrefab)
        {
            tmp2.Add($"{dicPrefabInfo.Key}\t{dicPrefabInfo.Value}");
        }
        tmp2.Add($"\n\n---------这是 excel配置,但是不存在prefab中的项");
        foreach (var dicExecelInfo in dicExecel)
        {
            tmp2.Add($"{dicExecelInfo.Key}\t{dicExecelInfo.Value}");
        }
        File.WriteAllLines(__NotLocalized_UI_Path, tmp2);
        Debug.LogFormat($"执行完成，结果请查看 {__NotLocalized_UI_Path}");
    }
}