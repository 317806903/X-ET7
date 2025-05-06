using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using ET;
using NUnit.Framework;
using UnityEngine.UI;

public partial class UICodeSpawner
{
    public static string GetPlatform()
    {
#if Platform_Mobile
        return "Platform_Mobile";
#elif Platform_Quest
        return "Platform_Quest";
#elif Platform_AVP
        return "Platform_AVP";
#else
        return "Platform_NotDefine";
#endif
    }

    public static string GetWindowTypeByPlatform()
    {
        // if (Is3DUI())
        // {
        //     return "UIWindowType.World3DRoot";
        // }
        // else
        {
            return "UIWindowType.NormalRoot";
        }
    }

    public static bool Is3DUI()
    {
#if Platform_Mobile
        return false;
#elif Platform_Quest
        return true;
#elif Platform_AVP
        return true;
#else
        return false;
#endif
    }

    static public void SpawnEUICode(GameObject gameObject)
    {
        if (null == gameObject)
        {
            Debug.LogError("UICode Select GameObject is null!");
            return;
        }

        try
        {
            string uiName = gameObject.name;
            if (uiName.StartsWith(UIPanelPrefix))
            {
                Debug.LogWarning($"----------开始生成Dlg{uiName} 相关代码 ----------");
                SpawnDlgCode(gameObject);
                Debug.LogWarning($"生成Dlg{uiName} 完毕!!!");
                return;
            }
            else if (uiName.StartsWith(CommonUIPrefix))
            {
                Debug.LogWarning($"-------- 开始生成子UI: {uiName} 相关代码 -------------");
                SpawnSubUICode(gameObject);
                Debug.LogWarning($"生成子UI: {uiName} 完毕!!!");
                return;
            }
            else if (uiName.StartsWith(UIPagePrefix))
            {
                Debug.LogWarning($"-------- 开始生成 page UI: {uiName} 相关代码 -------------");
                SpawnPageUICode(gameObject);
                Debug.LogWarning($"生成子UI: {uiName} 完毕!!!");
                return;
            }
            else if (uiName.StartsWith(UIItemPrefix))
            {
                Debug.LogWarning($"-------- 开始生成滚动列表项: {uiName} 相关代码 -------------");
                SpawnLoopItemCode(gameObject);
                Debug.LogWarning($" 开始生成滚动列表项: {uiName} 完毕！！！");
                return;
            }

            Debug.LogError($"选择的预设物不属于 Dlg, 子UI，滚动列表项，请检查 {uiName}！！！！！！");
        }
        finally
        {
            Path2WidgetCachedDict?.Clear();
            Path2WidgetCachedDict = null;
        }
    }

    static public void SpawnDlgCode(GameObject gameObject)
    {
        Path2WidgetCachedDict?.Clear();
        Path2WidgetCachedDict = new Dictionary<string, List<Component>>();

        FindAllWidgets(gameObject.transform, "");

        SpawnCodeForDlg(gameObject);
        SpawnCodeForDlgEventHandle(gameObject);
        SpawnCodeForDlgModel(gameObject);

        SpawnCodeForDlgBehaviour(gameObject);
        SpawnCodeForDlgComponentBehaviour(gameObject);

        AssetDatabase.Refresh();
    }

