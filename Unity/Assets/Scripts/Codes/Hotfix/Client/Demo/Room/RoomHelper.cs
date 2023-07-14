using System;

namespace ET.Client
{
    public static class RoomHelper
    {
        public static async ETTask GetRoomListAsync(Scene clientScene)
        {
            try
            {
                G2C_GetRoomList _G2C_GetRoomList = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoomList()) as G2C_GetRoomList;
                if (_G2C_GetRoomList.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.GetRoomListAsync Error==1 msg={_G2C_GetRoomList.Message}");
                }
                else
                {
                    clientScene.GetComponent<RoomManagerComponent>().Init(_G2C_GetRoomList.RoomInfos);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask GetRoomInfoAsync(Scene clientScene, long roomId)
        {
            try
            {
                G2C_GetRoomInfo _G2C_GetRoomInfo = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoomInfo()
                        {
                            RoomId = roomId,
                        }) as 
                G2C_GetRoomInfo;
                if (_G2C_GetRoomInfo.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.GetRoomInfoAsync Error==1 msg={_G2C_GetRoomInfo.Message}");
                }
                else
                {
                    clientScene.GetComponent<RoomManagerComponent>().Init(roomId, _G2C_GetRoomInfo.RoomInfo, _G2C_GetRoomInfo.RoomMemberInfos);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask<bool> CreateRoomAsync(Scene clientScene, string battleCfgId, bool isARRoom)
        {
            try
            {
                G2C_CreateRoom _G2C_CreateRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_CreateRoom()
                {
                    BattleCfgId = battleCfgId,
                    IsARRoom = isARRoom?1:0,
                }) as G2C_CreateRoom;
                if (_G2C_CreateRoom.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.CreateRoomAsync Error==1 msg={_G2C_CreateRoom.Message}");
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
        
        public static async ETTask<bool> JoinRoomAsync(Scene clientScene, long roomId)
        {
            try
            {
                G2C_JoinRoom _G2C_JoinRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_JoinRoom()
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
        
        public static async ETTask QuitRoomAsync(Scene clientScene)
        {
            try
            {
                G2C_QuitRoom _G2C_QuitRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_QuitRoom()) as G2C_QuitRoom;
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
        
        public static async ETTask ChgRoomMemberStatusAsync(Scene clientScene, bool isReady)
        {
            try
            {
                G2C_ChgRoomMemberStatus _G2C_ChgRoomMemberStatus = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_ChgRoomMemberStatus()
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
        
        public static async ETTask ChgRoomMemberSeatAsync(Scene clientScene, int newSeat)
        {
            try
            {
                G2C_ChgRoomMemberSeat _G2C_ChgRoomMemberSeat = await clientScene.GetComponent<SessionComponent>().Session.Call(new 
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
        
        public static async ETTask ChgRoomBattleLevelCfgAsync(Scene clientScene, string newBattleCfgId)
        {
            try
            {
                G2C_ChgRoomBattleLevelCfg _G2C_ChgRoomBattleLevelCfg = await clientScene.GetComponent<SessionComponent>().Session.Call(new 
                        C2G_ChgRoomBattleLevelCfg()
                {
                    NewBattleCfgId = newBattleCfgId,
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
        
        public static async ETTask MemberQuitBattleAsync(Scene clientScene)
        {
            try
            {
                M2C_MemberQuitBattle _M2C_MemberQuitBattle = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2M_MemberQuitBattle()) as 
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

        public static async ETTask BeKickedOutRoomAsync(Scene clientScene, long beKickedPlayerId)
        {
            try
            {
                G2C_KickMemberOutRoom _G2C_KickMemberOutRoom = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_KickMemberOutRoom()
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

        public static async ETTask MemberReturnRoomFromBattleAsync(Scene clientScene)
        {
            try
            {
                M2C_MemberReturnRoomFromBattle _M2C_MemberReturnRoomFromBattle = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2M_MemberReturnRoomFromBattle()) as 
                        M2C_MemberReturnRoomFromBattle;
                if (_M2C_MemberReturnRoomFromBattle.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"ET.Client.RoomHelper.MemberReturnRoomFromBattleAsync Error==1 msg={_M2C_MemberReturnRoomFromBattle.Message}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }

        public static async ETTask ReturnBackBattle(Scene clientScene)
        {
            try
            {
                G2C_ReturnBackBattle _G2C_ReturnBackBattle = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_ReturnBackBattle()) as G2C_ReturnBackBattle;
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