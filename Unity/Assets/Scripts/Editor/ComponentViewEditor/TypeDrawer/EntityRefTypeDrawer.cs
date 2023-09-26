using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(Entity), true)]
    public class EntityRefTypeDrawer: ITypeDrawer
    {
        public bool HandlesType(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            if (type.GetGenericTypeDefinition() == typeof (EntityRef<>))
            {
                return true;
            }

            return false;
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            FieldInfo fieldInfo = memberType.GetField("entity", BindingFlags.NonPublic | BindingFlags.Instance);
#if ENABLE_VIEW
            Entity entity = (Entity)fieldInfo.GetValue(value);
            GameObject go = entity?.viewGO;
            EditorGUILayout.ObjectField(memberName, go, memberType, true);
#endif
            return value;
        }
    }
}