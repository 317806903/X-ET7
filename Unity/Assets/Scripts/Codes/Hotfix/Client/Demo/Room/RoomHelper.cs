using System;
using MongoDB.Bson;

namespace ET.Client
{
    public static class RoomHelper
    {
        public static RoomManagerComponent GetRoomManager(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
            RoomManagerComponent roomManagerComponent = currentScenesComponent.GetComponent<RoomManagerComponent>();
            if (roomManagerComponent == null)
            {
                roomManagerComponent = currentScenesComponent.AddComponent<RoomManagerComponent>();
            }
            return roomManagerComponent;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="clientScene"></param>
        public static async ETTask GetRoomListAsync(Scene clientScene)
        {
            try
            {
                G2C_GetRoomList _G2C_GetRoomList = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetRoomList(), false) as G2C_GetRoomList;
                if (_G2C_GetRoomList.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.GetRoomListAsync Error==1 msg={_G2C_GetRoomList.Message}");
                }
                else
                {
                    RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(clientScene);
                    roomManagerComponent.Init(_G2C_GetRoomList.RoomInfos);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="roomId"></param>
        public static async ETTask<bool> GetRoomInfoAsync(Scene clientScene, long roomId)
        {
            if (roomId == 0)
            {
                return false;
            }
            G2C_GetRoomInfo _G2C_GetRoomInfo = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetRoomInfo()
                {
                    RoomId = roomId,
                }) as
                G2C_GetRoomInfo;
            if (_G2C_GetRoomInfo.Error != ET.ErrorCode.ERR_Success)
            {
                Log.Error($"ET.Client.RoomHelper.GetRoomInfoAsync Error==1 msg={_G2C_GetRoomInfo.Message}");
                return false;
            }
            else
            {
                RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(clientScene);
                roomManagerComponent.Init(roomId, _G2C_GetRoomInfo.RoomInfo, _G2C_GetRoomInfo.RoomMemberInfos);
                return true;
            }
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="battleCfgId"></param>
        /// <param name="isARRoom"></param>
        /// <returns></returns>
        public static async ETTask<(bool bRet, long roomId)> CreateRoomAsync(Scene clientScene, RoomTypeInfo roomTypeInfo)
        {
            try
            {
                G2C_CreateRoom _G2C_CreateRoom = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_CreateRoom()
                {
                    RoomTypeInfo = ET.RoomTypeInfo.ToBytes(roomTypeInfo),
                }) as G2C_CreateRoom;
                if (_G2C_CreateRoom.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.CreateRoomAsync Error==1 msg={_G2C_CreateRoom.Message}");
                    return (false, 0);
                }

                return (true, _G2C_CreateRoom.RoomId);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, 0);
            }
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public static async ETTask<bool> JoinRoomAsync(Scene clientScene, long roomId)
        {
            if (roomId == 0)
            {
                return false;
            }
            try
            {
                G2C_JoinRoom _G2C_JoinRoom = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_JoinRoom()
                {
                    RoomId = roomId,
                }) as G2C_JoinRoom;
                if (_G2C_JoinRoom.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.JoinRoomAsync Error==1 msg={_G2C_JoinRoom.Message}");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 玩家退出房间
        /// </summary>
        /// <param name="clientScene"></param>
        public static async ETTask QuitRoomAsync(Scene clientScene)
        {
            try
            {
                G2C_QuitRoom _G2C_QuitRoom = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_QuitRoom()) as G2C_QuitRoom;
                if (_G2C_QuitRoom.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.QuitRoomAsync Error==1 msg={_G2C_QuitRoom.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 修改准备状态
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="isReady"></param>
        public static async ETTask ChgRoomMemberStatusAsync(Scene clientScene, bool isReady)
        {
            try
            {
                G2C_ChgRoomMemberStatus _G2C_ChgRoomMemberStatus = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_ChgRoomMemberStatus()
                {
                    IsReady = isReady?1:0,
                }) as G2C_ChgRoomMemberStatus;
                if (_G2C_ChgRoomMemberStatus.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.ChgRoomMemberStatusAsync Error==1 msg={_G2C_ChgRoomMemberStatus.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 修改位置
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="newSeat"></param>
        public static async ETTask ChgRoomMemberSeatAsync(Scene clientScene, int newSeat)
        {
            try
            {
                G2C_ChgRoomMemberSeat _G2C_ChgRoomMemberSeat = await ET.Client.SessionHelper.GetSession(clientScene).Call(new
                C2G_ChgRoomMemberSeat()
                {
                    NewSeat = newSeat,
                }) as G2C_ChgRoomMemberSeat;
                if (_G2C_ChgRoomMemberSeat.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.ChgRoomSeatAsync Error==1 msg={_G2C_ChgRoomMemberSeat.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask ChgRoomMemberTeamAsync(Scene clientScene, RoomTeamId newTeam)
        {
            try
            {
                G2C_ChgRoomMemberTeam _G2C_ChgRoomMemberTeam = await ET.Client.SessionHelper.GetSession(clientScene).Call(new
                C2G_ChgRoomMemberTeam()
                {
                    NewTeam = (int)newTeam,
                }) as G2C_ChgRoomMemberTeam;
                if (_G2C_ChgRoomMemberTeam.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.ChgRoomTeamAsync Error==1 msg={_G2C_ChgRoomMemberTeam.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 设置ARMesh的下载链接
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="newBattleCfgId"></param>
        public static async ETTask<bool> SetARRoomInfoAsync(Scene clientScene, float arMapScale, ARMeshType arMeshType, string arSceneId, string arMeshDownLoadUrl, byte[] arMeshBytes)
        {
            try
            {
                G2C_SetARRoomInfo _G2C_SetARRoomInfo = await ET.Client.SessionHelper.GetSession(clientScene).Call(new
                        C2G_SetARRoomInfo()
                {
                    ARMapScale = (int)(arMapScale * 100),
                    ARMeshType = (int)arMeshType,
                    ARSceneId = arSceneId,
                    ARMeshDownLoadUrl = arMeshDownLoadUrl,
                    ARMeshBytes = arMeshBytes,
                }) as G2C_SetARRoomInfo;
                if (_G2C_SetARRoomInfo.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.SetARRoomInfoAsync Error==1 msg={_G2C_SetARRoomInfo.Message}");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        /// <summary>
        /// 修改房间对应战斗地图等信息
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="newBattleCfgId"></param>
        public static async ETTask ChgRoomBattleLevelCfgAsync(Scene clientScene, RoomTypeInfo roomTypeInfo)
        {
            try
            {
                G2C_ChgRoomBattleLevelCfg _G2C_ChgRoomBattleLevelCfg = await ET.Client.SessionHelper.GetSession(clientScene).Call(new
                        C2G_ChgRoomBattleLevelCfg()
                {
                    RoomTypeInfo = RoomTypeInfo.ToBytes(roomTypeInfo),
                }) as G2C_ChgRoomBattleLevelCfg;
                if (_G2C_ChgRoomBattleLevelCfg.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.ChgRoomBattleLevelCfgAsync Error==1 msg={_G2C_ChgRoomBattleLevelCfg.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 从战斗中退出
        /// </summary>
        /// <param name="clientScene"></param>
        public static async ETTask MemberQuitBattleAsync(Scene clientScene)
        {
            try
            {
                M2C_MemberQuitBattle _M2C_MemberQuitBattle = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2M_MemberQuitBattle()) as
                M2C_MemberQuitBattle;
                if (_M2C_MemberQuitBattle.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.MemberQuitBattleAsync Error==1 msg={_M2C_MemberQuitBattle.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 房主踢玩家出房间
        /// </summary>
        /// <param name="clientScene"></param>
        /// <param name="beKickedPlayerId"></param>
        public static async ETTask KickMemberOutRoomAsync(Scene clientScene, long beKickedPlayerId)
        {
            try
            {
                G2C_KickMemberOutRoom _G2C_KickMemberOutRoom = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_KickMemberOutRoom()
                        {
                            BeKickPlayerId = beKickedPlayerId,
                        }) as
                        G2C_KickMemberOutRoom;
                if (_G2C_KickMemberOutRoom.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.BeKickedOutRoomAsync Error==1 msg={_G2C_KickMemberOutRoom.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 战斗结束的返回
        /// </summary>
        /// <param name="clientScene"></param>
        public static async ETTask MemberReturnRoomFromBattleAsync(Scene clientScene)
        {
            {
                await ETTask.CompletedTask;
                return;
            }
            // try
            // {
            //     M2C_MemberReturnRoomFromBattle _M2C_MemberReturnRoomFromBattle = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2M_MemberReturnRoomFromBattle()) as
            //             M2C_MemberReturnRoomFromBattle;
            //     if (_M2C_MemberReturnRoomFromBattle.Error != ET.ErrorCode.ERR_Success)
            //     {
            //         Log.Error($"ET.Client.RoomHelper.MemberReturnRoomFromBattleAsync Error==1 msg={_M2C_MemberReturnRoomFromBattle.Message}");
            //     }
            // }
            // catch (Exception e)
            // {
            //     Log.Error(e);
            // }
        }

        /// <summary>
        /// 杀掉进程后重进的返回战斗
        /// </summary>
        /// <param name="clientScene"></param>
        public static async ETTask ReturnBackBattle(Scene clientScene)
        {
            try
            {
                G2C_ReturnBackBattle _G2C_ReturnBackBattle = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_ReturnBackBattle()) as G2C_ReturnBackBattle;
                if (_G2C_ReturnBackBattle.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.ReturnBackBattle Error==1 msg={_G2C_ReturnBackBattle.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

    }
}