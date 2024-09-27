using System;
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
				{
					eventName = "BasePlaced",
				});
				EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingStart()
				{
					eventName = "PortalPlaced",
				});
			}
		}

		public static async ETTask<(bool, string)> SendResetHome(Scene scene)
		{
			M2C_ResetHome _M2C_ResetHome = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_ResetHome()
			{
			}) as M2C_ResetHome;
			if (_M2C_ResetHome.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ResetHome.Message,
				});
				return (false, _M2C_ResetHome.Message);
			}
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
				{
					eventName = "ResetHome",
				});
			}

			return (true, "");
		}

		public static async ETTask<(float3, List<float3>)> SendGetMonsterCall2HeadQuarterPath(Scene scene, TeamFlagType homeTeamFlagType, float3 pos)
		{
			M2C_GetMonsterCall2HeadQuarterPath _M2C_GetMonsterCall2HeadQuarterPath = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_GetMonsterCall2HeadQuarterPath()
			{
				HomeTeamFlagType = (int)homeTeamFlagType,
				Position = pos,
			}, false) as M2C_GetMonsterCall2HeadQuarterPath;
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
			else
			{
			}

			return (true, "");
		}

		public static async ETTask SendBuyPlayerTower(Scene scene, int index, string towerCfgId)
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
				{
					eventName = "TowerPurchased",
					properties = new()
					{
						{"towerCfgId", towerCfgId},
					}
				});
			}
		}

		public static async ETTask SendRefreshBuyPlayerTower(Scene scene)
		{
			M2C_RefreshBuyPlayerTower _M2C_RefreshBuyPlayerTower = await ET.Client.SessionHelper.GetSession(scene).Call(new C2M_RefreshBuyPlayerTower()) as M2C_RefreshBuyPlayerTower;
			bool bSuccess = true;
			if (_M2C_RefreshBuyPlayerTower.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_RefreshBuyPlayerTower.Message,
				});
				bSuccess = false;
			}

			EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
			{
				eventName = "RefreshClicked",
				properties = new()
				{
					{"success", bSuccess},
				}
			});
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerPutSuccess",
				});
			}
		}

		public static async ETTask SendUpgradePlayerTower(Scene scene, long towerUnitId, string towerCfgId, bool onlyChkPool)
		{
			C2M_UpgradePlayerTower _C2M_UpgradePlayerTower = new ()
			{
				TowerUnitId = towerUnitId,
				TowerCfgId = towerCfgId,
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
				{
					eventName = "TowerUpgraded",
					properties = new()
					{
						{"towerCfgId", towerCfgId},
					}
				});
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerUpgradeSuccess",
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerScaleSuccess",
				});
			}
		}

		public static async ETTask SendScalePlayerTowerCard(Scene scene, string towerCfgId)
		{
			C2M_ScalePlayerTowerCard _C2M_ScalePlayerTowerCard = new ()
			{
				TowerCfgId = towerCfgId,
			};
			M2C_ScalePlayerTowerCard _M2C_ScalePlayerTowerCard = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ScalePlayerTowerCard) as M2C_ScalePlayerTowerCard;

			if (_M2C_ScalePlayerTowerCard.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ScalePlayerTowerCard.Message,
				});
			}
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerScaleSuccess",
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerReclaimSuccess",
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
			else
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeGuideConditionStatus()
				{
					guideConditionStaticMethodType = "ChkTowerMoveSuccess",
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

		public static async ETTask SendReScan(Scene scene)
		{
			C2M_ReScan _C2M_ReScan = new ()
			{
			};
			M2C_ReScan _M2C_ReScan = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_ReScan) as M2C_ReScan;
			if (_M2C_ReScan.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_ReScan.Message,
				});
			}
		}

		public static async ETTask SendGameRecoverCancelWatchAd(Scene scene, bool isFinished)
		{
			try
			{
				scene = scene.ClientScene();
				EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
				{
					IsAdmobAvailable = false,
				});

				while (true)
				{
					if (scene.IsDisposed)
					{
						return;
					}
					if (ReLoginComponent.Instance != null && ReLoginComponent.Instance.isReCreateSessioning == false)
					{
						break;
					}
					await TimerComponent.Instance.WaitFrameAsync();
				}

				C2M_BattleRecoverCancelWatchAd _C2M_BattleRecoverCancelWatchAd = new ()
				{
					IsFinished = isFinished?1:0,
				};
				M2C_BattleRecoverCancelWatchAd _M2C_BattleRecoverCancelWatchAd = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverCancelWatchAd) as M2C_BattleRecoverCancelWatchAd;

				if (_M2C_BattleRecoverCancelWatchAd.Error != ET.ErrorCode.ERR_Success)
				{
					EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
					{
						tipMsg = _M2C_BattleRecoverCancelWatchAd.Message,
					});
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
			finally
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
				{
					IsAdmobAvailable = true,
				});
			}

		}

		public static async ETTask SendGameRecoverConfirmWatchAd(Scene scene, bool isFinished)
		{
			try
			{
				scene = scene.ClientScene();
				EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
				{
					IsAdmobAvailable = false,
				});
				while (true)
				{
					if (scene.IsDisposed)
					{
						return;
					}
					if (ReLoginComponent.Instance != null && ReLoginComponent.Instance.isReCreateSessioning == false)
					{
						break;
					}
					await TimerComponent.Instance.WaitFrameAsync();
				}
				C2M_BattleRecoverConfirmWatchAd _C2M_BattleRecoverConfirmWatchAd = new ()
				{
					IsFinished = isFinished?1:0,
				};
				M2C_BattleRecoverConfirmWatchAd _M2C_BattleRecoverConfirmWatchAd = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverConfirmWatchAd) as M2C_BattleRecoverConfirmWatchAd;

				if (_M2C_BattleRecoverConfirmWatchAd.Error != ET.ErrorCode.ERR_Success)
				{
					EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
					{
						tipMsg = _M2C_BattleRecoverConfirmWatchAd.Message,
					});
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
			finally
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
				{
					IsAdmobAvailable = true,
				});
			}

		}

		public static async ETTask SendGameRecoverResult(Scene scene, bool isConfirm)
		{
			try
			{
				scene = scene.ClientScene();
				C2M_BattleRecoverResult _C2M_BattleRecoverResult = new ()
				{
					IsConfirm = isConfirm?1:0,
				};
				M2C_BattleRecoverResult _M2C_BattleRecoverResult = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverResult) as M2C_BattleRecoverResult;

				if (_M2C_BattleRecoverResult.Error != ET.ErrorCode.ERR_Success)
				{
					EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
					{
						tipMsg = _M2C_BattleRecoverResult.Message,
					});
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
			finally
			{
			}

		}
	}
}