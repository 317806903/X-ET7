using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
	public class ActionGame_DoUnitAction: IActionGameHandler
	{

		public override async ETTask Run(Scene scene, string actionId, float delayTime, ActionGameContext actionGameContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (scene.IsDisposed)
				{
					return;
				}
			}

			long unitId = actionGameContext.unitId;
			Unit unit = UnitHelper.GetUnit(scene, unitId);
			while (unit == null)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				if (scene.IsDisposed)
				{
					return;
				}
				unit = UnitHelper.GetUnit(scene, unitId);
			}

			ActionGameCfg_DoUnitAction actionGameCfgDoUnitAction = ActionGameCfg_DoUnitActionCategory.Instance.Get(actionId);

			List<UnitActionCall> MonitorTriggers = actionGameCfgDoUnitAction.MonitorTriggers;
			foreach (UnitActionCall unitActionCall in MonitorTriggers)
			{
				string unitActionId = unitActionCall.ActionId;
				delayTime = unitActionCall.DelayTime;
				ActionContext actionContext = new();
				actionContext.unitId = unitId;
				List<SequenceUnitCondition> actionCondition1 = unitActionCall.ActionCondition1;
				List<SequenceUnitCondition> actionCondition2 = unitActionCall.ActionCondition2;
				SelectObjectCfg selectObjectCfg = unitActionCall.ActionCallParam_Ref;

				(SelectHandle selectHandle, Unit resetPosByUnit) = ET.Ability.SelectHandleHelper.DealSelectHandler(unit, selectObjectCfg, null, null, ref actionContext);
				if (selectHandle == null)
				{
					continue;
				}

				ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, delayTime, unitActionId, actionCondition1, actionCondition2, selectHandle, resetPosByUnit, ref actionContext);
			}

			await ETTask.CompletedTask;
		}
	}
}
