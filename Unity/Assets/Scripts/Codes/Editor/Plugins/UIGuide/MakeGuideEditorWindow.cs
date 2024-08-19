using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using ET;
using ET.Client;
using UIGuide;

[Serializable]
public class UIGuidePathListEditor
{
    public List<UIGuidePath> list;
}

public class UIGuidePathListEditorWindow: EditorWindow
{
    [MenuItem("新手指引/编写指引步骤")]
    static void AddScript()
    {
        EditorWindow.GetWindow<UIGuidePathListEditorWindow>("指引步骤添加");
    }

    private string saveRelativePath = "Bundles/Config/UIGuideConfig";

    UIGuidePathListEditor makeGuide;

    string filePath;

    float reFreshTime = 0;
    Vector2 vScroll = new Vector2(0, 0);

    private bool showFoldout;
    private int indexRun = 0;
    private bool[] showFoldoutList;

    GUIStyle redStyle = new GUIStyle();

    void Awake()
    {
        // 设置文本颜色
        redStyle.normal.textColor = Color.red;
    }

    void OnGUI()
    {
        bool reFresh = false;
        //if (GlobalFunc.ChkTimeInterval(ref reFreshTime, 0.2f))
        {
            reFresh = true;
        }

        EditorGUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("新建"))
        {
            makeGuide = new();
            makeGuide.list = new();
            filePath = "未保存";
            GUI.FocusControl(null);
        }
        if (GUILayout.Button("读取文件(.asset)"))
        {
            Load();
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndHorizontal();

        if (makeGuide == null)
        {
            makeGuide = new();
            makeGuide.list = new();
        }
        int count = makeGuide.list.Count;
        if (showFoldoutList == null)
        {
            showFoldoutList = new bool[count];
            for (int i = 0; i < count; i++)
            {
                showFoldoutList[i] = false;
            }
        }
        if (count > 0)
        {
            EditorGUILayout.BeginHorizontal();

            showFoldout = EditorGUILayout.Foldout(showFoldout, $"指引步骤({count}):");
            if (GUILayout.Button("展开所有步骤"))
            {
                GUI.FocusControl(null);
                showFoldout = true;
                for (int i = 0; i < showFoldoutList.Length; i++)
                {
                    showFoldoutList[i] = true;
                }
            }
            if (GUILayout.Button("收起所有步骤"))
            {
                GUI.FocusControl(null);
                showFoldout = true;
                for (int i = 0; i < showFoldoutList.Length; i++)
                {
                    showFoldoutList[i] = false;
                }
            }

            if (GUILayout.Button("在最后插入步骤", GUILayout.Width(100)))
            {
                makeGuide.list.Add(new UIGuidePath());

                var showFoldoutListTmp = new bool[makeGuide.list.Count];
                for (int i = 0; i < showFoldoutList.Length; i++)
                {
                    showFoldoutListTmp[i] = showFoldoutList[i];
                }
                showFoldoutListTmp[showFoldoutListTmp.Length-1] = false;
                showFoldoutList = showFoldoutListTmp;
                this.showFoldout = true;
                vScroll = vScroll + new Vector2(0, 5000);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            if (showFoldout)
            {
                vScroll = GUILayout.BeginScrollView(vScroll, false, true);
                EditorGUILayout.Space(5);
                EditorGUI.indentLevel++;

                for (int i = 0; i < count; i++)
                {
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();

                    showFoldoutList[i] = EditorGUILayout.Foldout(showFoldoutList[i], $"----------步骤{i}:{makeGuide.list[i].name}");
                    if (showFoldoutList[i])
                    {
                        if (GUILayout.Button($"收起({i})", GUILayout.Width(70)))
                        {
                            showFoldoutList[i] = false;
                            GUI.FocusControl(null);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button($"展开({i})", GUILayout.Width(70)))
                        {
                            showFoldoutList[i] = true;
                            GUI.FocusControl(null);
                        }
                    }
                    if (GUILayout.Button("插入New", GUILayout.Width(70)))
                    {
                        this.makeGuide.list.Insert(i, new UIGuidePath());

                        var showFoldoutListTmp = new bool[makeGuide.list.Count];
                        for (int j = 0; j < i; j++)
                        {
                            showFoldoutListTmp[j] = showFoldoutList[j];
                        }
                        for (int j = i; j < showFoldoutList.Length; j++)
                        {
                            showFoldoutListTmp[j+1] = showFoldoutList[j];
                        }
                        showFoldoutListTmp[i] = false;
                        showFoldoutList = showFoldoutListTmp;

                        GUI.FocusControl(null);

                        EditorGUILayout.EndHorizontal();

                        GUILayout.EndScrollView();
                        return;
                    }
                    if (GUILayout.Button("复制", GUILayout.Width(50)))
                    {
                        string json = ET.JsonHelper.ToJson(this.makeGuide.list[i]);
                        this.makeGuide.list.Insert(i, ET.JsonHelper.FromJson<UIGuidePath>(json));

                        var showFoldoutListTmp = new bool[makeGuide.list.Count];
                        for (int j = 0; j < i; j++)
                        {
                            showFoldoutListTmp[j] = showFoldoutList[j];
                        }
                        for (int j = i; j < showFoldoutList.Length; j++)
                        {
                            showFoldoutListTmp[j+1] = showFoldoutList[j];
                        }
                        showFoldoutListTmp[i] = false;
                        showFoldoutList = showFoldoutListTmp;

                        GUI.FocusControl(null);

                        EditorGUILayout.EndHorizontal();

                        GUILayout.EndScrollView();
                        return;
                    }
                    if (GUILayout.Button("上移", GUILayout.Width(50)))
                    {
                        if (i > 0)
                        {
                            var UIGuidePath = this.makeGuide.list[i - 1];
                            var UIGuidePath2 = this.makeGuide.list[i];
                            this.makeGuide.list[i - 1] = UIGuidePath2;
                            this.makeGuide.list[i] = UIGuidePath;

                            var showFoldoutTmp = showFoldoutList[i - 1];
                            var showFoldoutTmp2 = showFoldoutList[i];
                            showFoldoutList[i - 1] = showFoldoutTmp2;
                            showFoldoutList[i] = showFoldoutTmp;

                            GUI.FocusControl(null);

                            EditorGUILayout.EndHorizontal();

                            GUILayout.EndScrollView();
                            return;
                        }

                    }
                    if (GUILayout.Button("下移", GUILayout.Width(50)))
                    {
                        if (i < this.makeGuide.list.Count - 1)
                        {
                            var UIGuidePath = this.makeGuide.list[i + 1];
                            var UIGuidePath2 = this.makeGuide.list[i];
                            this.makeGuide.list[i + 1] = UIGuidePath2;
                            this.makeGuide.list[i] = UIGuidePath;

                            var showFoldoutTmp = showFoldoutList[i + 1];
                            var showFoldoutTmp2 = showFoldoutList[i];
                            showFoldoutList[i + 1] = showFoldoutTmp2;
                            showFoldoutList[i] = showFoldoutTmp;

                            GUI.FocusControl(null);

                            EditorGUILayout.EndHorizontal();

                            GUILayout.EndScrollView();
                            return;
                        }

                    }
                    if (GUILayout.Button("删除", GUILayout.Width(50)))
                    {
                        makeGuide.list.RemoveAt(i);
                        GUI.FocusControl(null);

                        var showFoldoutListTmp = new bool[makeGuide.list.Count];
                        for (int i1 = 0; i1 < i; i1++)
                        {
                            showFoldoutListTmp[i1] = showFoldoutList[i1];
                        }
                        for (int i1 = i+1; i1 < showFoldoutList.Length; i1++)
                        {
                            showFoldoutListTmp[i1-1] = showFoldoutList[i1];
                        }
                        showFoldoutList = showFoldoutListTmp;
                        EditorGUILayout.EndHorizontal();

                        GUILayout.EndScrollView();
                        return;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (showFoldoutList[i])
                    {
                        EditorGUI.indentLevel++;

                        makeGuide.list[i].name = EditorGUILayout.TextField("名称(仅显示用): ", makeGuide.list[i].name);

                        EditorGUILayout.Space(5);
                        makeGuide.list[i].isFreeClickBeforeEnter = EditorGUILayout.Toggle("★进入前是否自由点击", makeGuide.list[i].isFreeClickBeforeEnter);
                        EditorGUILayout.Space(5);
                        makeGuide.list[i].trigEnterCondition = (TrigCondition) EditorGUILayout.EnumPopup("★进入条件", makeGuide.list[i].trigEnterCondition);

                        EditorGUI.indentLevel++;
                        if (makeGuide.list[i].trigEnterCondition == TrigCondition.None)
                        {
                        }
                        else if (makeGuide.list[i].trigEnterCondition == TrigCondition.FindNode)
                        {
                            EditorGUI.BeginChangeCheck();
                            makeGuide.list[i].trigEnterConditionGo = EditorGUILayout.ObjectField("存在节点:", makeGuide.list[i].trigEnterConditionGo, typeof (GameObject), true) as GameObject;

                            EditorGUI.indentLevel++;
                            if (EditorGUI.EndChangeCheck())
                            {
                                if (makeGuide.list[i].trigEnterConditionGo != null)
                                {
                                    string path = GetHierarchyPath(makeGuide.list[i].trigEnterConditionGo.transform);
                                    // makeGuide.list[i].trigEnterConditionParam = GetHierarchyCanvasPath(makeGuide.list[i].trigEnterConditionGo.transform);
                                    // makeGuide.list[i].hierarchyCanvasPath = path.Substring(0, path.Length - makeGuide.list[i].hierarchyGuidePath.Length - 1);
                                    makeGuide.list[i].trigEnterConditionParam = path;
                                    EditorGUILayout.LabelField("路径:" + makeGuide.list[i].trigEnterConditionParam);
                                }
                            }
                            else
                            {
                                if (reFresh)
                                {
                                    if (string.IsNullOrEmpty(makeGuide.list[i].trigEnterConditionParam))
                                    {
                                        makeGuide.list[i].trigEnterConditionGo = null;
                                        makeGuide.list[i].trigEnterConditionParam = "";
                                    }
                                    else
                                    {
                                        makeGuide.list[i].trigEnterConditionGo = GameObject.Find(makeGuide.list[i].trigEnterConditionParam);
                                    }
                                }

                                if (makeGuide.list[i].trigEnterConditionGo == null)
                                {
                                    EditorGUILayout.LabelField("路径(可能有问题，留意!!!): " + makeGuide.list[i].trigEnterConditionParam, redStyle);
                                }
                                else
                                {
                                    EditorGUILayout.LabelField("路径: " + makeGuide.list[i].trigEnterConditionParam);
                                }
                            }
                            EditorGUI.indentLevel--;

                        }
                        else if (makeGuide.list[i].trigEnterCondition == TrigCondition.StaticMethod)
                        {
                            ET.Client.GuideConditionStaticMethodType trigEnterConditionStaticMethod;
                            if (string.IsNullOrEmpty(makeGuide.list[i].trigEnterConditionStaticMethod))
                            {
                                trigEnterConditionStaticMethod = GuideConditionStaticMethodType.None;
                            }
                            else
                            {
                                bool bRet = Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(makeGuide.list[i].trigEnterConditionStaticMethod, out trigEnterConditionStaticMethod);
                                if (bRet == false)
                                {
                                    trigEnterConditionStaticMethod = GuideConditionStaticMethodType.None;
                                }
                            }
                            trigEnterConditionStaticMethod = (ET.Client.GuideConditionStaticMethodType) EditorGUILayout.EnumPopup("检查逻辑", trigEnterConditionStaticMethod);

                            makeGuide.list[i].trigEnterConditionStaticMethod = trigEnterConditionStaticMethod.ToString();
                            if (trigEnterConditionStaticMethod != GuideConditionStaticMethodType.None)
                            {
                                if (string.IsNullOrEmpty(makeGuide.list[i].trigEnterConditionParam))
                                {
                                    makeGuide.list[i].trigEnterConditionParam = "";
                                }
                                makeGuide.list[i].trigEnterConditionParam = EditorGUILayout.TextField("参数: ", makeGuide.list[i].trigEnterConditionParam);
                                makeGuide.list[i].trigEnterConditionParam = makeGuide.list[i].trigEnterConditionParam.Trim();
                            }
                        }
                        EditorGUI.indentLevel--;

                        EditorGUILayout.Space(5);
                        EditorGUILayout.LabelField("★指引内容:");
                        EditorGUI.indentLevel++;
                        EditorGUI.BeginChangeCheck();
                        makeGuide.list[i].go = EditorGUILayout.ObjectField("指引节点:", makeGuide.list[i].go, typeof (GameObject), true) as GameObject;

                        EditorGUI.indentLevel++;
                        if (EditorGUI.EndChangeCheck())
                        {
                            if (makeGuide.list[i].go != null)
                            {
                                string path = GetHierarchyPath(makeGuide.list[i].go.transform);
                                makeGuide.list[i].hierarchyGuidePath = GetHierarchyCanvasPath(makeGuide.list[i].go.transform);
                                if (path == makeGuide.list[i].hierarchyGuidePath)
                                {
                                    makeGuide.list[i].hierarchyCanvasPath = makeGuide.list[i].hierarchyGuidePath;
                                    makeGuide.list[i].hierarchyGuidePath = "";
                                }
                                else
                                {
                                    makeGuide.list[i].hierarchyCanvasPath = path.Substring(0, path.Length - makeGuide.list[i].hierarchyGuidePath.Length - 1);
                                }

                                EditorGUILayout.LabelField("canvas路径: " + makeGuide.list[i].hierarchyCanvasPath);
                                EditorGUILayout.LabelField("后面路径:" + makeGuide.list[i].hierarchyGuidePath);
                            }
                            else
                            {
                                makeGuide.list[i].hierarchyCanvasPath = "";
                                makeGuide.list[i].hierarchyGuidePath = "";
                                EditorGUILayout.LabelField("canvas路径: " + makeGuide.list[i].hierarchyCanvasPath);
                                EditorGUILayout.LabelField("后面路径:" + makeGuide.list[i].hierarchyGuidePath);
                            }
                        }
                        else
                        {
                            if (reFresh)
                            {
                                if (string.IsNullOrEmpty(makeGuide.list[i].hierarchyCanvasPath))
                                {
                                    makeGuide.list[i].go = null;
                                    makeGuide.list[i].hierarchyCanvasPath = "";
                                    makeGuide.list[i].hierarchyGuidePath = "";
                                }
                                else
                                {
                                    makeGuide.list[i].go = GameObject.Find(makeGuide.list[i].hierarchyCanvasPath + "/" + makeGuide.list[i].hierarchyGuidePath);
                                }
                            }

                            if (makeGuide.list[i].go == null)
                            {
                                EditorGUILayout.LabelField("canvas路径(可能有问题，留意!!!): " + makeGuide.list[i].hierarchyCanvasPath, redStyle);
                                EditorGUILayout.LabelField("后面路径(可能有问题，留意!!!):" + makeGuide.list[i].hierarchyGuidePath, redStyle);
                            }
                            else
                            {
                                EditorGUILayout.LabelField("canvas路径: " + makeGuide.list[i].hierarchyCanvasPath);
                                EditorGUILayout.LabelField("后面路径:" + makeGuide.list[i].hierarchyGuidePath);
                            }
                        }
                        EditorGUI.indentLevel--;

                        ////////

                        makeGuide.list[i].guideTextType = (GuideTextType) EditorGUILayout.EnumPopup("指引文字类型", makeGuide.list[i].guideTextType);

                        EditorGUI.indentLevel++;
                        if (makeGuide.list[i].guideTextType == GuideTextType.None)
                        {
                        }
                        else if (makeGuide.list[i].guideTextType == GuideTextType.Text)
                        {
                            makeGuide.list[i].text = EditorGUILayout.TextField("指引文字(key): ", makeGuide.list[i].text);
                            makeGuide.list[i].text = makeGuide.list[i].text.Trim();
                            if (string.IsNullOrEmpty(this.makeGuide.list[i].text) == false)
                            {
                                EditorGUI.indentLevel++;
                                if (LocalizeComponent.Instance != null)
                                {
                                    string txtTmpCN = LocalizeComponent.Instance.GetTextValue(LanguageType.CN, makeGuide.list[i].text);
                                    EditorGUILayout.LabelField("翻译CN: ", txtTmpCN);
                                    string txtTmp = LocalizeComponent.Instance.GetTextValue(makeGuide.list[i].text);
                                    EditorGUILayout.LabelField("翻译当前语言: ", txtTmp);
                                }
                                else
                                {
                                    EditorGUILayout.LabelField("翻译当前语言: 运行时才能正确显示");
                                }
                                EditorGUI.indentLevel--;
                            }
                        }
                        else if(makeGuide.list[i].guideTextType == GuideTextType.Image)
                        {
                            EditorGUI.BeginChangeCheck();
                            makeGuide.list[i].guideTextImg =
                                EditorGUILayout.ObjectField("指引图片:", makeGuide.list[i].guideTextImg, typeof (Sprite), false) as Sprite;
                            if (EditorGUI.EndChangeCheck())
                            {
                                if (makeGuide.list[i].guideTextImg != null)
                                {
                                    string reallyPath = AssetDatabase.GetAssetPath(makeGuide.list[i].guideTextImg.GetInstanceID());

                                    makeGuide.list[i].guideTextImgPath = reallyPath;
                                }
                                else
                                {
                                    makeGuide.list[i].guideTextImgPath = "";
                                }
                            }
                            else
                            {
                                if (reFresh)
                                {
                                    if (string.IsNullOrEmpty(makeGuide.list[i].guideTextImgPath))
                                    {
                                        makeGuide.list[i].guideTextImg = null;
                                        makeGuide.list[i].guideTextImgPath = "";
                                    }
                                    else
                                    {
                                        makeGuide.list[i].guideTextImg = AssetDatabase.LoadAssetAtPath<Sprite>(makeGuide.list[i].guideTextImgPath);
                                    }
                                }
                            }
                            EditorGUI.indentLevel++;
                            EditorGUILayout.LabelField("图片路径:" + makeGuide.list[i].guideTextImgPath);
                            EditorGUI.indentLevel--;
                        }
                        EditorGUI.indentLevel--;
                        ////////

                        makeGuide.list[i].specImgMatchType = (SpecImgMatchType) EditorGUILayout.EnumPopup("指定特定图片", makeGuide.list[i].specImgMatchType);

                        EditorGUI.indentLevel++;
                        if (makeGuide.list[i].specImgMatchType == SpecImgMatchType.None)
                        {
                        }
                        else
                        {
                            EditorGUI.BeginChangeCheck();
                            makeGuide.list[i].specImg =
                                EditorGUILayout.ObjectField("特定图片:", makeGuide.list[i].specImg, typeof (Sprite), false) as Sprite;
                            if (EditorGUI.EndChangeCheck())
                            {
                                if (makeGuide.list[i].specImg != null)
                                {
                                    string reallyPath = AssetDatabase.GetAssetPath(makeGuide.list[i].specImg.GetInstanceID());

                                    makeGuide.list[i].specImgPath = reallyPath;
                                }
                                else
                                {
                                    makeGuide.list[i].specImgPath = "";
                                }
                            }
                            else
                            {
                                if (reFresh)
                                {
                                    if (string.IsNullOrEmpty(makeGuide.list[i].specImgPath))
                                    {
                                        makeGuide.list[i].specImg = null;
                                        makeGuide.list[i].specImgPath = "";
                                    }
                                    else
                                    {
                                        makeGuide.list[i].specImg = AssetDatabase.LoadAssetAtPath<Sprite>(makeGuide.list[i].specImgPath);
                                    }
                                }
                            }
                            EditorGUI.indentLevel++;
                            EditorGUILayout.LabelField("图片路径:" + makeGuide.list[i].specImgPath);
                            EditorGUI.indentLevel--;
                        }
                        EditorGUI.indentLevel--;


                        makeGuide.list[i].curInNextType = (CurInNextType) EditorGUILayout.EnumPopup("0点击,1按下", makeGuide.list[i].curInNextType);
                        makeGuide.list[i].waitToNextUIGuideStep = (WaitToNextUIGuideStep) EditorGUILayout.EnumPopup("下一步骤前(0罩黑,1透明,2无遮罩,3罩黑但是不会产生点击)", makeGuide.list[i].waitToNextUIGuideStep);

                        if (makeGuide.list[i].audioClip == null && string.IsNullOrEmpty(makeGuide.list[i].audioPath) == false)
                        {
                            makeGuide.list[i].audioClip =
                                AssetDatabase.LoadAssetAtPath("Assets/" + makeGuide.list[i].audioPath, typeof (AudioClip)) as AudioClip;
                        }

                        EditorGUI.BeginChangeCheck();
                        makeGuide.list[i].audioClip =
                            EditorGUILayout.ObjectField("指引语音:", makeGuide.list[i].audioClip, typeof (AudioClip), false) as AudioClip;
                        if (EditorGUI.EndChangeCheck())
                        {
                            if (makeGuide.list[i].audioClip != null)
                            {
                                string reallyPath = AssetDatabase.GetAssetPath(makeGuide.list[i].audioClip.GetInstanceID());

                                makeGuide.list[i].audioPath = reallyPath;
                            }
                            else
                            {
                                makeGuide.list[i].audioPath = "";
                            }
                        }
                        else
                        {
                            if (reFresh)
                            {
                                if (string.IsNullOrEmpty(makeGuide.list[i].audioPath))
                                {
                                    makeGuide.list[i].audioClip = null;
                                    makeGuide.list[i].audioPath = "";
                                }
                                else
                                {
                                    makeGuide.list[i].audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(makeGuide.list[i].audioPath);
                                }
                            }
                        }
                        EditorGUI.indentLevel++;
                        EditorGUILayout.LabelField("音效路径:" + makeGuide.list[i].audioPath);
                        EditorGUI.indentLevel--;
                        {
                            ET.Client.GuideExecuteStaticMethodType guideExecuteStaticMethod;
                            if (string.IsNullOrEmpty(makeGuide.list[i].guideExecuteStaticMethod))
                            {
                                guideExecuteStaticMethod = GuideExecuteStaticMethodType.None;
                            }
                            else
                            {
                                bool bRet = Enum.TryParse<ET.Client.GuideExecuteStaticMethodType>(makeGuide.list[i].guideExecuteStaticMethod, out guideExecuteStaticMethod);
                                if (bRet == false)
                                {
                                    guideExecuteStaticMethod = GuideExecuteStaticMethodType.None;
                                }
                            }
                            guideExecuteStaticMethod = (ET.Client.GuideExecuteStaticMethodType) EditorGUILayout.EnumPopup("执行逻辑", guideExecuteStaticMethod);

                            makeGuide.list[i].guideExecuteStaticMethod = guideExecuteStaticMethod.ToString();

                            if (guideExecuteStaticMethod != GuideExecuteStaticMethodType.None)
                            {
                                makeGuide.list[i].guideExecuteParam = EditorGUILayout.TextField("参数: ", makeGuide.list[i].guideExecuteParam);
                                if (string.IsNullOrEmpty(makeGuide.list[i].guideExecuteParam))
                                {
                                    makeGuide.list[i].guideExecuteParam = "";
                                }
                                else
                                {
                                    makeGuide.list[i].guideExecuteParam = makeGuide.list[i].guideExecuteParam.Trim();
                                }
                            }
                        }
                        EditorGUI.indentLevel--;

                        EditorGUILayout.Space(5);
                        makeGuide.list[i].trigExitCondition = (TrigCondition) EditorGUILayout.EnumPopup("★完成条件", makeGuide.list[i].trigExitCondition);
                        EditorGUI.indentLevel++;
                        if (makeGuide.list[i].trigExitCondition == TrigCondition.None)
                        {
                        }
                        else if (makeGuide.list[i].trigExitCondition == TrigCondition.FindNode)
                        {
                            EditorGUI.BeginChangeCheck();
                            makeGuide.list[i].trigExitConditionGo = EditorGUILayout.ObjectField("存在节点:", makeGuide.list[i].trigExitConditionGo, typeof (GameObject), true) as GameObject;

                            EditorGUI.indentLevel++;
                            if (EditorGUI.EndChangeCheck())
                            {
                                if (makeGuide.list[i].trigExitConditionGo != null)
                                {
                                    string path = GetHierarchyPath(makeGuide.list[i].trigExitConditionGo.transform);
                                    // makeGuide.list[i].trigEnterConditionParam = GetHierarchyCanvasPath(makeGuide.list[i].trigEnterConditionGo.transform);
                                    // makeGuide.list[i].hierarchyCanvasPath = path.Substring(0, path.Length - makeGuide.list[i].hierarchyGuidePath.Length - 1);
                                    makeGuide.list[i].trigExitConditionParam = path;
                                    EditorGUILayout.LabelField("路径:" + makeGuide.list[i].trigExitConditionParam);
                                }
                            }
                            else
                            {
                                if (reFresh)
                                {
                                    if (string.IsNullOrEmpty(makeGuide.list[i].trigExitConditionParam))
                                    {
                                        makeGuide.list[i].trigExitConditionGo = null;
                                        makeGuide.list[i].trigExitConditionParam = "";
                                    }
                                    else
                                    {
                                        makeGuide.list[i].trigExitConditionGo = GameObject.Find(makeGuide.list[i].trigExitConditionParam);
                                    }
                                }

                                if (makeGuide.list[i].trigExitConditionGo == null)
                                {
                                    EditorGUILayout.LabelField("路径(可能有问题，留意!!!): " + makeGuide.list[i].trigExitConditionParam, redStyle);
                                }
                                else
                                {
                                    EditorGUILayout.LabelField("路径: " + makeGuide.list[i].trigExitConditionParam);
                                }
                            }
                            EditorGUI.indentLevel--;

                        }
                        else if (makeGuide.list[i].trigExitCondition == TrigCondition.StaticMethod)
                        {
                            ET.Client.GuideConditionStaticMethodType trigExitConditionStaticMethod;
                            if (string.IsNullOrEmpty(makeGuide.list[i].trigExitConditionStaticMethod))
                            {
                                trigExitConditionStaticMethod = GuideConditionStaticMethodType.None;
                            }
                            else
                            {
                                bool bRet = Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(makeGuide.list[i].trigExitConditionStaticMethod, out trigExitConditionStaticMethod);
                                if (bRet == false)
                                {
                                    trigExitConditionStaticMethod = GuideConditionStaticMethodType.None;
                                }
                            }
                            trigExitConditionStaticMethod = (ET.Client.GuideConditionStaticMethodType) EditorGUILayout.EnumPopup("检查逻辑", trigExitConditionStaticMethod);

                            makeGuide.list[i].trigExitConditionStaticMethod = trigExitConditionStaticMethod.ToString();

                            if (trigExitConditionStaticMethod != GuideConditionStaticMethodType.None)
                            {
                                if (string.IsNullOrEmpty(makeGuide.list[i].trigExitConditionParam))
                                {
                                    makeGuide.list[i].trigExitConditionParam = "";
                                }
                                makeGuide.list[i].trigExitConditionParam = EditorGUILayout.TextField("参数: ", makeGuide.list[i].trigExitConditionParam);
                                makeGuide.list[i].trigExitConditionParam = makeGuide.list[i].trigExitConditionParam.Trim();
                            }
                        }
                        EditorGUI.indentLevel--;

                        EditorGUI.indentLevel--;

                        //EditorGUILayout.LabelField("-----------------------");
                        EditorGUILayout.Space(5);

                    }
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(5);
                GUILayout.EndScrollView();

            }
        }
        else
        {
            EditorGUILayout.LabelField("步骤总数量：" + count);
            EditorGUILayout.LabelField("=============================================");
            if (GUILayout.Button("插入New", GUILayout.Width(100)))
            {
                makeGuide.list.Clear();
                makeGuide.list.Add(new UIGuidePath());
                showFoldoutList = new bool[makeGuide.list.Count];
                showFoldoutList[showFoldoutList.Length-1] = false;
                this.showFoldout = true;
                vScroll = vScroll + new Vector2(0, 5000);
            }
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("=============================================");
        EditorGUILayout.LabelField("文件路径：");
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField(filePath);
        EditorGUI.indentLevel--;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("打开存储目录"))
        {
            OpenSavePath();
        }

        if (GUILayout.Button("定位到存储目录"))
        {
            LocationSavePath();
        }

        if (GUILayout.Button("保存"))
        {
            Save();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        this.indexRun = EditorGUILayout.IntSlider(this.indexRun, 0, count-1);
        if (GUILayout.Button($"从步骤{this.indexRun}开始执行当前指引"))
        {
            this.DoGuideCurFile(this.indexRun);
        }
        if (Application.isPlaying)
        {
            UIGuidePath curUIGuidePath = GetCurUIGuide();
            if (curUIGuidePath != null)
            {
                EditorGUILayout.LabelField($"当前运行的指引步骤:{curUIGuidePath.index}");
            }
            else
            {
                EditorGUILayout.LabelField($"当前没有指引");
            }
        }
        else
        {
            EditorGUILayout.LabelField($"非运行状态");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("执行byFile"))
        {
            DoGuideFileByFile();
        }
        if (GUILayout.Button("停止"))
        {
            StopGuideFile();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(15);
    }

    string hierarchyCanvasPath;

    string GetHierarchyCanvasPath(Transform t, bool initPath = true)
    {
        if (initPath) hierarchyCanvasPath = "";
        hierarchyCanvasPath = t.name + hierarchyCanvasPath;
        if (t.parent == null)
        {
            return hierarchyCanvasPath;
        }

        Canvas canvas = t.parent.GetComponent<Canvas>();
        //if (canvas == null || canvas.isRootCanvas == false)
        if (canvas == null || canvas.gameObject.GetComponent<UnityEngine.UI.CanvasScaler>() == false)
        {
            Transform parent = t.parent;
            hierarchyCanvasPath = "/" + hierarchyCanvasPath;
            GetHierarchyCanvasPath(parent, false);
        }

        return hierarchyCanvasPath;
    }

    string hierarchyPath;

    string GetHierarchyPath(Transform t, bool initPath = true)
    {
        if (initPath) hierarchyPath = "";
        hierarchyPath = t.name + hierarchyPath;
        if (t.parent == null)
        {
            return hierarchyPath;
        }

        Transform parent = t.parent;
        hierarchyPath = "/" + hierarchyPath;
        GetHierarchyPath(parent, false);
        return hierarchyPath;
    }

    void OpenSavePath()
    {
        string savePath = Path.Combine(Application.dataPath, this.saveRelativePath);
        EditorUtility.RevealInFinder(savePath + "/");
    }

    void LocationSavePath()
    {
        string savePath = Path.Combine("Assets", this.saveRelativePath).Replace("\\", "/");
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(savePath);
        UnityEditor.EditorGUIUtility.PingObject(obj);
        //UnityEditor.Selection.activeObject = obj;
    }

    void Load()
    {
        string savePath = Path.Combine(Application.dataPath, this.saveRelativePath);
        string filePathSelect = EditorUtility.OpenFilePanel("读取文件(.asset)", savePath, "asset");
        if (string.IsNullOrEmpty(filePathSelect))
        {
            return;
        }
        this.filePath = filePathSelect.Replace(Application.dataPath, "Assets");
        var makeGuide1 = AssetDatabase.LoadAssetAtPath<UIGuidePathList>(filePath);
        if (makeGuide1 == null)
        {
            return;
        }
        this.makeGuide = new();
        this.makeGuide.list = new();
        for (int i = 0; i < makeGuide1.list.Count; i++)
        {
            string json = ET.JsonHelper.ToJson(makeGuide1.list[i]);
            this.makeGuide.list.Add(ET.JsonHelper.FromJson<UIGuidePath>(json));
        }

        this.showFoldout = true;
        this.showFoldoutList = new bool[makeGuide1.list.Count];
        for (int i = 0; i < makeGuide1.list.Count; i++)
        {
            showFoldoutList[i] = false;
        }

        this.indexRun = 0;
    }

    bool ChkStaticMethod()
    {
        for (int i = 0; i < this.makeGuide.list.Count; i++)
        {
            UIGuidePath _UIGuidePath = this.makeGuide.list[i];
            if (_UIGuidePath.trigEnterCondition == TrigCondition.StaticMethod)
            {
                if (Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(_UIGuidePath.trigEnterConditionStaticMethod, out var trigEnterConditionStaticMethod))
                {
                    bool bRet = ET.Client.UIGuideHelper.ChkStaticMethodParam(trigEnterConditionStaticMethod, _UIGuidePath.trigEnterConditionParam);
                    if (bRet == false)
                    {
                        string message = $"[{i} {_UIGuidePath.name}] trigEnterConditionStaticMethod[{_UIGuidePath.trigEnterConditionStaticMethod}] param[{_UIGuidePath.trigEnterConditionParam}] err";
                        if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                        {
                        }
                        return false;
                    }
                }
                else
                {
                    string message = $"[{i} {_UIGuidePath.name}] trigEnterConditionStaticMethod[{_UIGuidePath.trigEnterConditionStaticMethod}] param[{_UIGuidePath.trigEnterConditionParam}] err";
                    if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                    {
                    }
                    return false;
                }
            }
            if (_UIGuidePath.trigExitCondition == TrigCondition.StaticMethod)
            {
                if (Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(_UIGuidePath.trigExitConditionStaticMethod, out var trigExitConditionStaticMethod))
                {
                    bool bRet = ET.Client.UIGuideHelper.ChkStaticMethodParam(trigExitConditionStaticMethod, _UIGuidePath.trigExitConditionParam);
                    if (bRet == false)
                    {
                        string message = $"[{i} {_UIGuidePath.name}] trigExitConditionStaticMethod[{_UIGuidePath.trigExitConditionStaticMethod}] param[{_UIGuidePath.trigExitConditionParam}] err";
                        if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                        {
                        }
                        return false;
                    }
                }
                else
                {
                    string message = $"[{i} {_UIGuidePath.name}] trigExitConditionStaticMethod[{_UIGuidePath.trigExitConditionStaticMethod}] param[{_UIGuidePath.trigExitConditionParam}] err";
                    if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                    {
                    }
                    return false;
                }
            }

            if (string.IsNullOrEmpty(_UIGuidePath.guideExecuteStaticMethod) == false)
            {
                if (Enum.TryParse<ET.Client.GuideExecuteStaticMethodType>(_UIGuidePath.guideExecuteStaticMethod, out var guideExecuteStaticMethod))
                {
                    bool bRet = ET.Client.UIGuideHelper.ChkStaticMethodExecuteParam(guideExecuteStaticMethod, _UIGuidePath.guideExecuteParam);
                    if (bRet == false)
                    {
                        string message = $"[{i} {_UIGuidePath.name}] guideExecuteStaticMethod[{_UIGuidePath.guideExecuteStaticMethod}] param[{_UIGuidePath.guideExecuteParam}] err";
                        if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                        {
                        }
                        return false;
                    }
                }
                else
                {
                    string message = $"[{i} {_UIGuidePath.name}] guideExecuteStaticMethod[{_UIGuidePath.guideExecuteStaticMethod}] param[{_UIGuidePath.guideExecuteParam}] err";
                    if (EditorUtility.DisplayDialog("检查错误", message, "确定"))
                    {
                    }
                    return false;
                }
            }
        }
        return true;
    }

    void Save()
    {
        if (this.ChkStaticMethod() == false)
        {
            return;
        }

        string curFileName = "";
        if (string.IsNullOrEmpty(filePath) == false)
        {
            curFileName = Path.GetFileNameWithoutExtension(filePath);
        }

        string savePath = Path.Combine(Application.dataPath, this.saveRelativePath);
        filePath = EditorUtility.SaveFilePanel("Save", savePath, curFileName, "asset");
        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        this.filePath = this.filePath.Replace(Application.dataPath, "Assets");
        var _UIGuidePathList = ScriptableObject.CreateInstance<UIGuidePathList>();
        _UIGuidePathList.list = new();
        for (int i = 0; i < this.makeGuide.list.Count; i++)
        {
            string json = ET.JsonHelper.ToJson(this.makeGuide.list[i]);
            _UIGuidePathList.list.Add(ET.JsonHelper.FromJson<UIGuidePath>(json));
        }

        AssetDatabase.CreateAsset(_UIGuidePathList, filePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("保存成功");
    }

    public void DoGuideCurFile(int indexRun = 0)
    {
        if (Application.isPlaying == false)
        {
            return;
        }

        Scene clientScene = null;
        var childs = ClientSceneManagerComponent.Instance.Children;
        foreach (var child in childs.Values)
        {
            Scene scene = (Scene)child;
            if (scene.SceneType == SceneType.Client)
            {
                clientScene = scene;
                break;
            }
        }

        if (clientScene != null)
        {
            var _UIGuidePathList = ScriptableObject.CreateInstance<UIGuidePathList>();
            _UIGuidePathList.list = new();
            for (int i = 0; i < this.makeGuide.list.Count; i++)
            {
                string json = ET.JsonHelper.ToJson(this.makeGuide.list[i]);
                UIGuidePath _UIGuidePath = ET.JsonHelper.FromJson<UIGuidePath>(json);
                _UIGuidePath.index = i;
                _UIGuidePathList.list.Add(_UIGuidePath);
            }

            string guideFileName = Path.GetFileNameWithoutExtension(this.filePath);
            ET.Client.UIGuideHelper.DoUIGuide(clientScene, guideFileName, _UIGuidePathList, indexRun, null).Coroutine();
        }
    }

    public void DoGuideFileByFile()
    {
        if (Application.isPlaying == false)
        {
            return;
        }

        string savePath = Path.Combine(Application.dataPath, this.saveRelativePath);
        string filePathSelect = EditorUtility.OpenFilePanel("选择文件(.asset)", savePath, "asset");
        if (string.IsNullOrEmpty(filePathSelect))
        {
            return;
        }
        //string fileName = filePathSelect.Replace(Application.dataPath, "Assets");
        string fileName = Path.GetFileNameWithoutExtension(filePathSelect);

        Scene clientScene = null;
        var childs = ClientSceneManagerComponent.Instance.Children;
        foreach (var child in childs.Values)
        {
            Scene scene = (Scene)child;
            if (scene.SceneType == SceneType.Client)
            {
                clientScene = scene;
                break;
            }
        }

        if (clientScene != null)
        {
            ET.Client.UIGuideHelper.DoUIGuide(clientScene, fileName, 0, null).Coroutine();
        }
    }

    public void StopGuideFile()
    {
        if (Application.isPlaying == false)
        {
            return;
        }

        Scene clientScene = null;
        var childs = ClientSceneManagerComponent.Instance.Children;
        foreach (var child in childs.Values)
        {
            Scene scene = (Scene)child;
            if (scene.SceneType == SceneType.Client)
            {
                clientScene = scene;
                break;
            }
        }

        if (clientScene != null)
        {
            ET.Client.UIGuideHelper.StopUIGuide(clientScene).Coroutine();
        }
    }

    public UIGuidePath GetCurUIGuide()
    {
        if (Application.isPlaying == false)
        {
            return null;
        }

        if (ClientSceneManagerComponent.Instance == null)
        {
            return null;
        }
        Scene clientScene = null;
        var childs = ClientSceneManagerComponent.Instance.Children;
        foreach (var child in childs.Values)
        {
            Scene scene = (Scene)child;
            if (scene.SceneType == SceneType.Client)
            {
                clientScene = scene;
                break;
            }
        }

        if (clientScene != null)
        {
            return ET.Client.UIGuideHelper.GetCurUIGuide(clientScene);
        }

        return null;
    }

}