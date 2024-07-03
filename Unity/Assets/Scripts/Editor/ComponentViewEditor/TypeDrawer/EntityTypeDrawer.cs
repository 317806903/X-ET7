using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ET
{
    [TypeDrawer(typeof(Entity), true)]
    public class EntityTypeDrawer: ITypeDrawer
    {
        public bool HandlesType(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            if (type.GetGenericTypeDefinition() == typeof (Entity))
            {
                return true;
            }

            return false;
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
#if ENABLE_VIEW
            Entity entity = (Entity)value;
            GameObject go = entity?.viewGO;
            EditorGUILayout.ObjectField(memberName, go, memberType, true);
#endif
            return value;
        }
    }
}