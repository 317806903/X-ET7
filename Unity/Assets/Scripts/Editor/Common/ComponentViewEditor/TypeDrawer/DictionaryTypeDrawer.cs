#if ENABLE_VIEW
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(IDictionary), true)]
    public class DictionaryTypeDrawer : ITypeDrawer
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
                bool isNumericDic = false;
                if (memberName == "NumericDic")
                {
                    isNumericDic = true;
                }

                if (isNumericDic == false)
                {
                    foreach (var key in dic.Keys)
                    {
                        EditorGUILayout.BeginHorizontal("OL box flat");
                        GUILayout.TextField(key.ToString(), GUILayout.MaxWidth(160));
                        ComponentViewHelper.Draw(dic[key].GetType(), dic[key], dic[key].GetType().Name);
                        EditorGUILayout.EndHorizontal();
                    }
                }
                else
                {
                    var type = Type.GetType("ET.NumericType, Unity.Model.Codes, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                    var publicFields = type.GetFields();
                    Dictionary<string, string> numDic = new();
                    foreach (var key in dic.Keys)
                    {
                        foreach (var info in publicFields)
                        {
                            if ((int)info.GetValue(null) == (int)key)
                            {
                                numDic[$"{info.Name} {key.ToString()}"] = $"{((long)dic[key])*0.0001f}";
                                break;
                            }
                        }
                    }
                    Dictionary<string, string> numDicAsc = numDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                    foreach (var item in numDicAsc)
                    {
                        EditorGUILayout.BeginHorizontal("OL box flat");

                        GUILayout.TextField(item.Key, GUILayout.Width(200));
                        GUILayout.TextField(item.Value);

                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
            }
            return value;
        }
    }
}
#endif