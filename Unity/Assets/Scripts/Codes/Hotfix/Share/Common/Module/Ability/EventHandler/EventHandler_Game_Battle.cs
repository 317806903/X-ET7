namespace ET.Ability
{
	public static class EventHandler_Game_Battle
	{
		#region Base
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
				attackerUnit = attackerUnit.GetCasterRootActor();
				if (attackerUnit == null)
				{
					return;
				}
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
				attackerUnit = attackerUnit.GetCasterRootActor();
				if (attackerUnit == null)
				{
					return;
				}
				GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
				if (gamePlayComponent != null)
				{
					TowerComponent towerComponent = attackerUnit.GetComponent<TowerComponent>();
					if (towerComponent != null)
					{
						long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
						if (attackerPlayerId == (long)ET.PlayerId.PlayerNone)
						{
							return;
						}

						// long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
						// if (beKillUnitPlayerId != (long)ET.PlayerId.PlayerNone)
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
		#endregion

		//---------------------------------------------------------------------------------

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnCreate: AEvent<Scene, AbilityTriggerEventType.NearUnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnCreate args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.unitId = args.unit.Id;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.NearUnitOnCreate, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnHit: AEvent<Scene, AbilityTriggerEventType.NearUnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnHit args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.attackerUnitId = args.attackerUnit.Id;
				actionGameContext.defenderUnitId = args.defenderUnit.Id;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.NearUnitOnHit, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_NearUnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.NearUnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.NearUnitOnRemoved args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.unitId = args.unit.Id;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.NearUnitOnRemoved, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_PutTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_PutTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_PutTower args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.towerUnit.Id;
				actionGameContext.towerCfgId = args.towerCfgId;

				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_PutTower, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_ScaleTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.towerUnit.Id;
				actionGameContext.towerCfgId = args.towerCfgId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_ScaleTower, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_ReclaimTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_ReclaimTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_ReclaimTower args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.towerUnit.Id;
				actionGameContext.towerCfgId = args.towerCfgId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_ReclaimTower, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_UpgradeTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeTower args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.newTowerUnit.Id;
				actionGameContext.towerCfgId = args.newTowerCfgId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_UpgradeTower, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_UpgradeItemUnit: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeItemUnit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_UpgradeItemUnit args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.itemUnit.Id;
				actionGameContext.towerCfgId = args.itemCfgId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_UpgradeItemUnit, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_MoveTower: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_MoveTower>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_MoveTower args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.towerUnit.Id;
				actionGameContext.towerCfgId = args.towerCfgId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_MoveTower, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_TowerKillMonster: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_TowerKillMonster>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_TowerKillMonster args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;
				actionGameContext.unitId = args.towerUnit.Id;
				actionGameContext.towerCfgId = args.towerCfgId;
				actionGameContext.attackerUnitId = args.towerUnit.Id;
				actionGameContext.defenderUnitId = args.monsterUnit.Id;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_TowerKillMonster, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_RefreshTowerBuyPool: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_RefreshTowerBuyPool>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_RefreshTowerBuyPool args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.playerId;

				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_RefreshTowerBuyPool, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_GameWaitForStart: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_GameWaitForStart>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_GameWaitForStart args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_GameWaitForStart, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_Start: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_GameStart>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_GameStart args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_GameStart, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_LoadMeshFinished: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_LoadMeshFinished, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_AddPlayerWhenGlobal: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_AddPlayerWhenGlobal>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_AddPlayerWhenGlobal args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.actionContext.playerId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_AddPlayerWhenGlobal, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_PlayerQuit: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_PlayerQuit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_PlayerQuit args)
			{
				ActionGameContext actionGameContext = new();
				actionGameContext.playerId = args.actionContext.playerId;
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_PlayerQuit, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutHomeBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeBegin args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_PutHomeBegin, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutHomeEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_PutHomeEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutMonsterPointBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointBegin args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_PutMonsterPointBegin, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_PutMonsterPointEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_PutMonsterPointEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_ShowStartEffectBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_ShowStartEffectBegin, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_ShowStartEffectEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_ShowStartEffectEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_SkillReady: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_SkillReady, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_RestTimeBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_RestTimeBegin, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_RestTimeEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_RestTimeEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_InTheBattleBegin: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleBegin>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleBegin args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_InTheBattleBegin, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlayTowerDefense_Status_InTheBattleEnd: AEvent<Scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlayTowerDefense_Status_InTheBattleEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_GamePlay_Status_GameEnd: AEvent<Scene, AbilityTriggerEventType.GamePlay_Status_GameEnd>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.GamePlay_Status_GameEnd args)
			{
				ActionGameContext actionGameContext = new();
				EventHandlerHelper.Run_Game(scene, ET.AbilityConfig.GlobalBuffTriggerEvent.GamePlay_Status_GameEnd, ref actionGameContext);

				await ETTask.CompletedTask;
			}
		}

	}
}
