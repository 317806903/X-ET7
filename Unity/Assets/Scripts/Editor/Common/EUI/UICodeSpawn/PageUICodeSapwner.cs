﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;


public partial class UICodeSpawner
{
    static public void SpawnPageUICode(GameObject gameObject)
    {

        Path2WidgetCachedDict?.Clear();
        Path2WidgetCachedDict = new Dictionary<string, List<Component>>();
        FindAllWidgets(gameObject.transform, "");
        SpawnCodeForPageUISystem(gameObject);
        SpawnCodeForPageModel(gameObject);
        SpawnCodeForPageUI(gameObject);

        SpawnCodeForPageUIViewBehaviour(gameObject);
        AssetDatabase.Refresh();
    }

    static void SpawnCodeForPageUISystem(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/UIPage/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/UIPage/" + strDlgName + "/" + strDlgName + "System.cs";
        if (System.IO.File.Exists(strFilePath))
        {
            Debug.LogError("已存在 " + strDlgName + "System.cs,将不会再次生成。");
            return;
        }

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine("using System.Collections;")
            .AppendLine("using System.Collections.Generic;")
            .AppendLine("using System;")
            .AppendLine("using UnityEngine;")
            .AppendLine("using UnityEngine.UI;\r\n");

        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");

        strBuilder.AppendFormat("\t[FriendOf(typeof({0}))]\r\n", strDlgName);

        strBuilder.AppendFormat("\tpublic static class {0}\r\n", strDlgName + "System");
        strBuilder.AppendLine("\t{");

        strBuilder.AppendFormat("\t\tpublic static void RegisterUIEvent(this {0} self)\n", strDlgName)
            .AppendLine("\t\t{")
            .AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static async ETTask PreLoadWindow(this {0} self)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static async ETTask ShowPage(this {0} self, ShowWindowData contextData = null)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tself.View.uiTransform.SetVisible(true);");
        strBuilder.AppendLine("\t\t\t");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static void HidePage(this {0} self)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tself.View.uiTransform.SetVisible(false);");
        strBuilder.AppendLine("\t\t\t");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForPageModel(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/UIPage/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/UIPage/" + strDlgName + "/" + strDlgName + ".cs";
        if (System.IO.File.Exists(strFilePath))
        {
            Debug.LogError("已存在 " + strDlgName + ".cs,将不会再次生成。");
            return;
        }

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");

        strBuilder.AppendFormat("\tpublic class {0} : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic, IUIPage\r\n", strDlgName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendLine("\t\tpublic UnityEngine.Transform GetUITransform { get => View.uiTransform; }");
        strBuilder.AppendLine("\t\tpublic " + strDlgName + "ViewComponent View { get => this.GetComponent<" + strDlgName + "ViewComponent>(); }\r\n");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForPageUI(GameObject objPanel)
    {
        if (null == objPanel)
        {
            return;
        }
        string strDlgName = objPanel.name;
        string strDlgComponentName = objPanel.name + "ViewComponent";

        string strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/UIPage" +
                             "";

        if ( !System.IO.Directory.Exists(strFilePath) )
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }
        strFilePath     = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/UIPage/" + strDlgName + "ViewSystem.cs";

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}AwakeSystem : AwakeSystem<{1},Transform> \r\n", strDlgComponentName, strDlgComponentName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Awake({0} self,Transform transform)\n",strDlgComponentName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tself.uiTransform = transform;");
        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("");

        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}DestroySystem : DestroySystem<{1}> \r\n", strDlgComponentName, strDlgComponentName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Destroy({0} self)",strDlgComponentName);
        strBuilder.AppendLine("\n\t\t{");

        strBuilder.AppendFormat("\t\t\tself.DestroyWidget();\r\n");

        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("");

        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}AwakeSystem : AwakeSystem<{1},Transform> \r\n", strDlgName, strDlgName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Awake({0} self,Transform transform)\n",strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendFormat("\t\t\tself.AddComponent<{0},Transform>(transform);\r\n", strDlgComponentName);
        strBuilder.AppendLine("\t\t\tself.RegisterUIEvent();");
        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("\n");

        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}DestroySystem : DestroySystem<{1}> \r\n", strDlgName, strDlgName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Destroy({0} self)",strDlgName);
        strBuilder.AppendLine("\n\t\t{");

        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForPageUIViewBehaviour(GameObject objPanel)
    {
        if (null == objPanel)
        {
            return;
        }
        string strDlgName = objPanel.name;
        string strDlgComponentName = objPanel.name + "ViewComponent";

        string strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/UIPage";

        if ( !System.IO.Directory.Exists(strFilePath) )
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }
        strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/UIPage/" + strDlgComponentName + ".cs";

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[EnableMethod]");
        strBuilder.AppendFormat("\tpublic class {0} : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy\r\n", strDlgComponentName)
            .AppendLine("\t{");


        CreateWidgetBindCode(ref strBuilder, objPanel.transform);
        CreateDestroyWidgetCode(ref strBuilder);
        CreateDeclareCode(ref strBuilder);
        strBuilder.AppendLine("\t\tpublic Transform uiTransform = null;");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }
}
