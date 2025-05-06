using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    public class Action_GlobalBuffAdd: IActionHandler
    {
        public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
                if (unit == null || unit.DomainScene() == null || unit.DomainScene().IsDisposed)
                {
                    return;
                }
            }

            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                long playerId = TeamFlagHelper.GetPlayerId(targetUnit);
                await GlobalBuffHelper.AddGlobalBuff(unit.DomainScene(), playerId, actionId, targetUnit, TeamFlagType.None);
            }
            list.Dispose();

            await ETTask.CompletedTask;
        }
    }
}