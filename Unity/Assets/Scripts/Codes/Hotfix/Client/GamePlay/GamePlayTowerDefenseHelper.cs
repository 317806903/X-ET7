using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Client
{
    public static class GamePlayTowerDefenseHelper
	{
		public static async ETTask SendPutHome(Scene scene, string unitCfgId, float3 pos)
		{
			M2C_PutHome _M2C_PutHomeAndMonsterCall = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_PutHome()
			{
				UnitCfgId = unitCfgId,
				Position = pos,
			}) as M2C_PutHome;
			if (_M2C_PutHomeAndMonsterCall.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_PutHomeAndMonsterCall.Message,
				});
			}
		}

		public static async ETTask<(float3, List<float3>)> SendGetMonsterCall2HeadQuarterPath(Scene scene, TeamFlagType homeTeamFlagType, float3 pos)
		{
			M2C_GetMonsterCall2HeadQuarterPath _M2C_GetMonsterCall2HeadQuarterPath = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_GetMonsterCall2HeadQuarterPath()
			{
				HomeTeamFlagType = (int)homeTeamFlagType,
				Position = pos,
			}) as M2C_GetMonsterCall2HeadQuarterPath;
			if (_M2C_GetMonsterCall2HeadQuarterPath.Error != ET.ErrorCode.ERR_Success)
			{
				//Log.Error($"SendPutMonsterCall Error==1 msg={_M2C_GetMonsterCall2HeadQuarterPath.Message}");
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_GetMonsterCall2HeadQuarterPath.Message,
				});
				return (float3.zero, null);
			}
			else
			{
				return (_M2C_GetMonsterCall2HeadQuarterPath.Position, _M2C_GetMonsterCall2HeadQuarterPath.Points);
			}
		}

		public static async ETTask<(bool, string)> SendPutMonsterCall(Scene scene, string unitCfgId, float3 pos)
		{
			M2C_PutMonsterCall _M2C_PutMonsterCall = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_PutMonsterCall()
			{
				UnitCfgId = unitCfgId,
				Position = pos,
			}) as M2C_PutMonsterCall;
			if (_M2C_PutMonsterCall.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_PutMonsterCall.Message,
				});
				return (false, _M2C_PutMonsterCall.Message);
			}

			return (true, "");
		}

		public static async ETTask SendBuyPlayerTower(Scene scene, int index)
		{
			M2C_BuyPlayerTower _M2C_BuyPlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_BuyPlayerTower()
			{
				Index = index,
			}) as M2C_BuyPlayerTower;
			if (_M2C_BuyPlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_BuyPlayerTower.Message,
				});
			}
		}

		public static async ETTask SendRefreshBuyPlayerTower(Scene scene)
		{
			M2C_RefreshBuyPlayerTower _M2C_RefreshBuyPlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_RefreshBuyPlayerTower()) as M2C_RefreshBuyPlayerTower;
			if (_M2C_RefreshBuyPlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_RefreshBuyPlayerTower.Message,
				});
			}
		}

		public static async ETTask SendCallOwnTower(Scene scene, string towerUnitCfgId, float3 position)
		{
			C2M_CallOwnTower _C2M_CallOwnTower = new ()
			{
				TowerUnitCfgId = towerUnitCfgId,
				Position = position,
			};
			M2C_CallOwnTower _M2C_CallOwnTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_CallOwnTower) as M2C_CallOwnTower;
			if (_M2C_CallOwnTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_CallOwnTower.Message,
				});
			}
		}

		public static async ETTask SendUpgradePlayerTower(Scene scene, long towerUnitId, bool onlyChkPool)
		{
			C2M_UpgradePlayerTower _C2M_UpgradePlayerTower = new ()
			{
				TowerUnitId = towerUnitId,
				OnlyChkPool = onlyChkPool?1:0,
			};
			M2C_UpgradePlayerTower _M2C_UpgradePlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_UpgradePlayerTower) as M2C_UpgradePlayerTower;
			if (_M2C_UpgradePlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_UpgradePlayerTower.Message,
				});
			}
		}

		public static async ETTask SendScalePlayerTower(Scene scene, long towerUnitId)
		{
			C2M_ScalePlayerTower _C2M_ScalePlayerTower = new ()
			{
				TowerUnitId = towerUnitId,
			};
			M2C_ScalePlayerTower _M2C_ScalePlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ScalePlayerTower) as M2C_ScalePlayerTower;

			if (_M2C_ScalePlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ScalePlayerTower.Message,
				});
			}
		}

		public static async ETTask SendReclaimPlayerTower(Scene scene, long towerUnitId)
		{
			C2M_ReclaimPlayerTower _C2M_ReclaimPlayerTower = new ()
			{
				TowerUnitId = towerUnitId,
			};
			M2C_ReclaimPlayerTower _M2C_ReclaimPlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ReclaimPlayerTower) as M2C_ReclaimPlayerTower;
			if (_M2C_ReclaimPlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ReclaimPlayerTower.Message,
				});
			}
		}

		public static async ETTask SendMovePlayerTower(Scene scene, long towerUnitId, float3 position)
		{
			C2M_MovePlayerTower _C2M_MovePlayerTower = new ()
			{
				TowerUnitId = towerUnitId,
				Position = position,
			};
			M2C_MovePlayerTower _M2C_MovePlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_MovePlayerTower) as M2C_MovePlayerTower;
			if (_M2C_MovePlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_MovePlayerTower.Message,
				});
			}
		}

		public static async ETTask SendReadyWhenRestTime(Scene scene)
		{
			C2M_ReadyWhenRestTime _C2M_ReadyWhenRestTime = new ()
			{
			};
			M2C_ReadyWhenRestTime _M2C_ReadyWhenRestTime = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ReadyWhenRestTime) as M2C_ReadyWhenRestTime;
			if (_M2C_ReadyWhenRestTime.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ReadyWhenRestTime.Message,
				});
			}
		}


		public static async ETTask SendGameRecoverCancel(Scene scene)
		{
			C2M_BattleRecoverCancel _C2M_BattleRecoverCancel = new ()
			{
			};
			M2C_BattleRecoverCancel _M2C_BattleRecoverCancel = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverCancel) as M2C_BattleRecoverCancel;

			if (_M2C_BattleRecoverCancel.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_BattleRecoverCancel.Message,
				});
			}
		}

		public static async ETTask SendGameRecoverConfirm(Scene scene)
		{
			C2M_BattleRecoverConfirm _C2M_BattleRecoverConfirm = new ()
			{
			};
			M2C_BattleRecoverConfirm _M2C_BattleRecoverConfirm = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverConfirm) as M2C_BattleRecoverConfirm;

			if (_M2C_BattleRecoverConfirm.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_BattleRecoverConfirm.Message,
				});
			}
		}
	}
}