    static void SpawnCodeForDlg(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName + "/" + strDlgName + "System.cs";
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

        strBuilder.AppendFormat("\t\tpublic static async ETTask PreLoadWindow(this {0} self, ShowWindowData contextData, Action finished)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tif (self.IsDisposed)");
        strBuilder.AppendLine("\t\t\t{");
        strBuilder.AppendLine("\t\t\t\treturn;");
        strBuilder.AppendLine("\t\t\t}");
        strBuilder.AppendLine("\t\t\tfinished?.Invoke();");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static async ETTask ShowWindow(this {0} self, ShowWindowData contextData = null)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tself.dlgShowTime = TimeHelper.ClientNow();");
        if (UICodeSpawner.Is3DUI())
        {
            strBuilder.AppendLine("\t\t\tET.Client.UIManagerHelper.SetUIFollowHead(self, self.View.uiTransform);");
        }
        strBuilder.AppendLine("\t\t\t");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static bool ChkCanClickBg(this {0} self)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tif (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))");
        strBuilder.AppendLine("\t\t\t{");
        strBuilder.AppendLine("\t\t\t\treturn true;");
        strBuilder.AppendLine("\t\t\t}");
        strBuilder.AppendLine("\t\t\treturn false;");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendFormat("\t\tpublic static void HideWindow(this {0} self)\n", strDlgName);
        strBuilder.AppendLine("\t\t{");
        if (UICodeSpawner.Is3DUI())
        {
            strBuilder.AppendLine("\t\t\tET.Client.UIManagerHelper.RemoveUIFollow(self);");
        }
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    /// <summary>
    /// 自动生成WindowId代码
    /// </summary>
    /// <param name="gameObject"></param>
    static void SpawnWindowIdCode(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/Common/Plugins/EUI/WindowId.cs";

        if (!File.Exists(strFilePath))
        {
            Debug.LogError(" 当前不存在WindowId.cs!!!");
            return;
        }

        string windId = $"WindowID_{UICodeSpawner.GetPlatform()}_{strDlgName}";
        string originWindowIdContent = File.ReadAllText(strFilePath);
        if (originWindowIdContent.Contains(windId))
        {
            return;
        }

        int windowIdEndIndex = GetWindowIdEndIndex(originWindowIdContent);
        originWindowIdContent = originWindowIdContent.Insert(windowIdEndIndex, $"\t{windId},\n\t");
        File.WriteAllText(strFilePath, originWindowIdContent);
    }

    public static int GetWindowIdEndIndex(string content)
    {
        Regex regex = new Regex("WindowID");
        Match match = regex.Match(content);
        Regex regex1 = new Regex("}");
        MatchCollection matchCollection = regex1.Matches(content);
        for (int i = 0; i < matchCollection.Count; i++)
        {
            if (matchCollection[i].Index > match.Index)
            {
                return matchCollection[i].Index;
            }
        }

        return -1;
    }

    static void SpawnCodeForDlgEventHandle(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName + "/Event";

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName + "/Event/" + strDlgName + "EventHandler.cs";
        if (System.IO.File.Exists(strFilePath))
        {
            Debug.LogError("已存在 " + strDlgName + ".cs,将不会再次生成。");
            return;
        }

        SpawnWindowIdCode(gameObject);
        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[FriendOf(typeof(UIBaseWindow))]");
        strBuilder.AppendFormat("\t[AUIEvent(WindowID.WindowID_{0}_{1})]\n", UICodeSpawner.GetPlatform(), strDlgName);
        strBuilder.AppendFormat("\tpublic class {0}EventHandler : IAUIEventHandler\r\n", strDlgName);
        strBuilder.AppendLine("\t{");

        strBuilder.AppendLine("\t\tpublic void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)")
            .AppendLine("\t\t{");

        strBuilder.AppendFormat("\t\t\tuiBaseWindow.windowType = {0};\r\n", UICodeSpawner.GetWindowTypeByPlatform());

        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t\tpublic void OnInitComponent(UIBaseWindow uiBaseWindow)")
            .AppendLine("\t\t{");

        strBuilder.AppendFormat("\t\t\tuiBaseWindow.AddComponent<{0}>().AddComponent<{1}ViewComponent>();\r\n", strDlgName, strDlgName);

        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t\tpublic void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)")
            .AppendLine("\t\t{");

