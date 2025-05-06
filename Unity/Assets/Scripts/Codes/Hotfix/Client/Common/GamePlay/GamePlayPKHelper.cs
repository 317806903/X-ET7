using Unity.Mathematics;

namespace ET.Client
{
    public static class GamePlayPKHelper
	{
		public static async ETTask SendCallTower(Scene scene, string towerUnitCfgId, float3 position, float3 forward, string createActionIds)
		{
			position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, position);
			forward = ET.Ability.UnitHelper.TranClientForward2ServerForward(forward);
			C2M_CallTower _C2M_CallTower = new ()
			{
				TowerUnitCfgId = towerUnitCfgId,
				Position = position,
				Forward = forward,
				CreateActionIds = createActionIds.Trim(),
			};
			M2C_CallTower _M2C_CallTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_CallTower) as M2C_CallTower;

			if (_M2C_CallTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_CallTower.Message,
				});
			}
		}

		public static async ETTask SendCallTowerActions(Scene scene, long towerUnitId, string addActionIds)
		{
			C2M_CallTowerActions _C2M_CallTowerActions = new ()
			{
				TowerUnitId = towerUnitId,
				AddActionIds = addActionIds.Trim(),
			};
			M2C_CallTowerActions _M2C_CallTowerActions = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_CallTowerActions) as M2C_CallTowerActions;

			if (_M2C_CallTowerActions.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_CallTowerActions.Message,
				});
			}
		}

		public static async ETTask SendCallMonster(Scene scene, string monsterUnitCfgId, float3 position, float3 forward, int count, string createActionIds)
		{
			position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, position);
			forward = ET.Ability.UnitHelper.TranClientForward2ServerForward(forward);
			C2M_CallMonster _C2M_CallMonster = new ()
			{
				MonsterUnitCfgId = monsterUnitCfgId,
				Position = position,
				Forward = forward,
				Count = count,
				CreateActionIds = createActionIds.Trim(),
			};
			M2C_CallMonster _M2C_CallMonster = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_CallMonster) as M2C_CallMonster;

			if (_M2C_CallMonster.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_CallMonster.Message,
				});
			}
		}

		public static async ETTask SendClearMyTower(Scene scene, long towerUnitId)
		{
			C2M_ClearMyTower _C2M_ClearMyTower = new ()
			{
				TowerUnitId = towerUnitId,
			};
			M2C_ClearMyTower _M2C_ClearMyTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ClearMyTower) as M2C_ClearMyTower;

			if (_M2C_ClearMyTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_ClearMyTower.Message,
				});
			}
		}

		public static async ETTask SendClearAllMonster(Scene scene)
		{
			C2M_ClearAllMonster _C2M_ClearAllMonster = new ()
			{
			};
			M2C_ClearAllMonster _M2C_ClearAllMonster = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ClearAllMonster) as M2C_ClearAllMonster;

			if (_M2C_ClearAllMonster.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_ClearAllMonster.Message,
				});
			}
		}

		public static async ETTask SendMovePKTower(Scene scene, long towerUnitId, float3 position, float3 forward)
		{
			position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, position);
			forward = ET.Ability.UnitHelper.TranClientForward2ServerForward(forward);
			C2M_PKMoveTower _C2M_PKMoveTower = new ()
			{
				TowerUnitId = towerUnitId,
				Position = position,
				Forward = forward,
			};
			M2C_PKMoveTower _M2C_PKMoveTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_PKMoveTower) as M2C_PKMoveTower;
			if (_M2C_PKMoveTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_PKMoveTower.Message,
				});
			}
		}

		public static async ETTask SendMovePKPlayer(Scene scene, long towerUnitId, float3 position, float3 forward)
		{
			position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, position);
			forward = ET.Ability.UnitHelper.TranClientForward2ServerForward(forward);
			C2M_PKMovePlayer _C2M_PKMovePlayer = new ()
			{
				TowerUnitId = towerUnitId,
				Position = position,
				Forward = forward,
			};
			M2C_PKMovePlayer _M2C_PKMovePlayer = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_PKMovePlayer) as M2C_PKMovePlayer;
			if (_M2C_PKMovePlayer.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
				{
					tipMsg = _M2C_PKMovePlayer.Message,
				});
			}
		}

	}
}