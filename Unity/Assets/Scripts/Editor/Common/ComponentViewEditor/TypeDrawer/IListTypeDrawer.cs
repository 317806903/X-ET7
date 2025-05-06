#if ENABLE_VIEW

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(IList), true)]
    public class IListTypeDrawer : ITypeDrawer
    {

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            var state = GUIElementStateManager.Add(GUIElementStateDomain.ComponentView, value);
            var list = value as IList;
            state[1] = EditorGUILayout.Foldout(state[1], $"{memberName}({list.Count})", "FoldoutHeader");
            if (state[1])
            {
                EditorGUILayout.BeginVertical("FrameBox");
                var index = 0;
                var argTypes = memberType.GetGenericArguments();
                foreach (var val in list)
                {
                    ComponentViewHelper.Draw(argTypes[0], val, $"{index++}");
                }
                EditorGUILayout.EndVertical();
            }
            return value;
        }
    }
}
#endif