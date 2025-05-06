using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class EventHandler_GamePlayTowerDefense_ChgGamePlayNumeric: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ChgGamePlayNumeric>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ChgGamePlayNumeric args)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent != null)
			{
				var gameNumericType = args.gameNumericType;
				if (gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountBase ||
				    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountAdd ||
				    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountPct ||
				    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountFinalAdd ||
				    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountFinalPct)
				{
					gamePlayTowerDefenseComponent.ResetPutAttackTowerLimitCount();
					gamePlayTowerDefenseComponent.NoticeToClientAll();
				}
				else if (gameNumericType == GameNumericType.TowerDefense_HomeMaxHpBase ||
				         gameNumericType == GameNumericType.TowerDefense_HomeMaxHpAdd ||
				         gameNumericType == GameNumericType.TowerDefense_HomeMaxHpPct ||
				         gameNumericType == GameNumericType.TowerDefense_HomeMaxHpFinalAdd ||
				         gameNumericType == GameNumericType.TowerDefense_HomeMaxHpFinalPct)
				{
					long playerId = args.actionGameContext.playerId;
					if (playerId != (long)ET.PlayerId.PlayerNone)
					{
						TeamFlagType homeTeamFlagType = GamePlayHelper.GetHomeTeamFlagTypeByPlayer(scene, playerId);
						PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
						if (putHomeComponent != null)
						{
							Unit homeUnit = putHomeComponent.GetHomeUnit(homeTeamFlagType);
							if (homeUnit != null)
							{
								NumericComponent numericComponent = homeUnit.GetComponent<NumericComponent>();
								float curHp = numericComponent.GetAsFloat(NumericType.Hp);
								float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);

								float homeMaxHp = GamePlayHelper.GetGamePlayNumericValueByHomeTeamFlagType(scene, homeTeamFlagType, GameNumericType.TowerDefense_HomeMaxHp);
								float dis = homeMaxHp - maxHp;
								if (dis > 0)
								{
									float newCurHp = curHp + dis;
									numericComponent.SetAsFloatToBase(NumericType.MaxHp, homeMaxHp);
									numericComponent.SetAsFloatToBase(NumericType.Hp, newCurHp);

									putHomeComponent.ResetHomeHp(homeUnit.Id, dis, dis);
								}
							}
						}

					}

				}
				else if (gameNumericType == GameNumericType.TowerDefense_UpgradeItemUnitPriceBase ||
				         gameNumericType == GameNumericType.TowerDefense_UpgradeItemUnitPriceAdd ||
				         gameNumericType == GameNumericType.TowerDefense_UpgradeItemUnitPricePct ||
				         gameNumericType == GameNumericType.TowerDefense_UpgradeItemUnitPriceFinalAdd ||
				         gameNumericType == GameNumericType.TowerDefense_UpgradeItemUnitPriceFinalPct)
				{
					UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(scene);
					if (unitComponent == null)
					{
						return;
					}

					MultiMapSetSimple<UnitType, Unit> list = unitComponent.GetRecordListAll();
					if (list == null)
					{
						return;
					}

					foreach (var tmp in list)
					{
						foreach (Unit unit in tmp.Value)
						{
							ItemUpgradeComponent itemUpgradeComponent = unit.GetComponent<ItemUpgradeComponent>();
							if (itemUpgradeComponent != null)
							{
								itemUpgradeComponent.ResetUpgradeCostInfo();
								Ability.UnitHelper.AddSyncData_UnitComponent(unit, itemUpgradeComponent.GetType());
							}
						}
					}
				}
			}
			await ETTask.CompletedTask;
		}
	}
}