        strBuilder.AppendFormat("\t\t\tuiBaseWindow.GetComponent<{0}>().RegisterUIEvent();\r\n", strDlgName);

        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t\tpublic void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)")
            .AppendLine("\t\t{");
        strBuilder.AppendFormat(@"
			var dlg = uiBaseWindow.GetComponent<{0}>();
			if (uiBaseWindow.IsFirstLoad)
			{{
				EventSystem.Instance.Publish(dlg.DomainScene(), new ClientEventType.NoticeUIShowCommonLoading(){{bForce = true}});

				dlg.View.uiTransform.gameObject.SetActive(false);
				dlg.PreLoadWindow(contextData, () =>
				{{
					EventSystem.Instance.Publish(dlg.DomainScene(), new ClientEventType.NoticeUIHideCommonLoading(){{bForce = true}});

					dlg.View.uiTransform.gameObject.SetActive(true);
					ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
					{{
					}});
					dlg.ShowWindow(contextData).Coroutine();
				}}).Coroutine();
			}}
			else
			{{
				ET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>
				{{
				}});
				dlg.ShowWindow(contextData).Coroutine();
			}}", strDlgName)
            .AppendLine();
        strBuilder.AppendLine("\t\t}")
            .AppendLine();
        // strBuilder.AppendLine("\t\tpublic void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)")
        //     .AppendLine("\t\t{");
        // strBuilder.AppendFormat("\t\t\tvar dlg = uiBaseWindow.GetComponent<{0}>();\r\n", strDlgName);
        // strBuilder.AppendFormat("\t\t\tET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_OpenAnimationRectTransform, () =>\r\n");
        // strBuilder.AppendLine("\t\t\t{");
        // strBuilder.AppendLine("\t\t\t});");
        // strBuilder.AppendLine("\t\t\tdlg.ShowWindow(contextData).Coroutine();");
        // strBuilder.AppendLine("\t\t}")
        //     .AppendLine();

        strBuilder.AppendLine("\t\tpublic void OnHideWindow(UIBaseWindow uiBaseWindow, System.Action finished)")
            .AppendLine("\t\t{");
        strBuilder.AppendFormat("\t\t\tvar dlg = uiBaseWindow.GetComponent<{0}>();\r\n", strDlgName);
        strBuilder.AppendLine("\t\t\tdlg.HideWindow();");
        strBuilder.AppendLine("\t\t\tET.Client.UIManagerHelper.ShowUIAnimation(dlg.View.EG_CloseAnimationRectTransform, () =>");
        strBuilder.AppendLine("\t\t\t{");
        strBuilder.AppendLine("\t\t\t\tfinished?.Invoke();");
        strBuilder.AppendLine("\t\t\t});");
        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t\tpublic void BeforeUnload(UIBaseWindow uiBaseWindow)")
            .AppendLine("\t\t{");

        strBuilder.AppendLine("\t\t}")
            .AppendLine();

        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForDlgModel(GameObject gameObject)
    {
        string strDlgName = gameObject.name;
        string strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UI/" + strDlgName + "/" + strDlgName + ".cs";
        if (System.IO.File.Exists(strFilePath))
        {
            Debug.LogError("已存在 " + strDlgName + ".cs,将不会再次生成。");
            return;
        }

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[ComponentOf(typeof(UIBaseWindow))]");

        strBuilder.AppendFormat("\tpublic class {0} : Entity, IAwake, IUILogic, IUIDlg\r\n", strDlgName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendLine("\t\tpublic UnityEngine.Transform GetUITransform { get => View.uiTransform; }");
        strBuilder.AppendLine("\t\tpublic " + strDlgName + "ViewComponent View { get => this.GetComponent<" + strDlgName + "ViewComponent>(); }");
        strBuilder.AppendLine("\t\tpublic long dlgShowTime;");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForDlgBehaviour(GameObject gameObject)
    {
        if (null == gameObject)
        {
            return;
        }

        string strDlgName = gameObject.name;
        string strDlgComponentName = gameObject.name + "ViewComponent";

        string strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/" + strDlgName;

        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/HotfixView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/" + strDlgName + "/" + strDlgComponentName +
            "System.cs";

        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}AwakeSystem : AwakeSystem<{1}>\r\n", strDlgComponentName, strDlgComponentName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Awake({0} self)\n", strDlgComponentName);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tself.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;");
        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("");

        strBuilder.AppendLine("\t[ObjectSystem]");
        strBuilder.AppendFormat("\tpublic class {0}DestroySystem : DestroySystem<{1}>\r\n", strDlgComponentName, strDlgComponentName);
        strBuilder.AppendLine("\t{");
        strBuilder.AppendFormat("\t\tprotected override void Destroy({0} self)", strDlgComponentName);
        strBuilder.AppendLine("\n\t\t{");
        strBuilder.AppendFormat("\t\t\tself.DestroyWidget();\r\n");
        strBuilder.AppendLine("\t\t}");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");
        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    static void SpawnCodeForDlgComponentBehaviour(GameObject gameObject)
    {
        if (null == gameObject)
        {
            return;
        }

        string strDlgName = gameObject.name;
        string strDlgComponentName = gameObject.name + "ViewComponent";

        string strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/" + strDlgName;
        if (!System.IO.Directory.Exists(strFilePath))
        {
            System.IO.Directory.CreateDirectory(strFilePath);
        }

        strFilePath = Application.dataPath + $"/Scripts/Codes/ModelView/Client/{UICodeSpawner.GetPlatform()}/Demo/UIBehaviour/" + strDlgName + "/" + strDlgComponentName + ".cs";
        StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine()
            .AppendLine("using UnityEngine;");
        strBuilder.AppendLine("using UnityEngine.UI;");
        strBuilder.AppendLine("namespace ET.Client");
        strBuilder.AppendLine("{");
        strBuilder.AppendLine($"\t[ComponentOf(typeof({strDlgName}))]");
        strBuilder.AppendLine("\t[EnableMethod]");
        strBuilder.AppendFormat("\tpublic class {0} : Entity, IAwake, IDestroy\r\n", strDlgComponentName)
            .AppendLine("\t{");

        CreateWidgetBindCode(ref strBuilder, gameObject.transform);

        CreateDestroyWidgetCode(ref strBuilder);

        CreateDeclareCode(ref strBuilder);
        strBuilder.AppendFormat("\t\tpublic Transform uiTransform = null;\r\n");
        strBuilder.AppendLine("\t}");
        strBuilder.AppendLine("}");

        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();
    }

    public static void CreateDestroyWidgetCode(ref StringBuilder strBuilder, bool isScrollItem = false)
    {
        strBuilder.AppendFormat("\t\tpublic void DestroyWidget()");
        strBuilder.AppendLine("\n\t\t{");
        CreateDlgWidgetDisposeCode(ref strBuilder);
        strBuilder.AppendFormat("\t\t\tthis.uiTransform = null;\r\n");
        if (isScrollItem)
        {
            strBuilder.AppendLine("\t\t\tthis.DataId = 0;");
        }

        strBuilder.AppendLine("\t\t}\n");
    }

    public static void CreateDlgWidgetDisposeCode(ref StringBuilder strBuilder, bool isSelf = false)
    {
        string pointStr = isSelf? "self" : "this";
        foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
        {
            foreach (var info in pair.Value)
            {
                Component widget = info;
                string strClassType = widget.GetType().ToString();

                if (pair.Key.StartsWith(CommonUIPrefix) && pair.Key.StartsWith(NotCommonUIPrefix) == false)
                {
                    strBuilder.AppendFormat("\t\t\t{0}.m_{1}?.Dispose();\r\n", pointStr, pair.Key.ToLower());
                    strBuilder.AppendFormat("\t\t\t{0}.m_{1} = null;\r\n", pointStr, pair.Key.ToLower());
                    continue;
                }
                if (pair.Key.StartsWith(UIPagePrefix))
                {
                    strBuilder.AppendFormat("\t\t\t{0}.m_{1}?.Dispose();\r\n", pointStr, pair.Key.ToLower());
                    strBuilder.AppendFormat("\t\t\t{0}.m_{1} = null;\r\n", pointStr, pair.Key.ToLower());
                    continue;
                }

                string widgetName = widget.name + strClassType.Split('.').ToList().Last();
                strBuilder.AppendFormat("\t\t\t{0}.m_{1} = null;\r\n", pointStr, widgetName);
            }
        }
    }

    public static void CreateWidgetBindCode(ref StringBuilder strBuilder, Transform transRoot)
    {
        foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
        {
            foreach (var info in pair.Value)
            {
                Component widget = info;
                string strPath = GetWidgetPath(widget.transform, transRoot);
                string strClassType = widget.GetType().ToString();
                string strInterfaceType = strClassType;

                if (pair.Key.StartsWith(CommonUIPrefix) && pair.Key.StartsWith(NotCommonUIPrefix) == false)
                {
                    var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                    if (subUIClassPrefab == null)
                    {
                        Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                        return;
                    }

                    GetSubUIBaseWindowCode(ref strBuilder, pair.Key, strPath, subUIClassPrefab.name);
                    continue;
                }
                if (pair.Key.StartsWith(UIPagePrefix))
                {
                    var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                    if (subUIClassPrefab == null)
                    {
                        Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                        return;
                    }

                    GetPageUIBaseWindowCode(ref strBuilder, pair.Key, strPath, subUIClassPrefab.name);
                    continue;
                }

                string widgetName = widget.name + strClassType.Split('.').ToList().Last();

                strBuilder.AppendFormat("\t\tpublic {0} {1}\r\n", strInterfaceType, widgetName);
                strBuilder.AppendLine("\t\t{");
                strBuilder.AppendLine("\t\t\tget");
                strBuilder.AppendLine("\t\t\t{");

                strBuilder.AppendLine("\t\t\t\tif (this.uiTransform == null)");
                strBuilder.AppendLine("\t\t\t\t{");
                strBuilder.AppendLine("\t\t\t\t\tLog.Error(\"uiTransform is null.\");");
                strBuilder.AppendLine("\t\t\t\t\treturn null;");
                strBuilder.AppendLine("\t\t\t\t}");

                if (transRoot.gameObject.name.StartsWith(UIItemPrefix))
                {
                    strBuilder.AppendLine("\t\t\t\tif (this.isCacheNode)");
                    strBuilder.AppendLine("\t\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\t\tif( this.m_{0} == null )\n", widgetName);
                    strBuilder.AppendLine("\t\t\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\t\t\tthis.m_{0} = UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject, \"{1}\");\r\n",
                        widgetName, strPath, strInterfaceType);
                    strBuilder.AppendLine("\t\t\t\t\t}");
                    strBuilder.AppendFormat("\t\t\t\t\treturn this.m_{0};\n", widgetName);
                    strBuilder.AppendLine("\t\t\t\t}");
                    strBuilder.AppendLine("\t\t\t\telse");
                    strBuilder.AppendLine("\t\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\t\treturn UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject, \"{1}\");\r\n", widgetName,
                        strPath, strInterfaceType);
                    strBuilder.AppendLine("\t\t\t\t}");
                }
                else
                {
                    strBuilder.AppendFormat("\t\t\t\tif( this.m_{0} == null )\n", widgetName);
                    strBuilder.AppendLine("\t\t\t\t{");
                    strBuilder.AppendFormat("\t\t\t\t\tthis.m_{0} = UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject, \"{1}\");\r\n",
                        widgetName, strPath, strInterfaceType);
                    strBuilder.AppendLine("\t\t\t\t}");
                    strBuilder.AppendFormat("\t\t\t\treturn this.m_{0};\n", widgetName);
                }

                strBuilder.AppendLine("\t\t\t}");
                strBuilder.AppendLine("\t\t}\n");
            }
        }
    }

    public static void CreateDeclareCode(ref StringBuilder strBuilder)
    {
        foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
        {
            foreach (var info in pair.Value)
            {
                Component widget = info;
                string strClassType = widget.GetType().ToString();

                if (pair.Key.StartsWith(CommonUIPrefix) && pair.Key.StartsWith(NotCommonUIPrefix) == false)
                {
                    var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                    if (subUIClassPrefab == null)
                    {
                        Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                        return;
                    }

                    string subUIClassType = subUIClassPrefab.name;
                    strBuilder.AppendFormat("\t\tprivate {0} m_{1} = null;\r\n", subUIClassType, pair.Key.ToLower());
                    continue;
                }

                if (pair.Key.StartsWith(UIPagePrefix))
                {
                    var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                    if (subUIClassPrefab == null)
                    {
                        Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                        return;
                    }

                    string subUIClassType = subUIClassPrefab.name;
                    strBuilder.AppendFormat("\t\tprivate {0} m_{1} = null;\r\n", subUIClassType, pair.Key.ToLower());
                    continue;
                }

                string widgetName = widget.name + strClassType.Split('.').ToList().Last();
                strBuilder.AppendFormat("\t\tprivate {0} m_{1} = null;\r\n", strClassType, widgetName);
            }
        }
    }

    public static bool IsValidNodeName(string name)
    {
        // 检查字符串是否为空或null
        if (string.IsNullOrEmpty(name))
            return false;

        // 检查首字符是否是字母或下划线
        if (!char.IsLetter(name[0]) && name[0] != '_')
            return false;

        // 检查后续字符是否是字母、数字或下划线
        for (int i = 1; i < name.Length; i++)
        {
            if (!char.IsLetterOrDigit(name[i]) && name[i] != '_')
                return false;
        }

        return true;
    }

    public static void FindAllWidgets(Transform trans, string strPath)
    {
        if (null == trans)
        {
            return;
        }

        for (int nIndex = 0; nIndex < trans.childCount; ++nIndex)
        {
            Transform child = trans.GetChild(nIndex);

            string childName = child.name;
            bool isValidNodeName = IsValidNodeName(childName);

            string strTemp = strPath + "/" + childName;

            bool isSubUI = childName.StartsWith(CommonUIPrefix) && childName.StartsWith(NotCommonUIPrefix) == false;
            if (isSubUI == false)
            {
                isSubUI = childName.StartsWith(UIPagePrefix);
            }
            if (isSubUI == false)
            {
                bool isItemUI = childName.StartsWith(UIItemPrefix);
                if (isItemUI)
                {
                    continue;
                }
            }
            if (isSubUI)
            {
                if (isValidNodeName == false)
                {
                    Debug.LogError($"[{strPath}]下的节点名称有异常：[{childName}]");
                    continue;
                }
                List<Component> rectTransfomrComponents = new List<Component>();
                rectTransfomrComponents.Add(child.GetComponent<RectTransform>());
                Path2WidgetCachedDict.Add(childName, rectTransfomrComponents);
            }
            else if (childName.StartsWith(UIWidgetPrefix) && childName.StartsWith("Effect") == false)
            {
                if (isValidNodeName == false)
                {
                    Debug.LogError($"[{strPath}]下的节点名称有异常：[{childName}]");
                    continue;
                }
                if (childName.StartsWith(UIGameObjectPrefix))
                {
                    List<Component> rectTransfomrComponents = new List<Component>();
                    rectTransfomrComponents.Add(child.GetComponent<RectTransform>());
                    Path2WidgetCachedDict.Add(childName, rectTransfomrComponents);
                }

                foreach (var uiComponent in WidgetInterfaceList)
                {
                    Component component = child.GetComponent(uiComponent);
                    if (null == component)
                    {
                        continue;
                    }

                    if (Path2WidgetCachedDict.ContainsKey(childName))
                    {
                        Path2WidgetCachedDict[childName].Add(component);
                        continue;
                    }

                    List<Component> componentsList = new List<Component>();
                    componentsList.Add(component);
                    Path2WidgetCachedDict.Add(childName, componentsList);
                }
            }

            if (isSubUI)
            {
                Debug.Log($"遇到子UI：{childName},不生成子UI项代码");
                continue;
            }

            FindAllWidgets(child, strTemp);
        }
    }

    static string GetWidgetPath(Transform obj, Transform root)
    {
        string path = obj.name;

        while (obj.parent != null && obj.parent != root)
        {
            obj = obj.transform.parent;
            path = obj.name + "/" + path;
        }

        return path;
    }

    static void GetSubUIBaseWindowCode(ref StringBuilder strBuilder, string widget, string strPath, string subUIClassType)
    {
        strBuilder.AppendFormat("\t\tpublic {0} {1}\r\n", subUIClassType, widget);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tget");
        strBuilder.AppendLine("\t\t\t{");

        strBuilder.AppendLine("\t\t\t\tif (this.uiTransform == null)");
        strBuilder.AppendLine("\t\t\t\t{");
        strBuilder.AppendLine("\t\t\t\t\tLog.Error(\"uiTransform is null.\");");
        strBuilder.AppendLine("\t\t\t\t\treturn null;");
        strBuilder.AppendLine("\t\t\t\t}");

        strBuilder.AppendFormat("\t\t\t\tif( this.m_{0} == null )\n", widget.ToLower());
        strBuilder.AppendLine("\t\t\t\t{");
        strBuilder.AppendFormat("\t\t\t\t\tTransform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, \"{0}\");\r\n",
            strPath);
        strBuilder.AppendFormat("\t\t\t\t\tthis.m_{0} = this.AddChild<{1}, Transform>(subTrans);\r\n", widget.ToLower(), subUIClassType);
        strBuilder.AppendLine("\t\t\t\t}");
        strBuilder.AppendFormat("\t\t\t\treturn this.m_{0};\n", widget.ToLower());
        strBuilder.AppendLine("\t\t\t}");

        strBuilder.AppendLine("\t\t}\n");
    }

    static void GetPageUIBaseWindowCode(ref StringBuilder strBuilder, string widget, string strPath, string subUIClassType)
    {
        strBuilder.AppendFormat("\t\tpublic {0} {1}\r\n", subUIClassType, widget);
        strBuilder.AppendLine("\t\t{");
        strBuilder.AppendLine("\t\t\tget");
        strBuilder.AppendLine("\t\t\t{");

        strBuilder.AppendLine("\t\t\t\tif (this.uiTransform == null)");
        strBuilder.AppendLine("\t\t\t\t{");
        strBuilder.AppendLine("\t\t\t\t\tLog.Error(\"uiTransform is null.\");");
        strBuilder.AppendLine("\t\t\t\t\treturn null;");
        strBuilder.AppendLine("\t\t\t\t}");

        strBuilder.AppendFormat("\t\t\t\tif( this.m_{0} == null )\n", widget.ToLower());
        strBuilder.AppendLine("\t\t\t\t{");
        strBuilder.AppendFormat("\t\t\t\t\tTransform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, \"{0}\");\r\n",
            strPath);
        strBuilder.AppendFormat("\t\t\t\t\tthis.m_{0} = this.AddChild<{1}, Transform>(subTrans);\r\n", widget.ToLower(), subUIClassType);
        strBuilder.AppendLine("\t\t\t\t}");
        strBuilder.AppendFormat("\t\t\t\treturn this.m_{0};\n", widget.ToLower());
        strBuilder.AppendLine("\t\t\t}");

        strBuilder.AppendLine("\t\t}\n");
    }

    static UICodeSpawner()
    {
        WidgetInterfaceList = new List<System.Type>();
        WidgetInterfaceList.Add(typeof(Button));
        WidgetInterfaceList.Add(typeof(Text));
        WidgetInterfaceList.Add(typeof(TMPro.TextMeshProUGUI));
        WidgetInterfaceList.Add(typeof(TMPro.TMP_InputField));
        //WidgetInterfaceList.Add(typeof(Input));
        WidgetInterfaceList.Add(typeof(InputField));
        WidgetInterfaceList.Add(typeof(Scrollbar));
        WidgetInterfaceList.Add(typeof(ToggleGroup));
        WidgetInterfaceList.Add(typeof(Toggle));
        WidgetInterfaceList.Add(typeof(Dropdown));
        WidgetInterfaceList.Add(typeof(TMPro.TMP_Dropdown));
        WidgetInterfaceList.Add(typeof(Slider));
        WidgetInterfaceList.Add(typeof(ScrollRect));
        WidgetInterfaceList.Add(typeof(Image));
        WidgetInterfaceList.Add(typeof(RawImage));
        WidgetInterfaceList.Add(typeof(Canvas));
        WidgetInterfaceList.Add(typeof(LoopVerticalScrollRect));
        WidgetInterfaceList.Add(typeof(LoopHorizontalScrollRect));
        WidgetInterfaceList.Add(typeof(UnityEngine.EventSystems.EventTrigger));
        WidgetInterfaceList.Add(typeof(UITextLocalizeMonoView));
        WidgetInterfaceList.Add(typeof(SuperScrollView.LoopListView2));
        WidgetInterfaceList.Add(typeof(UnityEngine.Video.VideoPlayer));
    }

    private static Dictionary<string, List<Component>> Path2WidgetCachedDict = null;
    private static List<System.Type> WidgetInterfaceList = null;
    private const string CommonUIPrefix = "ES";
    private const string NotCommonUIPrefix = "ESprite";
    private const string UIPanelPrefix = "Dlg";
    private const string UIPagePrefix = "EPage";
    private const string UIWidgetPrefix = "E";
    private const string UIGameObjectPrefix = "EG";
    private const string UIItemPrefix = "Item";
}