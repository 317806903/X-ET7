using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffUnitObj))]
    public static class GlobalBuffUnitObjSystem
    {
        [ObjectSystem]
        public class GlobalBuffObjAwakeSystem: AwakeSystem<GlobalBuffUnitObj>
        {
            protected override void Awake(GlobalBuffUnitObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffObjDestroySystem: DestroySystem<GlobalBuffUnitObj>
        {
            protected override void Destroy(GlobalBuffUnitObj self)
            {
                if (self.selfAoeList != null)
                {
                    self.selfAoeList.Clear();
                    self.selfAoeList = null;
                }
            }
        }

        public static async ETTask Init(this GlobalBuffUnitObj self, long casterPlayerId, long unitId, string unitGlobalBuffCfgId)
        {
            self.CfgId = unitGlobalBuffCfgId;

            GlobalConditionManagerComponent globalConditionManagerComponent = self.AddComponent<GlobalConditionManagerComponent>();
            await globalConditionManagerComponent.Init(self.model.MonitorTriggers.ActionCondition1, self.model.MonitorTriggers.ActionCondition2);

            self.permanent = true;
            self.duration = 100;
            self.orgDuration = self.duration;

            self.unitId = unitId;
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = gamePlayComponent.GetPlayerIdByUnitId(self.unitId);
            self.playerId = playerId;
            self.teamFlagType = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene()).GetHomeTeamFlagTypeByPlayer(self.playerId);

            self.casterPlayerId = casterPlayerId;
            self.timeElapsed = 0;
            self.ticked = 0;
        }

        public static void ChgDuration(this GlobalBuffUnitObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
        }

        public static void SetEnabled(this GlobalBuffUnitObj self, bool isEnabled)
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

        public static void FixedUpdate(this GlobalBuffUnitObj self, float fixedDeltaTime)
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

        public static Unit GetUnit(this GlobalBuffUnitObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.unitId);
        }

        public static void TrigEvent(this GlobalBuffUnitObj self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
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
                    actionGameContextNew.unitId = self.unitId;
                }
                float delayTime = self.model.MonitorTriggers.DelayTime;
                foreach (string actionId in self.model.MonitorTriggers.ActionId)
                {
                    ActionGameHandlerHelper.CreateAction(self.DomainScene(), actionId, delayTime, ref actionGameContextNew);
                }
            }
        }

        public static bool ChkNeedRemove(this GlobalBuffUnitObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            Unit unit = self.GetUnit();
            if (UnitHelper.ChkUnitAlive(unit) == false)
            {
                return true;
            }

            return false;
        }

    }
}
