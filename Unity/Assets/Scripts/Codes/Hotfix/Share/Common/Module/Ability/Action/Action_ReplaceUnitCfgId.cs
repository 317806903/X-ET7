using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_ReplaceUnitCfgId: IActionHandler
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
			ActionCfg_ReplaceUnitCfgId actionCfg_ReplaceUnitCfgId = ActionCfg_ReplaceUnitCfgIdCategory.Instance.Get(actionId);

			for (int i = 0; i < list.Count; i++)
			{
				Unit targetUnit = list[i];

				GameObjectComponent gameObjectComponent = targetUnit.GetComponent<GameObjectComponent>();
				string replaceUnitCfgId = actionCfg_ReplaceUnitCfgId.ReplaceUnitCfgId;
				if (string.IsNullOrEmpty(replaceUnitCfgId))
				{
					if (gameObjectComponent != null)
					{
						replaceUnitCfgId = gameObjectComponent.orgUnitCfgId;
					}
					else
					{
						continue;
					}
				}

				if (targetUnit.CfgId != replaceUnitCfgId)
				{
					targetUnit.CfgId = replaceUnitCfgId;
					UnitHelper.AddSyncData_UnitBaseInfo(targetUnit);


					NumericComponent numericComponent = targetUnit.GetComponent<NumericComponent>();
					numericComponent.SetAsFloat(NumericType.SpeedBase, unit.model.MoveSpeed);
					numericComponent.SetAsFloat(NumericType.RotationSpeedBase, targetUnit.model.RotationSpeed);

				}

				if (gameObjectComponent != null)
				{
					if (gameObjectComponent.curUnitCfgId != replaceUnitCfgId)
					{
						gameObjectComponent.Reset(replaceUnitCfgId);
						UnitHelper.AddSyncData_UnitComponent(targetUnit, typeof(GameObjectComponent));
					}
				}

			}
			list.Dispose();

			await ETTask.CompletedTask;
		}
	}
}
