#if UNITY_EDITOR
using System;
using ET;
using UnityEngine;
using UnityEditor;
//CustomEditor 自定义编辑器
//描述了用于编辑器实时运行类型的一个编辑器类。
//注意：这是一个编辑器类，如果想使用它你需要把它放到工程目录下的Assets/Editor文件夹下。
//编辑器类在UnityEditor命名空间下。所以当使用C#脚本时，你需要在脚本前面加上 "using UnityEditor"引用。
[CustomEditor(typeof(UIRedDotMonoView))]
public class UIRedDotMonoViewEditor : Editor
{

    UIRedDotMonoView m_Target;

    void Awake()
    {
        this.m_Target = target as UIRedDotMonoView;
    }

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(15);
        if (string.IsNullOrEmpty(this.m_Target.UIRedDotType))
        {
            EditorGUI.BeginChangeCheck();
            UIRedDotType _UIRedDotTypeNew = (UIRedDotType)EditorGUILayout.EnumPopup("UIRedDotType: ", UIRedDotType.None);
            if (EditorGUI.EndChangeCheck())
            {
                this.m_Target.UIRedDotType = _UIRedDotTypeNew.ToString();
            }
        }
        else if(Enum.TryParse<UIRedDotType>(this.m_Target.UIRedDotType, out UIRedDotType _UIRedDotType))
        {
            EditorGUI.BeginChangeCheck();
            UIRedDotType _UIRedDotTypeNew = (UIRedDotType)EditorGUILayout.EnumPopup("UIRedDotType: ", _UIRedDotType);
            if (EditorGUI.EndChangeCheck())
            {
                this.m_Target.UIRedDotType = _UIRedDotTypeNew.ToString();
            }
        }
        else
        {
            this.m_Target.UIRedDotType = EditorGUILayout.TextField("UIRedDotType: ", this.m_Target.UIRedDotType);
        }

        this.m_Target.RedDotScale = EditorGUILayout.Vector3Field("RedDotScale: ", this.m_Target.RedDotScale);
        this.m_Target.PositionOffset = EditorGUILayout.Vector2Field("PositionOffset: ", this.m_Target.PositionOffset);

        if (GUILayout.Button("Save"))
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("请注意", $"当前是运行模式,无法修改", "OK");
                return;
            }

            UnityEditor.EditorUtility.SetDirty(this.m_Target);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.DisplayDialog("修改成功", $"修改成功", "OK");
        }
        // EditorGUILayout.Space(10);
        // serializedObject.Update();
        //

        EditorGUILayout.Space(15);
    }
}
#endif