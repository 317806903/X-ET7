using System;
using System.Collections.Generic;
using System.Reflection;

namespace ET.Ability
{
    [FriendOf(typeof (ActionHandlerComponent))]
    public static class ActionHandlerComponentSystem
    {
        [ObjectSystem]
        public class ActionHandlerComponentAwakeSystem: AwakeSystem<ActionHandlerComponent>
        {
            protected override void Awake(ActionHandlerComponent self)
            {
                ActionHandlerComponent.Instance = self;
                self.dic = new();
            }
        }

        [ObjectSystem]
        public class ActionHandlerComponentDestroySystem: DestroySystem<ActionHandlerComponent>
        {
            protected override void Destroy(ActionHandlerComponent self)
            {
                ActionHandlerComponent.Instance = null;
                self.dic.Clear();
            }
        }

        public static void Run(this ActionHandlerComponent self, string actionId, long fromUnitId, long toUnitId)
        {
            int index = actionId.IndexOf("_", 0);
            if (index == -1)
            {
                return;
            }

            string key = actionId.Substring(0, index);
            if (self.dic.ContainsKey(key) == false)
            {
                Type type = typeof (ActionHandlerComponentSystem);
                MethodInfo info = type.GetMethod("Run", BindingFlags.NonPublic | BindingFlags.Static);
                self.dic[key] = info.CreateDelegate(typeof (Action<ActionHandlerComponent, string, long, long>), null) as Action<ActionHandlerComponent, string, long, long>;
            }

            self.dic[key](self, actionId, fromUnitId, toUnitId);
        }

        public static void ActionHandler_AddBuffToCaster(this ActionHandlerComponent self, string actionId, long fromUnitId, long toUnitId)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityEventType.SummonUnit() { unitId = fromUnitId, cfgId = actionId, });
        }
    }
}