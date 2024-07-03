
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

using System.Text;
using ET;

public partial class UICodeSpawner
{
    static public void SpawnLoopItemCode(GameObject gameObject)
    {
        Path2WidgetCachedDict?.Clear();
        Path2WidgetCachedDict = new Dictionary<string, List<Component>>();
        FindAllWidgets(gameObject.transform, "");
        SpawnCodeForScrollLoopItem(gameObject);
        SpawnCodeForScrollLoopItemBehaviour(gameObject);
        SpawnCodeForScrollLoopItemViewSystem(gameObject);
        AssetDatabase.Refresh();
    }

    static void SpawnCodeForScrollLoopItem(GameObject gameObject)
    {
        string strDlgName = $"Scroll_{gameObject.name}";
        string strFilePath = Application.dataPath + "/Scripts/Codes/HotfixView/Client/Demo/UI/UIItem/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + "/Scripts/Codes/HotfixView/Client/Demo/UI/UIItem/" + strDlgName + "/" + strDlgName + "System.cs";
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

        strBuilder.AppendFormat("\t\tpublic static void Init(this {0} self)\n", strDlgName)
            .AppendLine("\t\t{")
            .AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForScrollLoopItemViewSystem(GameObject gameObject)
    {
        if (null == gameObject)
        {
            return;
        }
        string strDlgName = gameObject.name;

        string strFilePath = Application.dataPath + "/Scripts/Codes/HotfixView/Client/Demo/UIBehaviour/UIItemBehaviour";

        if ( !System.IO.Directory.Exists(strFilePath) )
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }
        strFilePath     = Application.dataPath + "/Scripts/Codes/HotfixView/Client/Demo/UIBehaviour/UIItemBehaviour/" + strDlgName + "ViewSystem.cs";
        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");


        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class Scroll_{0}DestroySystem : DestroySystem<Scroll_{1}> \r\n", strDlgName, strDlgName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Destroy( Scroll_{0} self )",strDlgName);
        strBuilder.AppendLine("\n\t\t{");

        strBuilder.AppendFormat("\t\t\tself.DestroyWidget();\r\n");

        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }


    static void SpawnCodeForScrollLoopItemBehaviour(GameObject gameObject)
    {
        if (null == gameObject)
        {
            return;
        }
        string strDlgName = gameObject.name;

        string strFilePath = Application.dataPath + "/Scripts/Codes/ModelView/Client/Demo/UIBehaviour/UIItemBehaviour";

        if ( !System.IO.Directory.Exists(strFilePath) )
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }
        strFilePath     = Application.dataPath + "/Scripts/Codes/ModelView/Client/Demo/UIBehaviour/UIItemBehaviour/" + strDlgName + ".cs";
        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[EnableMethod]");
        strBuilder.AppendFormat("\tpublic class Scroll_{0} : Entity, IAwake, IDestroy, IUIScrollItem\r\n", strDlgName)
            .AppendLine("\t{");

        strBuilder.AppendLine("\t\tpublic long DataId {get;set;}");

        strBuilder.AppendLine("\t\tprivate bool isCacheNode = false;");
        strBuilder.AppendLine("\t\tpublic void SetCacheMode(bool isCache)");
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tthis.isCacheNode = isCache;");
        strBuilder.AppendLine("\t\t}\n");
        strBuilder.AppendFormat("\t\tpublic Scroll_{0} BindTrans(Transform trans)\r\n",strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tthis.uiTransform = trans;");
        foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
        {
            foreach (var info in pair.Value)
            {
                if (pair.Key.StartsWith(CommonUIPrefix))
                {
                    strBuilder.AppendFormat("\t\t\tif(this.m_{0} != null)\r\n", pair.Key.ToLower());
                    strBuilder.AppendLine("\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\tthis.m_{0}?.Dispose();\r\n", pair.Key.ToLower());
                    strBuilder.AppendFormat("\t\t\t\tthis.m_{0} = null;\r\n", pair.Key.ToLower());
                    strBuilder.AppendLine("\t\t\t}");
                    continue;
                }
                if (pair.Key.StartsWith(UIPagePrefix))
                {
                    strBuilder.AppendFormat("\t\t\tif( this.m_{0} != null )\r\n", pair.Key.ToLower());
                    strBuilder.AppendLine("\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\tthis.m_{0}?.Dispose();\r\n", pair.Key.ToLower());
                    strBuilder.AppendFormat("\t\t\t\tthis.m_{0} = null;\r\n", pair.Key.ToLower());
                    strBuilder.AppendLine("\t\t\t}");
                    continue;
                }
                continue;
            }
        }
        strBuilder.AppendLine("\t\t\treturn this;");
        strBuilder.AppendLine("\t\t}\n");

        CreateWidgetBindCode(ref strBuilder, gameObject.transform);
        CreateDestroyWidgetCode(ref strBuilder,true);
        CreateDeclareCode(ref strBuilder);

        strBuilder.AppendLine("\t\tpublic Transform uiTransform = null;");

        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

}
