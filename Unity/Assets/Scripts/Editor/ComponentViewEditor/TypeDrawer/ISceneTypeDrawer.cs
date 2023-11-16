#if ENABLE_VIEW

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(IScene))]
    public class ISceneTypeDrawer: ITypeDrawer
    {
        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            Entity iScene = (Entity)value;
            EditorGUILayout.ObjectField(memberName, iScene.viewGO, memberType, true);
            return value;
        }
    }
}
#endif