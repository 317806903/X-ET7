using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (AoeObj))]
    public static class AoeObjSystem
    {
        [ObjectSystem]
        public class AoeObjAwakeSystem: AwakeSystem<AoeObj>
        {
            protected override void Awake(AoeObj self)
            {
            }
        }

        [ObjectSystem]
        public class AoeObjDestroySystem: DestroySystem<AoeObj>
        {
            protected override void Destroy(AoeObj self)
            {
            }
        }
        
        [ObjectSystem]
        public class AoeObjFixedUpdateSystem: FixedUpdateSystem<AoeObj>
        {
            protected override void FixedUpdate(AoeObj self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this AoeObj self)
        {
        }

        public static void InitActionContext(this AoeObj self, ActionContext actionContext)
        {
            self.actionContext = actionContext;
        }

        public static string GetActionId(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            if (self.model.monitorTriggers.TryGetValue(abilityAoeMonitorTriggerEvent, out string actionId))
            {
                return actionId;
            }

            return "";
        }

        /// <summary>
        /// 获取aoe发射者(上级)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterUnit(this AoeObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
        }
        
        /// <summary>
        /// 获取aoe发射者(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterPlayerUnit(this AoeObj self)
        {
            Unit unit = UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
            while(true)
            {
                if (UnitHelper.ChkIsBullet(unit))
                {
                    unit = unit.GetComponent<BulletObj>().GetCasterPlayerUnit();
                }
                else if (UnitHelper.ChkIsAoe(unit))
                {
                    unit = unit.GetComponent<AoeObj>().GetCasterPlayerUnit();
                }
                else
                {
                    break;
                }
            }
            return unit;
        }

        /// <summary>
        /// 获取子弹unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this AoeObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void EventHandler(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            string actionId = self.GetActionId(abilityAoeMonitorTriggerEvent);
            if (string.IsNullOrWhiteSpace(actionId) == false)
            {
                ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, 0, null, self.actionContext);
            }
        }

        public static void FixedUpdate(this AoeObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetUnit().Destroy();
                return;
            }
            
            if (self.model.tickTime > 0)
            {
                string actionId = self.GetActionId(AbilityAoeMonitorTriggerEvent.AoeOnTick);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    //float取模不精准，所以用x1000后的整数来
                    if (Math.Round(self.timeElapsed * 1000) % Math.Round(self.model.tickTime * 1000) == 0)
                    {
                        ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, 0,null, self.actionContext);
                        self.ticked += 1;
                    }
                }
            }
        }
    }
}