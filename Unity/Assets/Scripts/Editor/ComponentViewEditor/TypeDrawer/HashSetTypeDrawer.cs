#if ENABLE_VIEW

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace ET
{
    [TypeDrawer(typeof(HashSet<>))]
    public class HashSetTypeDrawer : ITypeDrawer
    {
        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            var argTypes = memberType.GetGenericArguments();
            var tmp = value as IEnumerable;
            var count = 0;
            foreach (var val in tmp)
            {
                count++;
            }
            var state = GUIElementStateManager.Add(GUIElementStateDomain.ComponentView, value);
            state[1] = EditorGUILayout.Foldout(state[1], $"{memberName}({count}) {memberType.Name} {argTypes[0].Name}", "FoldoutHeader");
            if (state[1])
            {
                EditorGUILayout.BeginVertical("FrameBox");
                var list = value as IEnumerable;
                var index = 0;
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