using ET.Ability;
using ET.EventType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            M2C_ResetHome _M2C_ResetHome = await SessionHelper.GetSession(scene).Call(new C2M_ResetHome()
            {
            }) as M2C_ResetHome;
            if (_M2C_ResetHome.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_ResetHome.Message,
                });
                return (false, _M2C_ResetHome.Message);
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeEventLogging()
                {
                    eventName = "ResetHome",
                });
            }

            return (true, "");
        }

        public static async ETTask<(float3, List<float3>)> SendGetMonsterCall2HeadQuarterPath(Scene scene, TeamFlagType homeTeamFlagType, float3 pos)
        {
            M2C_GetMonsterCall2HeadQuarterPath _M2C_GetMonsterCall2HeadQuarterPath = await SessionHelper.GetSession(scene).Call(
                new C2M_GetMonsterCall2HeadQuarterPath()
                {
                    HomeTeamFlagType = (int)homeTeamFlagType,
                    Position = pos,
                }, false) as M2C_GetMonsterCall2HeadQuarterPath;
            if (_M2C_GetMonsterCall2HeadQuarterPath.Error != ErrorCode.ERR_Success)
            {
                //Log.Error($"SendPutMonsterCall Error==1 msg={_M2C_GetMonsterCall2HeadQuarterPath.Message}");
                EventSystem.Instance.Publish(scene, new NoticeUITip()
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

        public static async ETTask<M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath> SendTryMoveUnitAndGetAllMonsterCall2HeadQuarterPath(
            Scene scene, long unitId, string unitCfgId, float3 pos)
        {
            M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath _M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath = await SessionHelper
                   .GetSession(scene).Call(new C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath()
                    {
                        UnitId = unitId,
                        UnitCfgId = unitCfgId,
                        Position = pos,
                    }, false) as M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath;
            if (_M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath.Message,
                });
                return null;
            }
            return _M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath;
        }

        public static async ETTask<NavmeshManagerComponent.NavMeshData> RequestReachableAreaFromPosition(Scene scene, float3 position)
        {
            M2C_GetNavMeshFromPosition _M2C_GetNavMeshFromHeadQuarter = await SessionHelper.GetSession(scene).Call(new C2M_GetNavMeshFromPosition()
            {
                Position = position,
            }, false) as M2C_GetNavMeshFromPosition;
            NavmeshManagerComponent.NavMeshData meshData = new();
            if (_M2C_GetNavMeshFromHeadQuarter.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_GetNavMeshFromHeadQuarter.Message,
                });
                return meshData;
            }
            meshData.Indices = _M2C_GetNavMeshFromHeadQuarter.Indices;
            meshData.Vertices = _M2C_GetNavMeshFromHeadQuarter.Vertices;
            meshData.PolygonRefs = _M2C_GetNavMeshFromHeadQuarter.PolygonRefs;
            return meshData;
        }

        public static async ETTask<(bool, string)> SendPutMonsterCall(Scene scene, string unitCfgId, float3 pos)
        {
            M2C_PutMonsterCall _M2C_PutMonsterCall = await SessionHelper.GetSession(scene).Call(new C2M_PutMonsterCall()
            {
                UnitCfgId = unitCfgId,
                Position = pos,
            }) as M2C_PutMonsterCall;
            if (_M2C_PutMonsterCall.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
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
            M2C_BuyPlayerTower _M2C_BuyPlayerTower = await SessionHelper.GetSession(scene).Call(new C2M_BuyPlayerTower()
            {
                Index = index,
            }) as M2C_BuyPlayerTower;
            if (_M2C_BuyPlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_BuyPlayerTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeEventLogging()
                {
                    eventName = "TowerPurchased",
                    properties = new()
                    {
                        { "towerCfgId", towerCfgId },
                    }
                });
            }
        }

        public static async ETTask SendRefreshBuyPlayerTower(Scene scene)
        {
            M2C_RefreshBuyPlayerTower _M2C_RefreshBuyPlayerTower =
                    await SessionHelper.GetSession(scene).Call(new C2M_RefreshBuyPlayerTower()) as M2C_RefreshBuyPlayerTower;
            bool bSuccess = true;
            if (_M2C_RefreshBuyPlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_RefreshBuyPlayerTower.Message,
                });
                bSuccess = false;
            }

            EventSystem.Instance.Publish(scene, new NoticeEventLogging()
            {
                eventName = "RefreshClicked",
                properties = new()
                {
                    { "success", bSuccess },
                }
            });
        }

        public static async ETTask SendCallOwnTower(Scene scene, string towerUnitCfgId, float3 position)
        {
            C2M_CallOwnTower _C2M_CallOwnTower = new()
            {
                TowerUnitCfgId = towerUnitCfgId,
                Position = position,
            };
            M2C_CallOwnTower _M2C_CallOwnTower = await SessionHelper.GetSession(scene).Call(_C2M_CallOwnTower) as M2C_CallOwnTower;
            if (_M2C_CallOwnTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_CallOwnTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerPutSuccess",
                });
            }
        }

        public static async ETTask SendUpgradePlayerTower(Scene scene, long towerUnitId, string towerCfgId, bool onlyChkPool)
        {
            C2M_UpgradePlayerTower _C2M_UpgradePlayerTower = new()
            {
                TowerUnitId = towerUnitId,
                TowerCfgId = towerCfgId,
                OnlyChkPool = onlyChkPool? 1 : 0,
            };
            M2C_UpgradePlayerTower _M2C_UpgradePlayerTower =
                    await SessionHelper.GetSession(scene).Call(_C2M_UpgradePlayerTower) as M2C_UpgradePlayerTower;
            if (_M2C_UpgradePlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_UpgradePlayerTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeEventLogging()
                {
                    eventName = "TowerUpgraded",
                    properties = new()
                    {
                        { "towerCfgId", towerCfgId },
                    }
                });
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerUpgradeSuccess",
                });
            }
        }

        public static async ETTask SendScalePlayerTower(Scene scene, long towerUnitId)
        {
            C2M_ScalePlayerTower _C2M_ScalePlayerTower = new()
            {
                TowerUnitId = towerUnitId,
            };
            M2C_ScalePlayerTower _M2C_ScalePlayerTower =
                    await SessionHelper.GetSession(scene).Call(_C2M_ScalePlayerTower) as M2C_ScalePlayerTower;

            if (_M2C_ScalePlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_ScalePlayerTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerScaleSuccess",
                });
            }
        }

        public static async ETTask SendScalePlayerTowerCard(Scene scene, string towerCfgId)
        {
            C2M_ScalePlayerTowerCard _C2M_ScalePlayerTowerCard = new()
            {
                TowerCfgId = towerCfgId,
            };
            M2C_ScalePlayerTowerCard _M2C_ScalePlayerTowerCard =
                    await SessionHelper.GetSession(scene).Call(_C2M_ScalePlayerTowerCard) as M2C_ScalePlayerTowerCard;

            if (_M2C_ScalePlayerTowerCard.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_ScalePlayerTowerCard.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerScaleSuccess",
                });
            }
        }

        public static async ETTask SendReclaimPlayerTower(Scene scene, long towerUnitId)
        {
            C2M_ReclaimPlayerTower _C2M_ReclaimPlayerTower = new()
            {
                TowerUnitId = towerUnitId,
            };
            M2C_ReclaimPlayerTower _M2C_ReclaimPlayerTower =
                    await SessionHelper.GetSession(scene).Call(_C2M_ReclaimPlayerTower) as M2C_ReclaimPlayerTower;
            if (_M2C_ReclaimPlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_ReclaimPlayerTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerReclaimSuccess",
                });
            }
        }

        public static async ETTask SendMovePlayerTower(Scene scene, long towerUnitId, float3 position)
        {
            C2M_MovePlayerTower _C2M_MovePlayerTower = new()
            {
                TowerUnitId = towerUnitId,
                Position = position,
            };
            M2C_MovePlayerTower _M2C_MovePlayerTower =
                    await SessionHelper.GetSession(scene).Call(_C2M_MovePlayerTower) as M2C_MovePlayerTower;
            if (_M2C_MovePlayerTower.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_MovePlayerTower.Message,
                });
            }
            else
            {
                EventSystem.Instance.Publish(scene, new NoticeGuideConditionStatus()
                {
                    guideConditionStaticMethodType = "ChkTowerMoveSuccess",
                });
            }
        }

        public static async ETTask SendReadyWhenRestTime(Scene scene)
        {
            C2M_ReadyWhenRestTime _C2M_ReadyWhenRestTime = new()
            {
            };
            M2C_ReadyWhenRestTime _M2C_ReadyWhenRestTime =
                    await SessionHelper.GetSession(scene).Call(_C2M_ReadyWhenRestTime) as M2C_ReadyWhenRestTime;
            if (_M2C_ReadyWhenRestTime.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
                {
                    tipMsg = _M2C_ReadyWhenRestTime.Message,
                });
            }
        }

        public static async ETTask SendReScan(Scene scene)
        {
            C2M_ReScan _C2M_ReScan = new()
            {
            };
            M2C_ReScan _M2C_ReScan = await SessionHelper.GetSession(scene).Call(_C2M_ReScan) as M2C_ReScan;
            if (_M2C_ReScan.Error != ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new NoticeUITip()
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
                EventSystem.Instance.Publish(scene, new NoticeAdmobSDKStatus()
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

                C2M_BattleRecoverCancelWatchAd _C2M_BattleRecoverCancelWatchAd = new()
                {
                    IsFinished = isFinished? 1 : 0,
                };
                M2C_BattleRecoverCancelWatchAd _M2C_BattleRecoverCancelWatchAd =
                        await SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverCancelWatchAd) as M2C_BattleRecoverCancelWatchAd;

                if (_M2C_BattleRecoverCancelWatchAd.Error != ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(scene, new NoticeUITip()
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
                EventSystem.Instance.Publish(scene, new NoticeAdmobSDKStatus()
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
                EventSystem.Instance.Publish(scene, new NoticeAdmobSDKStatus()
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
                C2M_BattleRecoverConfirmWatchAd _C2M_BattleRecoverConfirmWatchAd = new()
                {
                    IsFinished = isFinished? 1 : 0,
                };
                M2C_BattleRecoverConfirmWatchAd _M2C_BattleRecoverConfirmWatchAd =
                        await SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverConfirmWatchAd) as M2C_BattleRecoverConfirmWatchAd;

                if (_M2C_BattleRecoverConfirmWatchAd.Error != ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(scene, new NoticeUITip()
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
                EventSystem.Instance.Publish(scene, new NoticeAdmobSDKStatus()
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
                C2M_BattleRecoverResult _C2M_BattleRecoverResult = new()
                {
                    IsConfirm = isConfirm? 1 : 0,
                };
                M2C_BattleRecoverResult _M2C_BattleRecoverResult =
                        await SessionHelper.GetSession(scene).Call(_C2M_BattleRecoverResult) as M2C_BattleRecoverResult;

                if (_M2C_BattleRecoverResult.Error != ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(scene, new NoticeUITip()
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
        public static async Task<(long, float3)> GetNearestNavmeshPos(Scene scene, float3 pos)
        {
            try
            {
                scene = scene.ClientScene();
                M2C_GetNearestNavMeshPoint _M2C_GetNearestNavMeshPoint =
                        await SessionHelper.GetSession(scene).Call(new C2M_GetNearestNavMeshPoint()
                        {
                            Position = pos,
                        }, false) as M2C_GetNearestNavMeshPoint;
                return (_M2C_GetNearestNavMeshPoint.PolygonRef, _M2C_GetNearestNavMeshPoint.NavMeshPosition);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return (0, float3.zero);
        }
    }
}
