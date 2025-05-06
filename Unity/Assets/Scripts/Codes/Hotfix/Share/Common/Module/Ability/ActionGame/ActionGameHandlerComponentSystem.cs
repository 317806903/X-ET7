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
                self.actionIdIsChk = new();
                self.actionId2ActionHandle = new();
                self.Load();
            }
        }

        [ObjectSystem]
        public class ActionGameHandlerComponentDestroySystem: DestroySystem<ActionGameHandlerComponent>
        {
            protected override void Destroy(ActionGameHandlerComponent self)
            {
                self.dic.Clear();
                self.actionIdIsChk.Clear();
                self.actionId2ActionHandle.Clear();
            }
        }

        public static void Load(this ActionGameHandlerComponent self)
        {
            self.dic.Clear();
            self.actionIdIsChk.Clear();
            self.actionId2ActionHandle.Clear();
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

        public static IActionGameHandler GetActionHandle(this ActionGameHandlerComponent self, string actionId)
        {
            if (self.actionId2ActionHandle.TryGetValue(actionId, out IActionGameHandler actionHandler) == false)
            {
                if (self.actionIdIsChk.Contains(actionId))
                {
                    return null;
                }

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

                self.actionIdIsChk.Add(actionId);
                self.actionId2ActionHandle[actionId] = actionHandler;
            }

            return actionHandler;
        }

        public static async ETTask<bool> Run(this ActionGameHandlerComponent self, string actionId, float delayTime, ActionGameContext actionGameContext)
        {
            IActionGameHandler actionHandler = self.GetActionHandle(actionId);
            if (actionHandler == null)
            {
                return false;
            }

            await actionHandler.Run(self.DomainScene(), actionId, delayTime, actionGameContext);
            return true;
        }
    }
}