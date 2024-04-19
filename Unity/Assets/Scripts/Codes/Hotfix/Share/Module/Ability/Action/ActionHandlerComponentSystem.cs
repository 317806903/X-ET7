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
                self.dic = new();
                self.actionId2ActionHandle = new();
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionHandlerComponentDestroySystem: DestroySystem<ActionHandlerComponent>
        {
            protected override void Destroy(ActionHandlerComponent self)
            {
                self.dic.Clear();
                self.actionId2ActionHandle.Clear();
            }
        }

        public static void Load(this ActionHandlerComponent self)
        {
            self.dic.Clear();
            self.actionId2ActionHandle.Clear();
            var types = EventSystem.Instance.GetTypes(typeof (ActionHandlerAttribute));
            foreach (Type type in types)
            {
                if (type.Name.StartsWith("Action_") == false)
                {
                    Log.Error($"it is not startWith Action_: {type.Name}");
                    continue;
                }
                IActionHandler iActionHandler = Activator.CreateInstance(type) as IActionHandler;
                if (iActionHandler == null)
                {
                    Log.Error($"it is not IActionHandler: {type.Name}");
                    continue;
                }
                self.dic.Add(type.Name.Replace("Action_", ""), iActionHandler);
            }
        }

        public static IActionHandler GetActionHandle(this ActionHandlerComponent self, string actionId)
        {
            if (self.actionId2ActionHandle.TryGetValue(actionId, out IActionHandler actionHandler) == false)
            {
                int index = actionId.IndexOf("_", 0);
                if (index == -1)
                {
                    actionHandler = null;
#if UNITY_EDITOR
                    Log.Error($"actionId[{actionId}] not contain _");
#endif
                }
                else
                {
                    string key = actionId.Substring(0, index);
                    if (self.dic.TryGetValue(key, out actionHandler) == false)
                    {
                        actionHandler = null;
#if UNITY_EDITOR
                        Log.Error($"actionId[{actionId}] key[{key}] not define");
#endif
                    }
                }

                self.actionId2ActionHandle[actionId] = actionHandler;
            }

            return actionHandler;
        }

        public static async ETTask Run(this ActionHandlerComponent self, Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            IActionHandler actionHandler = self.GetActionHandle(actionId);
            if (actionHandler == null)
            {
                return;
            }

            selectHandle.SetHolding(true);
            await actionHandler.Run(unit, resetPosByUnit, actionId, delayTime, selectHandle, actionContext);
            selectHandle.SetHolding(false);

            ET.Ability.UnitHelper.AddRecycleSelectHandles(self.DomainScene(), selectHandle);
        }
    }
}