using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public class Action_CallActor: IActionHandler
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

            ActionCfg_CallActor actionCfgCallActor = ActionCfg_CallActorCategory.Instance.Get(actionId);

            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                float3 ? position = null;
                List<float3> positionList = null;
                if (actionCfgCallActor.CallActorPositionType == CallActorPositionType.ByParent)
                {
                    position = null;
                }
                else if (actionCfgCallActor.CallActorPositionType == CallActorPositionType.MapRandom)
                {
                    positionList = ET.GamePlayHelper.GetRandomPointList(unit.DomainScene(), actionCfgCallActor.CallActorPositionType, actionCfgCallActor.CallCount);
                }
                else if (actionCfgCallActor.CallActorPositionType == CallActorPositionType.PathLineRandom)
                {
                    positionList = ET.GamePlayHelper.GetRandomPointList(unit.DomainScene(), actionCfgCallActor.CallActorPositionType, actionCfgCallActor.CallCount);
                }
                else if (actionCfgCallActor.CallActorPositionType == CallActorPositionType.Center)
                {
                    positionList = ET.GamePlayHelper.GetRandomPointList(unit.DomainScene(), actionCfgCallActor.CallActorPositionType, actionCfgCallActor.CallCount);
                }

                Unit targetUnit = list[i];
                for (int j = 0; j < actionCfgCallActor.CallCount; j++)
                {
                    float3 ? curPosition = null;
                    if (position == null && positionList != null)
                    {
                        curPosition = positionList[j];
                    }
                    else
                    {
                        curPosition = position;
                    }
                    Unit actorUnit = GamePlayHelper.CreateActorByUnit(unit.DomainScene(), targetUnit, actionCfgCallActor, selectHandle, ref actionContext, curPosition);
                    targetUnit.AddOwnCaller(actorUnit);
                    actorUnit.AddCaster(targetUnit);
                    EventSystem.Instance.Publish(unit.DomainScene(), new ET.Ability.AbilityTriggerEventType.CallActor()
                    {
                        actionContext = actionContext,
                        unit = targetUnit,
                        beCallUnit = actorUnit,
                    });
                }
            }
            list.Dispose();

            await ETTask.CompletedTask;
        }
    }
}