#if UNITY_EDITOR
using System;
using NLog.Fluent;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CanEditMultipleObjects, CustomEditor(typeof(UITextLocalizeMonoView))]
public class UITextLocalizeMonoViewEditor: Editor
{
    private UITextLocalizeMonoView m_Target;
    private void OnEnable()
    {
        m_Target = target as UITextLocalizeMonoView;
    }

    public override void OnInspectorGUI()
    {
        // 绘制全部原有属性
        base.DrawDefaultInspector();
        if (string.IsNullOrEmpty(m_Target.textKey))
        {
            m_Target.DoChangeLanguage_ResetKey();
            UnityEditor.EditorUtility.SetDirty(this.m_Target);
        }

        EditorGUILayout.Space(15);

        if (GUILayout.Button("DoChangeLanguage_ResetKey"))
        {
            m_Target.DoChangeLanguage_ResetKey();
            UnityEditor.EditorUtility.SetDirty(this.m_Target);
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("DoChangeLanguage_ReShowValue"))
        {
            m_Target.DoChangeLanguage_ReShowValue();
        }
        EditorGUILayout.Space(10);

        if (GUILayout.Button("DoChangeLanguage_CN"))
        {
            m_Target.DoChangeLanguage_Force("CN");
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("DoChangeLanguage_EN"))
        {
            m_Target.DoChangeLanguage_Force("EN");
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("DoChangeLanguage_TW"))
        {
            m_Target.DoChangeLanguage_Force("TW");
        }
    }

}

#endif