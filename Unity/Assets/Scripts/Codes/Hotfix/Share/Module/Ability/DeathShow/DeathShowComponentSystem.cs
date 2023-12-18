using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (DeathShowComponent))]
    public static class DeathShowComponentSystem
    {
        [ObjectSystem]
        public class DeathShowComponentAwakeSystem: AwakeSystem<DeathShowComponent>
        {
            protected override void Awake(DeathShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DeathShowComponentDestroySystem: DestroySystem<DeathShowComponent>
        {
            protected override void Destroy(DeathShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DeathShowComponentFixedUpdateSystem: FixedUpdateSystem<DeathShowComponent>
        {
            protected override void FixedUpdate(DeathShowComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this DeathShowComponent self, ActionCfg_DeathShow actionCfg_DeathShow)
        {
            self.duration = actionCfg_DeathShow.Duration;
            self.timeElapsed = 0;

            string actionId = actionCfg_DeathShow.Id;

            ActionContext actionContext = new();
            SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(self.GetUnit());
            ET.Ability.ActionHandlerHelper.CreateAction(self.GetUnit(), null, actionId, 0, selectHandleSelf, ref actionContext);
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this DeathShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this DeathShowComponent self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            //Log.Error(" self.duration:" + self.duration + " " + self.GetUnit().Id);
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetUnit()._Destroy();
            }
        }
    }
}