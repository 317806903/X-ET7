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
        }
    }

}

#endif