using System.Collections.Generic;
using System;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(ActionFromHttpManagerComponent))]
    public static class ActionFromHttpManagerComponentSystem
    {
        [ObjectSystem]
        public class ActionFromHttpManagerComponentAwakeSystem : AwakeSystem<ActionFromHttpManagerComponent>
        {
            protected override void Awake(ActionFromHttpManagerComponent self)
            {
                self.dic = new();
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionFromHttpManagerComponentDestroySystem: DestroySystem<ActionFromHttpManagerComponent>
        {
            protected override void Destroy(ActionFromHttpManagerComponent self)
            {
                self.dic.Clear();
            }
        }

        public static void Load(this ActionFromHttpManagerComponent self)
        {
            self.dic.Clear();
            var types = EventSystem.Instance.GetTypes(typeof (ActionFromHttpHandlerAttribute));
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(ActionFromHttpHandlerAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                ActionFromHttpHandlerAttribute actionFromHttpHandlerAttribute = (ActionFromHttpHandlerAttribute)attrs[0];

                IActionFromHttpHandler iActionHandler = Activator.CreateInstance(type) as IActionFromHttpHandler;
                if (iActionHandler == null)
                {
                    Log.Error($"it is not IActionHandler: {type.Name}");
                    continue;
                }
                self.dic.Add(actionFromHttpHandlerAttribute.actionFromHttpStatus, iActionHandler);
            }
        }

        public static async ETTask<(bool bRet, string msg)> Run(this ActionFromHttpManagerComponent self, ActionFromHttpStatus actionFromHttpStatus, Dictionary<string, string> paramDic)
        {
            self.dic.TryGetValue(actionFromHttpStatus, out var actionFromHttpHandler);
            if (actionFromHttpHandler == null)
            {
                return (false, "");
            }

            return await actionFromHttpHandler.Run(self.DomainScene(), paramDic);
        }

    }
}