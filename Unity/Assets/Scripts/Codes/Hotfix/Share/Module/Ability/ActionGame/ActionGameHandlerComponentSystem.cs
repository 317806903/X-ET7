using System;
using System.Collections.Generic;
using System.Reflection;

namespace ET.Ability
{
    [FriendOf(typeof (ActionGameHandlerComponent))]
    public static class ActionGameHandlerComponentSystem
    {
        [ObjectSystem]
        public class ActionGameHandlerComponentAwakeSystem: AwakeSystem<ActionGameHandlerComponent>
        {
            protected override void Awake(ActionGameHandlerComponent self)
            {
                self.dic = new();
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionGameHandlerComponentDestroySystem: DestroySystem<ActionGameHandlerComponent>
        {
            protected override void Destroy(ActionGameHandlerComponent self)
            {
                self.dic.Clear();
            }
        }

        public static void Load(this ActionGameHandlerComponent self)
        {
            self.dic.Clear();
            var types = EventSystem.Instance.GetTypes(typeof (ActionGameHandlerAttribute));
            foreach (Type type in types)
            {
                if (type.Name.StartsWith("ActionGame_") == false)
                {
                    Log.Error($"it is not startWith Action_: {type.Name}");
                    continue;
                }
                IActionGameHandler iActionGameHandler = Activator.CreateInstance(type) as IActionGameHandler;
                if (iActionGameHandler == null)
                {
                    Log.Error($"it is not IActionGameHandler: {type.Name}");
                    continue;
                }
                self.dic.Add(type.Name.Replace("ActionGame_", ""), iActionGameHandler);
            }
        }

        public static async ETTask Run(this ActionGameHandlerComponent self, string actionId, ActionGameContext actionGameContext)
        {
            int index = actionId.IndexOf("_", 0);
            if (index == -1)
            {
                return;
            }

            string key = actionId.Substring(0, index);
            if (self.dic.ContainsKey(key) == false)
            {
                Log.Error($"not found IActionGameHandler: {actionId}");
                return;
            }
#if UNITY_EDITOR
            //Log.Debug($"---ActionGameHandlerComponent {key}:{actionId}");
#endif
            await self.dic[key].Run(self.DomainScene(), actionId, actionGameContext);
        }
    }
}