using System;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(Unity.Mathematics.quaternion))]
    public class MathematicsQuaternionTypeDrawer: ITypeDrawer
    {
        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            return EditorGUILayout.Vector4Field(memberName, (Vector4)((Unity.Mathematics.quaternion)value).value);
        }
    }
}
