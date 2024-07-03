using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffPlayerObj))]
    public static class GlobalBuffPlayerObjSystem
    {
        [ObjectSystem]
        public class GlobalBuffObjAwakeSystem: AwakeSystem<GlobalBuffPlayerObj>
        {
            protected override void Awake(GlobalBuffPlayerObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffObjDestroySystem: DestroySystem<GlobalBuffPlayerObj>
        {
            protected override void Destroy(GlobalBuffPlayerObj self)
            {
                if (self.selfAoeList != null)
                {
                    self.selfAoeList.Clear();
                    self.selfAoeList = null;
                }
            }
        }

        public static async ETTask Init(this GlobalBuffPlayerObj self, long casterPlayerId, long playerId, string playerGlobalBuffCfgId)
        {
            self.CfgId = playerGlobalBuffCfgId;

            GlobalConditionManagerComponent globalConditionManagerComponent = self.AddComponent<GlobalConditionManagerComponent>();
            await globalConditionManagerComponent.Init(self.model.MonitorTriggers.ActionCondition1, self.model.MonitorTriggers.ActionCondition2);

            self.permanent = true;
            self.duration = 100;
            self.orgDuration = self.duration;

            self.playerId = playerId;
            self.teamFlagType = GamePlayHelper.GetHomeTeamFlagTypeByPlayer(self.DomainScene(), self.playerId);
            self.casterPlayerId = casterPlayerId;
            self.timeElapsed = 0;
            self.ticked = 0;
        }

        public static void ChgDuration(this GlobalBuffPlayerObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
        }

        public static void SetEnabled(this GlobalBuffPlayerObj self, bool isEnabled)
        {
            bool oldIsEnabled = self.isEnabled;
            if (oldIsEnabled == isEnabled)
            {
                return;
            }
            if (isEnabled == false)
            {
                self.isEnabled = false;
            }
            else
            {
                self.isEnabled = true;
            }
        }

        public static void FixedUpdate(this GlobalBuffPlayerObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            float lastTimeElapsed = self.timeElapsed;
            self.timeElapsed += timePassed;

            int tickCount = math.min(self.model.TickTime.Count, 3);
            for (int i = 0; i < tickCount; i++)
            {
                if (self.model.TickTime[i] > 0)
                {
                    int lastCount = (int)(lastTimeElapsed / self.model.TickTime[i]);
                    int newCount = (int)(self.timeElapsed / self.model.TickTime[i]);
                    while (newCount > lastCount)
                    {
                        lastCount++;
                        if (i == 0)
                        {
                            ActionGameContext actionGameContext = new();
                            self.TrigEvent(ET.AbilityConfig.GlobalBuffTriggerEvent.GlobalBuffOnTick1, ref actionGameContext);
                        }
                        else if (i == 1)
                        {
                            ActionGameContext actionGameContext = new();
                            self.TrigEvent(ET.AbilityConfig.GlobalBuffTriggerEvent.GlobalBuffOnTick2, ref actionGameContext);
                        }
                        else if (i == 2)
                        {
                            ActionGameContext actionGameContext = new();
                            self.TrigEvent(ET.AbilityConfig.GlobalBuffTriggerEvent.GlobalBuffOnTick3, ref actionGameContext);
                        }

                        self.ticked += 1;
                    }
                }
            }
        }

        public static void TrigEvent(this GlobalBuffPlayerObj self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = self.GetComponent<GlobalConditionManagerComponent>();
            globalConditionManagerComponent.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
            bool bPass = globalConditionManagerComponent.ChkConditionPass();
            if (bPass)
            {
                ActionGameContext actionGameContextNew = actionGameContext;
                if (actionGameContextNew.playerId == 0)
                {
                    actionGameContextNew.playerId = self.playerId;
                }
                if (actionGameContextNew.teamFlagType == TeamFlagType.None)
                {
                    actionGameContextNew.teamFlagType = self.teamFlagType;
                }
                if (actionGameContextNew.unitId == 0)
                {
                    actionGameContextNew.unitId = self.playerId;
                }
                float delayTime = self.model.MonitorTriggers.DelayTime;
                foreach (string actionId in self.model.MonitorTriggers.ActionId)
                {
                    ActionGameHandlerHelper.CreateAction(self.DomainScene(), actionId, delayTime, ref actionGameContextNew);
                }
            }
        }

        public static bool ChkNeedRemove(this GlobalBuffPlayerObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            return false;
        }

    }
}
