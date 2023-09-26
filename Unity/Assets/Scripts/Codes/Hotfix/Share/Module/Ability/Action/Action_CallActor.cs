using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    public class Action_CallActor: IActionHandler
    {
        public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (delayTime > 0)
            {
                await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
            }

            ActionCfg_CallActor actionCfgCallActor = ActionCfg_CallActorCategory.Instance.Get(actionId);

            List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                GamePlayHelper.CreateActorByUnit(unit.DomainScene(), targetUnit, actionCfgCallActor, selectHandle, actionContext);
            }

            await ETTask.CompletedTask;
        }
    }
}