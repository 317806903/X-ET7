using System;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(Unity.Mathematics.float3))]
    public class MathematicsFloat3TypeDrawer: ITypeDrawer
    {
        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            return EditorGUILayout.Vector3Field(memberName, (Unity.Mathematics.float3) value);
        }
    }
}