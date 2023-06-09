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
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionHandlerComponentDestroySystem: DestroySystem<ActionHandlerComponent>
        {
            protected override void Destroy(ActionHandlerComponent self)
            {
                self.dic.Clear();
            }
        }

        public static void Load(this ActionHandlerComponent self)
        {
            self.dic.Clear();
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
        
        public static void Run(this ActionHandlerComponent self, Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            int index = actionId.IndexOf("_", 0);
            if (index == -1)
            {
                return;
            }

            string key = actionId.Substring(0, index);
            if (self.dic.ContainsKey(key) == false)
            {
                Log.Error($"not found IActionHandler: {actionId}");
                return;
            }

            Log.Debug($"ActionHandlerComponent {key}:{actionId}");
            self.dic[key].Run(unit, actionId, delayTime, selectHandle, actionContext).Coroutine();
        }
    }
}