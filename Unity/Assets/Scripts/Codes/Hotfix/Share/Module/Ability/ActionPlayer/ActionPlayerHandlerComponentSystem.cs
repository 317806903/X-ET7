using System;
using System.Collections.Generic;
using System.Reflection;

namespace ET.Ability
{
    [FriendOf(typeof (ActionPlayerHandlerComponent))]
    public static class ActionPlayerHandlerComponentSystem
    {
        [ObjectSystem]
        public class ActionPlayerHandlerComponentAwakeSystem: AwakeSystem<ActionPlayerHandlerComponent>
        {
            protected override void Awake(ActionPlayerHandlerComponent self)
            {
                self.dic = new();
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionPlayerHandlerComponentDestroySystem: DestroySystem<ActionPlayerHandlerComponent>
        {
            protected override void Destroy(ActionPlayerHandlerComponent self)
            {
                self.dic.Clear();
            }
        }

        public static void Load(this ActionPlayerHandlerComponent self)
        {
            self.dic.Clear();
            var types = EventSystem.Instance.GetTypes(typeof (ActionPlayerHandlerAttribute));
            foreach (Type type in types)
            {
                if (type.Name.StartsWith("ActionPlayer_") == false)
                {
                    Log.Error($"it is not startWith Action_: {type.Name}");
                    continue;
                }
                IActionPlayerHandler iActionPlayerHandler = Activator.CreateInstance(type) as IActionPlayerHandler;
                if (iActionPlayerHandler == null)
                {
                    Log.Error($"it is not IActionPlayerHandler: {type.Name}");
                    continue;
                }
                self.dic.Add(type.Name.Replace("ActionPlayer_", ""), iActionPlayerHandler);
            }
        }

        public static async ETTask Run(this ActionPlayerHandlerComponent self, long playerId, string actionId, ActionPlayerContext actionPlayerContext)
        {
            int index = actionId.IndexOf("_", 0);
            if (index == -1)
            {
                return;
            }

            string key = actionId.Substring(0, index);
            if (self.dic.ContainsKey(key) == false)
            {
                Log.Error($"not found IActionPlayerHandler: {actionId}");
                return;
            }
#if UNITY_EDITOR
            //Log.Debug($"---ActionPlayerHandlerComponent {key}:{actionId}");
#endif
            await self.dic[key].Run(self.DomainScene(), playerId, actionId, actionPlayerContext);
        }
    }
}