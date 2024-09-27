#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
//CustomEditor 自定义编辑器
//描述了用于编辑器实时运行类型的一个编辑器类。
//注意：这是一个编辑器类，如果想使用它你需要把它放到工程目录下的Assets/Editor文件夹下。
//编辑器类在UnityEditor命名空间下。所以当使用C#脚本时，你需要在脚本前面加上 "using UnityEditor"引用。
[CustomEditor(typeof(UIImageLocalizeMonoView))]
public class UIImageLocalizeMonoViewEditor : Editor
{
    string curPath;
    Rect rect;
    SerializedProperty Path;
    SerializedProperty LocalizeImageScaleType;

    UIImageLocalizeMonoView m_Target;

    void Awake()
    {
        this.m_Target = target as UIImageLocalizeMonoView;
        Path = serializedObject.FindProperty("path");
        LocalizeImageScaleType = serializedObject.FindProperty("localizeImageScaleType");
        InitCurPath();
    }

    void OnEnable()
    {
    }

    public void InitCurPath()
    {
        if (string.IsNullOrEmpty(Path.stringValue) == false)
        {
            curPath = this.m_Target.GetImagePathWhenEditor("None", Path.stringValue);
        }
        else
        {
            if (this.m_Target.mImage != null)
            {
                Sprite sprite = null;
                if (this.m_Target.mImage.sprite != null)
                {
                    sprite = this.m_Target.mImage.sprite;
                }

                if (sprite != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(sprite);
                    (bool bRet, string relativePath) = ChkIsValidate(assetPath);
                    if (bRet)
                    {
                        curPath = assetPath;
                        Path.stringValue = relativePath;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
            else if (this.m_Target.mRawImage != null)
            {
                Texture texture = null;
                if (this.m_Target.mRawImage.texture != null)
                {
                    texture = this.m_Target.mRawImage.texture;
                }

                if (texture != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(texture);
                    (bool bRet, string relativePath) = ChkIsValidate(assetPath);
                    if (bRet)
                    {
                        curPath = assetPath;
                        Path.stringValue = relativePath;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("当前相对路径:", Path.stringValue);

        EditorGUILayout.Space(15);

        EditorGUILayout.LabelField("拖拉图片到此区域: ");
        rect = EditorGUILayout.GetControlRect(GUILayout.Width(500), GUILayout.Height(50));
        EditorGUI.TextField(rect, curPath);

        //如果鼠标正在拖拽中或拖拽结束时，并且鼠标所在位置在文本输入框内
        if ((Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragExited)&& rect.Contains(Event.current.mousePosition))
        {
            //改变鼠标的外表
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
            {
                string retPath = DragAndDrop.paths[0];
                (bool bRet, string relativePath) = ChkIsValidate(retPath);
                if (bRet)
                {
                    curPath = retPath;
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", $"路径必须是: {UIImageLocalizeMonoView.UI_MultiLanguagePath}", "OK");
                }
            }
        }

        if (GUILayout.Button("Save Path"))
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("请注意", $"当前是运行模式,无法修改", "OK");
                return;
            }
            (bool bRet, string relativePath) = ChkIsValidate(curPath);
            if (bRet)
            {
                if (Path.stringValue == relativePath)
                {
                    EditorUtility.DisplayDialog("请注意", $"没有改变", "OK");
                }
                else
                {
                    string tmp = Path.stringValue;
                    Path.stringValue = relativePath;
                    serializedObject.ApplyModifiedProperties();
                    UnityEditor.EditorUtility.SetDirty(this.m_Target);
                    EditorUtility.DisplayDialog("修改成功", $"由[{tmp}]改为[{relativePath}]", "OK");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", $"路径必须是: {UIImageLocalizeMonoView.UI_MultiLanguagePath}", "OK");
            }
        }
        EditorGUILayout.Space(10);
        serializedObject.Update();
        EditorGUILayout.PropertyField(LocalizeImageScaleType);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(15);

        if (GUILayout.Button("DoChangeLanguage_CN"))
        {
            this.m_Target.LoadImage("CN");
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("DoChangeLanguage_EN"))
        {
            this.m_Target.LoadImage("EN");
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("DoChangeLanguage_TW"))
        {
            this.m_Target.LoadImage("TW");
        }
    }

    public (bool bRet, string relativePath) ChkIsValidate(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return (false, "");
        }
        if (path.ToLower().EndsWith(".png")||path.ToLower().EndsWith(".jpg"))
        {
            if (path.StartsWith($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/CN/"))
            {
                return (true, path.Replace($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/CN/", ""));
            }
            if (path.StartsWith($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/EN/"))
            {
                return (true, path.Replace($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/EN/", ""));
            }
            if (path.StartsWith($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/TW/"))
            {
                return (true, path.Replace($"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/TW/", ""));
            }

        }
        return (false, "");
    }
}
#endif