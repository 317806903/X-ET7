using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_GameObjectDeal: IActionHandler
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
			ActionCfg_GameObjectDeal actionCfg_GameObjectDeal = ActionCfg_GameObjectDealCategory.Instance.Get(actionId);

			for (int i = 0; i < list.Count; i++)
			{
				Unit targetUnit = list[i];
				GameObjectComponent gameObjectComponent = targetUnit.GetComponent<GameObjectComponent>();
				if (gameObjectComponent == null)
				{
					continue;
				}

				bool needNotice = false;
				foreach (GameObjectDeal gameObjectDeal in actionCfg_GameObjectDeal.DealType)
				{
					if (gameObjectDeal is GameObjectHide)
					{
						if (gameObjectComponent.isHiding)
						{
							continue;
						}
						gameObjectComponent.isHiding = true;
					}
					else if (gameObjectDeal is GameObjectNotHide)
					{
						if (gameObjectComponent.isHiding == false)
						{
							continue;
						}
						gameObjectComponent.isHiding = false;
					}
					else if (gameObjectDeal is GameObjectFlicker gameObjectFlicker)
					{
						if (gameObjectFlicker.FlickerDuration <= 0)
						{
							continue;
						}
						gameObjectComponent.flickerEndTime = TimeHelper.ServerNow() + (long)(gameObjectFlicker.FlickerDuration * 1000);
						gameObjectComponent.flickerFrequency = gameObjectFlicker.FlickerFrequency;
						gameObjectComponent.flickerStartColor = gameObjectFlicker.StartColor;
						gameObjectComponent.flickerEndColor = gameObjectFlicker.EndColor;
					}
					else if (gameObjectDeal is GameObjectTransparent)
					{
						if (gameObjectComponent.isTransparent)
						{
							continue;
						}
						gameObjectComponent.isTransparent = true;
					}
					else if (gameObjectDeal is GameObjectNotTransparent)
					{
						if (gameObjectComponent.isTransparent == false)
						{
							continue;
						}
						gameObjectComponent.isTransparent = false;
					}
					else if (gameObjectDeal is GameObjectFly)
					{
						if (gameObjectComponent.isFly)
						{
							continue;
						}
						gameObjectComponent.isFly = true;
					}
					else if (gameObjectDeal is GameObjectNotFly)
					{
						if (gameObjectComponent.isFly == false)
						{
							continue;
						}
						gameObjectComponent.isFly = false;
					}
					needNotice = true;
				}

				if (needNotice)
				{
					UnitHelper.AddSyncData_UnitComponent(targetUnit, typeof(GameObjectComponent));
				}
			}
			list.Dispose();

			await ETTask.CompletedTask;
		}
	}
}
