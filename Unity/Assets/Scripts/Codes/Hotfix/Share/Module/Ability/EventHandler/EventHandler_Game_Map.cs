namespace ET.Ability
{
	public static class EventHandler_Game_Map
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				Unit unit = args.unit;

				EventSystem.Instance.Publish(scene, new ET.Ability.AbilityTriggerEventType.NearUnitOnCreate()
				{
					unit = unit,
				});

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
			{
				Unit attackerUnit = args.attackerUnit;
				attackerUnit = UnitHelper.GetCasterActorUnit(attackerUnit);
				Unit defenderUnit = args.defenderUnit;

				EventSystem.Instance.Publish(scene, new ET.Ability.AbilityTriggerEventType.NearUnitOnHit()
				{
					attackerUnit = attackerUnit,
					defenderUnit = defenderUnit,
				});


				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				Unit unit = args.unit;

				EventSystem.Instance.Publish(scene, new ET.Ability.AbilityTriggerEventType.NearUnitOnRemoved()
				{
					unit = unit,
				});

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnKill args)
			{
				Unit attackerUnit = args.attackerUnit;
				Unit beKillUnit = args.defenderUnit;
				if (attackerUnit == null || beKillUnit == null)
				{
					return;
				}
				attackerUnit = UnitHelper.GetCasterActorUnit(attackerUnit);
				GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
				if (gamePlayComponent != null)
				{
					TowerComponent towerComponent = attackerUnit.GetComponent<TowerComponent>();
					if (towerComponent != null)
					{
						long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
						if (attackerPlayerId == -1)
						{
							return;
						}

						// long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
						// if (beKillUnitPlayerId != -1)
						// {
						// 	return;
						// }

						EventSystem.Instance.Publish(scene, new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_TowerKillMonster()
						{
							playerId = attackerPlayerId,
							towerUnit = attackerUnit,
							towerCfgId = towerComponent.towerCfgId,
							monsterUnit = beKillUnit,
						});

					}
				}
				await ETTask.CompletedTask;
			}
		}

		//---------------------------------------------------------------------------------

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnCreate: AEvent<Scene, AbilityTriggerEventType.NearUnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnCreate args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.NearUnitOnCreate, args.unit, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnHit: AEvent<Scene, AbilityTriggerEventType.NearUnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnHit args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.NearUnitOnHit, args.attackerUnit, args.defenderUnit);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.NearUnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnRemoved args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.NearUnitOnRemoved, args.unit, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_PutTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_PutTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_PutTower args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_PutTower, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_ScaleTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_ScaleTower, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_ReclaimTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_ReclaimTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_ReclaimTower args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_ReclaimTower, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_UpgradeTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeTower args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_UpgradeTower, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_TowerKillMonster: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_TowerKillMonster>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_TowerKillMonster args)
			{

				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_TowerKillMonster, args.towerUnit, args.monsterUnit);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutHomeBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeBegin args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_PutHomeBegin, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutHomeEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_PutHomeEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutMonsterPointBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointBegin args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_PutMonsterPointBegin, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutMonsterPointEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_PutMonsterPointEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_ShowStartEffectBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_ShowStartEffectBegin, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_ShowStartEffectEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_ShowStartEffectEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_RestTimeBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_RestTimeBegin, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_RestTimeEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_RestTimeEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_InTheBattleBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleBegin args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_InTheBattleBegin, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_InTheBattleEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_InTheBattleEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_GameEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_GameEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_GameEnd args)
			{
				EventHandlerHelper.Run_Game(scene, AbilityGameMonitorTriggerEvent.GamePlayTowerDefense_Status_GameEnd, null, null);

				await ETTask.CompletedTask;
			}
		}

	}
}
