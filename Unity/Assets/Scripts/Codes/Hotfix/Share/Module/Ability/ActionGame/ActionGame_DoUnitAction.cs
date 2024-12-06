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

			ActionContext actionContext = new();
			actionContext.unitId = unitId;
			List<UnitActionCall> MonitorTriggers = actionGameCfgDoUnitAction.MonitorTriggers;
			foreach (UnitActionCall unitActionCall in MonitorTriggers)
			{
				bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(unit, unitActionCall.ChkCondition1, unitActionCall.ChkCondition2, unitActionCall.ChkCondition1SelectObj_Ref, unitActionCall.ChkCondition2SelectObj_Ref, ref actionContext);
				if (bRetChk == false)
				{
					continue;
				}

				List<SequenceUnitCondition> filterCondition1 = unitActionCall.FilterCondition1;
				List<SequenceUnitCondition> filterCondition2 = unitActionCall.FilterCondition2;
				SelectObjectCfg selectObjectCfg = unitActionCall.ActionCallParam_Ref;

				(SelectHandle selectHandle, Unit resetPosByUnit) = ET.Ability.SelectHandleHelper.DealSelectHandler(unit, selectObjectCfg, null, null, ref actionContext);
				if (selectHandle == null)
				{
					continue;
				}

				ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, unitActionCall.DelayTime, unitActionCall.ActionId, filterCondition1, filterCondition2, selectHandle, resetPosByUnit, ref actionContext);
			}

			await ETTask.CompletedTask;
		}
	}
}
