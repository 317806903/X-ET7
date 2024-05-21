using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    public class Action_CallAoe: IActionHandler
    {
        public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
            }

            ActionCfg_CallAoe actionCfgCallAoe = ActionCfg_CallAoeCategory.Instance.Get(actionId);

            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                GamePlayHelper.CreateAoeByUnit(unit.DomainScene(), targetUnit, actionCfgCallAoe, selectHandle, ref actionContext);
            }
            list.Dispose();

            await ETTask.CompletedTask;
        }
    }
}