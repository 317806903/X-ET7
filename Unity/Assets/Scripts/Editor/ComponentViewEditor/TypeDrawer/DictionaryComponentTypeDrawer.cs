#if ENABLE_VIEW
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(DictionaryComponent<,>), true)]
    public class DictionaryComponentTypeDrawer : ITypeDrawer
    {
        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            var state = GUIElementStateManager.Add(GUIElementStateDomain.ComponentView, value);
            var dic = value as IDictionary;
            state[1] = EditorGUILayout.Foldout(state[1], $"{memberName}({dic.Count})", "FoldoutHeader");
            if (state[1])
            {
                EditorGUILayout.BeginVertical("box");
                var width = EditorGUIUtility.currentViewWidth;
                EditorGUILayout.BeginHorizontal("AC BoldHeader");
                var argTypes = memberType.GetGenericArguments();
                EditorGUILayout.LabelField($"Key ({argTypes[0].Name})", GUILayout.Width(width / 2));
                EditorGUILayout.LabelField($"Value ({argTypes[1].Name})", GUILayout.Width(width / 2));
                EditorGUILayout.EndHorizontal();
                foreach (var key in dic.Keys)
                {
                    EditorGUILayout.BeginHorizontal("OL box flat");
                    // if (GUILayout.Button(key.ToString(), GUILayout.Width(width / 2)))
                    // {
                    //
                    // }

                    // string tmp1 = "";
                    // try
                    // {
                    //     tmp1 = dic[key]?.ToString();
                    // }
                    // catch (Exception e)
                    // {
                    //     tmp1 = "Error Null";
                    // }
                    // if (GUILayout.Button(tmp1 ?? "Null", GUILayout.Width(width / 2)))
                    // {
                    //
                    // }

                    GUILayout.TextField(key.ToString());
                    ComponentViewHelper.Draw(dic[key].GetType(), dic[key], dic[key].GetType().Name);

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            return value;
        }
    }
}
#endif
